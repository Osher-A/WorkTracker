<template>
  <main-layout >
    <title-header title="Client Manager"></title-header>
    <div class="client-manager">
      <table>
        <thead>
          <tr>
            <th>Client Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(client, index) in clients" :key="client.id">
            <td>
              <input 
                v-if="client.isEditing" 
                v-model="client.name" 
                type="text" 
                class="form-control" 
              />
              <span v-else>{{ client.name }}</span>
            </td>
            <td class="action-buttons">
              <button @click="editClient(index)" v-if="!client.isEditing">Edit</button>
              <button @click="saveClient(index)" v-if="client.isEditing">Save</button>
              <button @click="deleteClient(index)" disabled>Delete</button>
            </td>
          </tr>
          <tr v-if="isAddingNewClient">
            <td>
              <input 
                v-model="newClientName" 
                type="text" 
                class="form-control" 
                placeholder="New Client Name" 
              />
            </td>
            <td>
              <button class="btn btn-small" @click="saveNewClient">Save</button>
            </td>
          </tr>
        </tbody>
      </table>
      <div style="display: flex; justify-content: center; margin-top: 20px;">
          <button @click="showNewClientInput" v-if="!isAddingNewClient" title="Add client">
              <i class="fas fa-plus"></i>
          </button>
      </div>
     
    </div>
</main-layout>
</template>

<script>
    export default {
        data() {
            return {
                clients: [],
                newClientName: '',
                isAddingNewClient: false
            };
        },

        async created() {
            // Fetch clients from the server
            try {
                const response = await this.$axios.get('client');
                const clients = response.data;
                this.clients = clients.map(client => ({ id: client.id, name: client.name, isEditing: false }));
            } catch (e) {
                console.error(e);
            }
        },
        methods: {
            showNewClientInput() {
                this.isAddingNewClient = true;
            },
            async saveNewClient() {
                try {
                    if (this.newClientName.trim() !== '') {
                        await this.$axios.post('client', { name: this.newClientName });
                        this.newClientName = '';
                        this.isAddingNewClient = false;
                    }
                } catch (e) {
                    console.error(e);
                }
            },
            editClient(index) {
                this.clients[index].isEditing = true;
            },
            async saveClient(index) {
                try {
                    await this.$axios.put('client', {id: this.clients[index].id, name: this.clients[index].name });

                    this.clients[index].isEditing = false;
                } catch (e) {
                    console.error(e);
                }
            },
            async deleteClient(index) {
                try {
                    await this.$axios.delete(`client/${this.clients[index].id}`);
                } catch (e) {
                    console.error(e);
                }
            }
        }
    };
</script>

<style>
    .client-manager {
      padding: 20px;
      background-color: #ffffff;
      border: 1px solid #e0e0e0;
      border-radius: 10px;
      width: 100%;
    }
    table {
      width: 100%;
      border-collapse: collapse;
    }
    th, td {
      padding: 10px;
      border: 1px solid #e0e0e0;
    }
    .form-control {
      width: 100%;
      padding: 8px;
      box-sizing: border-box;
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
        justify-content:  space-around; /* This spreads out the buttons evenly */
        padding: 10px 10px; /* Optional: Adds some padding on the sides */
    }
</style>