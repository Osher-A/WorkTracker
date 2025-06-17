<template>
  <b-overlay :show="isLoading" spinner>
    <main-layout width="100%">
      <div class="search-filters">
        <div>
          <label for="searchFromDate">From:</label>
          <input type="date" class="form-control" id="searchFromDate" v-model="searchFromDate"/>
        </div>
        <div>
          <label for="searchToDate">To:</label>
          <input type="date" class="form-control" id="searchToDate" v-model="searchToDate" />
        </div>
     </div>
    
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Hours Per Client</th>
            <th>Total Hours</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
        <tr v-for="(workDay, index) in work" :key="workDay.id"
          @click="selectRow(index)" 
          :class="{'selected-row': selectedIndex === index}">
          <td>{{ new Date(workDay.date).toLocaleDateString() }}</td>
          <td>
            <ul>
              <li v-for="(client, clientIndex) in getTotalHoursPerClient(workDay.workDetails)" :key="clientIndex">
                <strong>{{ client.totalHours }} hrs </strong> - {{ client.clientName }}
              </li>
            </ul>
          </td>
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
      <div class="mt-3">
        <ul>
          <li v-for="client in totalHoursWorkedPerClientForSelectedDays" :key="client.clientName">
            <strong>{{ client.totalHours }} - </strong>{{ client.clientName }}
          </li>
        </ul>
      </div>
        <div class="total-hours">
            Total Hours Worked: {{ totalHoursWorked }} hours
        </div>
      <!-- <div class="button-container">
      <button class="button form-control mt-5" @click="$router.push({ name: 'Home'})" title="Add Work detail">
        <i class="fas fa-plus"></i>
      </button>
    </div> -->
    </main-layout>
    
  </b-overlay>
</template>
  
  <script>
      import { useEditModeStore } from '@/stores/editModeStore';  // Import the store
      import { useSearchRangeStore } from '@/stores/searchDateRangeStore';  // Import the store
      import { useSelectedGridItemStore } from '@/stores/selectedGridItemStore';
      import Swal from 'sweetalert2';

  export default {
    data() {
      return {
        isLoading: true,
        work: [],
        showModal: true,
      };
    },
    computed: {
      searchFromDate: {
        get() {
          return useSearchRangeStore().searchFromDate;
        },
        set(value) {
          const store = useSearchRangeStore();
          store.setSearchFromDate(value);
          this.getFilteredResult(); // Trigger the filtering logic
        }
      },
      searchToDate: {
        get() {
          return useSearchRangeStore().searchToDate;
        },
        set(value) {
          const store = useSearchRangeStore();
          store.setSearchToDate(value);
          this.getFilteredResult(); // Trigger the filtering logic
        }
      },
      totalHoursWorkedPerClientForSelectedDays() {
        if (this.work) {
          // Flatten the structure to get a single list of all workDetails
          const flattenedWorkDetails = this.work.flatMap(workDay => workDay.workDetails);

          const result = this.getTotalHoursPerClient(flattenedWorkDetails);
          return result;

          // // Calculate totals using reduce
          // const totals = flattenedWorkDetails.reduce((acc, workDetail) => {
          //   const clientName = workDetail.client.name;
          //   if (!acc[clientName]) {
          //     acc[clientName] = { clientName, totalHours: 0 };
          //   }
          //   acc[clientName].totalHours += workDetail.hours;
          //   return acc;
          // }, {});

          // // Convert the totals object into an array of objects
          // return Object.values(totals);
        }
        else {
          return 0;
        }
      },
     
      totalHoursWorked() {
        if (this.work) {
          return this.work.reduce((acc, workDay) => acc + this.getTotalHours(workDay.workDetails), 0);
        }
        else {
          return 0;
        }
      },
      selectedIndex() {
        const selectedGridItemStore = useSelectedGridItemStore();
        return selectedGridItemStore.selectedGridItem; // Reactively link to the store's value
      },
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
        const result = await Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Yes, delete it!'
        });
        if (result.isConfirmed) {
          try {
            await this.$axios.delete(`/work/${workDay.id}`);
            await this.apiResult();
          } catch (error) {
            console.error('Error deleting work detail:', error);
          }
        }
      },
      selectRow(index) {
        useSelectedGridItemStore().setSelectedGridItem(index); // Update the store with the selected index
      },
      getTotalHours(workDetails) {
        return workDetails.reduce((acc, work) => acc + work.hours, 0);
      },
      getTotalHoursPerClient(workDetails) {
        const totals = {};
        workDetails.forEach(workDetail => {
          const clientName = workDetail.client.name;
          if (!totals[clientName]) {
            totals[clientName] = 0;
          }
          totals[clientName] += workDetail.hours;
        });
        return Object.entries(totals).map(([clientName, totalHours]) => ({ clientName, totalHours }));
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
      },
      },
      async created() {
        this.isLoading = true;
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
    background-color: white;;
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
  background-color: white;
}


td, th {
  border: 1px solid black;
  text-align: left;
  padding: 8px;
}

td ul {
  padding-left: 0;
  margin: 0;
  list-style: none;
}

td ul li:last-child {
  margin-bottom: 0;
}

tr:nth-child(even) {
  background-color: #ddd;
}

.selected-row {
  background-color: darkgray !important;
  
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
  margin-top: 10px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-weight: bold;
  background-color: #ddd;
}
</style>