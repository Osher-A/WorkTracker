<template>
  <b-overlay :show="isLoading" spinner>
    <main-layout>
      <title-header title="Work Grid"></title-header>
      <div class="search-filters">
        <div>
          <label for="searchFromDate">From:</label>
          <input type="date" class="form-control" id="searchFromDate" v-model="searchFromDate" @change="getFilteredResult"/>
        </div>
        <div>
          <label for="searchToDate">To</label>
          <input type="date" class="form-control" id="searchToDate" v-model="searchToDate" @change="getFilteredResult" />
        </div>
     </div>
    
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Total Hours</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
        <tr v-for="(workDay, index) in work" :key="workDay.id"
          @click="selectRow(index)" 
          :class="{'selected-row': selectedIndex === index}">
          <td>{{ new Date(workDay.date).toLocaleDateString() }}</td>
          <td>{{ getTotalHours(workDay.workDetails) }}</td>
          <td class="action-buttons">
            <button @click="editWorkDay(workDay)">
              <i class="fas fa-pencil-alt"></i>
            </button>
            <button @click="viewWorkDay(workDay)">
                <i class="fas fa-eye"></i>
            </button>
            <button @click="deleteWorkDetail(workDay)">
              <i class="fas fa-trash"></i>
            </button>
          </td>
        </tr>
        </tbody>
      </table>
      <div>
        <ul>
          <li v-for="client in totalHoursWorkedPerClient" :key="client.clientName">
            {{ client.clientName }}: {{ client.totalHours }} hours
          </li>
        </ul>
      </div>
        <div class="total-hours">
            Total Hours Worked: {{ totalHoursWorked }} hours
        </div>
      <div class="button-container">
      <button class="button form-control mt-5" @click="$router.push({ name: 'Home'})" title="Add Work detail">
        <i class="fas fa-plus"></i>
      </button>
    </div>
    </main-layout>
  </b-overlay>
</template>
  
  <script>
  import { useEditModeStore } from '@/stores/editModeStore';  // Import the store
  export default {
    data() {
      return {
        isLoading: true,
        work: [],
        searchFromDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1, new Date().getTimezoneOffset() / -60).toISOString().slice(0, 10),
        searchToDate: new Date().toISOString().slice(0, 10),
        selectedIndex: null, // Tracks the selected row
      };
    },
    computed: {
      totalHoursWorkedPerClient() {
       if (this.work) {
         // Flatten the structure to get a single list of all workDetails
         const flattenedWorkDetails = this.work.flatMap(workDay => workDay.workDetails);
 
         // Calculate totals
         const totals = flattenedWorkDetails.reduce((acc, workDetail) => {
           const clientName = workDetail.client.name;
           if (!acc[clientName]) {
             acc[clientName] = { clientName, totalHours: 0 };
           }
           acc[clientName].totalHours += workDetail.hours;
           return acc;
         }, {});
 
         // Convert the totals object into an array of objects
         return Object.values(totals);
       }
       else{
          return 0;
       }
        },
      totalHoursWorked() {
        if (this.work) {
          return this.work.reduce((acc, workDay) => acc + this.getTotalHours(workDay.workDetails), 0);
        }
        else{
          return 0;
        }
      }
    },
    methods: {
      viewWorkDay(workDay) {
        useEditModeStore().setEditMode(false); // Set edit mode to false
        this.$router.push({ name: 'WorkDay', params: { id: workDay.id } });
      },

      editWorkDay(workDay) {
        useEditModeStore().setEditMode(true); // Set edit mode to true
        this.$router.push({ name: 'WorkDay', params: { id: workDay.id } });
      },

      async deleteWorkDetail(workDay) {
        try {
          await this.$axios.delete(`/work/${workDay.id}`);
          await this.apiResult();
        } catch (error) {
          console.error('Error deleting work detail:', error);
        }
      },
      selectRow(index) {
        this.selectedIndex = index;
      },
      getTotalHours(workDetails) {
        return workDetails.reduce((acc, work) => acc + work.hours, 0);
      },
      getFilteredResult()
      {
        this.apiResult();
      },
      async apiResult(){
        try {
          const result = await this.$axios.get('/work', {
            params: {
              fromDate: this.searchFromDate,
              toDate: this.searchToDate
            }
          });
          this.work = result.data.sort((a, b) => new Date(a.date) - new Date(b.date));
        } catch (error) {
          console.error('Error fetching work details:', error);
        }
      }
    },
    async created() {
      this.isLoading = true;

      // fetch work details from the server
      this.searchfromDate = new Date(new Date().getFullYear(), new Date().getMonth(), 1, new Date().getTimezoneOffset() / -60).toISOString().slice(0, 10);  
      this.searchToDate = new Date().toISOString().slice(0, 10);
      await this.apiResult();

      this.isLoading = false;
  }
};
  </script>
  <style scoped>

.search-filters, table, .container > div {
  width: 100%; /* Ensures each child takes the full width of its parent */
}

.search-filters-input {
  margin: 0 10px 20px 0; /* Adds spacing between inputs and below them */
}

.search-filters {
    display: flex;
    gap: 10px; /* Adjust the space between each label-input pair */
    justify-content: space-between; /* Adjust as needed */
    align-items: center; /* Align items vertically */
    margin-bottom: 20px; /* Space below the filters */
    padding: 10px;
    border: black 1px solid;
    border-radius: 5px;
}

.button-container {
  text-align: center;
  margin-bottom: 10px;
}

/* Table styling */
table {
  margin-top: 20px;
  border-collapse: collapse;
  border: 1px solid black;
  min-width: 500px; 
}


td, th {
  border: 1px solid black;
  text-align: left;
  padding: 8px;
}

tr:nth-child(even) {
  background-color: #ddd;
}

.selected-row {
  background-color: darkgray;
}

/* Buttons styling */
button {
  cursor: pointer;
  padding: 10px 15px;
  margin: 2px;
  border: none;
  border-radius: 5px;
  background-color: #111d2980;
  color: white;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: darkgray;
}

.action-buttons {
  display: flex;
  justify-content: space-between; /* This spreads out the buttons evenly */
  padding: 10 10px; /* Optional: Adds some padding on the sides */
}

/* Total Hours Worked styling */
.total-hours {
  margin-top: 20px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-weight: bold;
}
</style>