<template>
    <main-layout width="100%">
    <div class="add-work-form">
        <!-- Date Picker -->
        <div class="form-group">
            <label for="date">Date:</label>
            <input type="date" id="date" v-model="date" class="form-control">
        </div>

        <!-- Client Information -->
        <div class="client-section">
            <div class="form-group">
                <label for="clientName">Client Name:</label>
                <select v-model="currentWorkDetail.client.id" class="form-control">
                    <option value=0 disabled selected="true">Select a client</option>
                    <option v-for="client in clients" :key="client.id" :value="client.id">
                        {{ client.name }}
                    </option>
                </select>
                <div class="invalid-feedback" v-if="v$.currentWorkDetail.client.id.$error">Client is required.</div>
            </div>

            <!-- Work Description -->
            <div class="form-group">
                <label for="workDescription">Work Description:</label>
                <textarea id="workDescription" v-model="currentWorkDetail.description" class="form-control"></textarea>
            </div>

            <!-- Hours of Work -->
            <div class="form-group">
                <label for="hoursWorked">Hours of Work:</label>
                <input type="number" id="hoursWorked" v-model="currentWorkDetail.hours" min="0" class="form-control">
                <div class="invalid-feedback" v-if="v$.currentWorkDetail.hours.$error">Hours of work is required.</div>
            </div>

            <div class="btn-group-flex" role="group">
                <!-- Clear Fields Button -->
                <button class="btn small" @click="removeWorkDetail" title="Clear work detail fields">
                <i class="fas fa-undo"></i>
                </button>
                <!-- Add Client Button -->
                <button class="btn small" @click="addWorkDetail" title="Add work detail">
                <i class="fas fa-plus"></i>
                </button>
            </div>
        </div>

        <div style="display: flex; justify-content: space-between; align-items: center;">
            <!-- Number of Clients -->
            <div style="margin-top: 30px;flex-grow: 1; text-align: left;">
                <label style="font-weight:bold;">Number of Clients:</label>
                <span style="padding: 4px; font-weight: bold;">{{ numberOfClients }}</span>
            </div>
            <!-- Total Hours Worked -->
            <div class="total-hours" style="margin-top: 30px; flex-grow: 1; text-align: right;">
                <label style="font-weight:bold;">Total Hours Worked:</label>
                <span style="padding: 4px; font-weight: bold;">{{ totalHours }}</span>
            </div>
        </div>
        <!-- Submit Button -->
        <button class="btn btn-success form-control submit-btn" style="background-color: black;" @click="submitWork">Submit</button>
    </div>
</main-layout>
</template>

<script>
    import { required, minValue, numeric} from '@vuelidate/validators';
    import { useVuelidate } from '@vuelidate/core';
    export default {
        data() {
            return {
                date: new Date().toISOString().slice(0, 10),
                currentWorkDetail: {
                    client: { id: 0},
                    description: '',
                    hours: 0
                }, // Initial client
                workDetails: [], // Array to store multiple work details
                clients: []
                };
        },
        validations() {
        return {
            date: { required },
            currentWorkDetail: {
                hours: { required, numeric, minValue: minValue(0.1)},
                client:{
                    id: {required, minValue: minValue(1)}
                }
            },
            }
        },
        setup() {
            const v$ = useVuelidate();
            return { v$ };
        },

        async created() {
            const response = await this.$axios.get('/Client');
            this.clients = response.data;
        },
        
        methods: {
            addWorkDetail() {
                this.v$.$touch(); // Touch all fields to trigger validation
                if (this.v$.$invalid) {
                    // Prevent submission if any field is invalid
                    return;
                }
                this.workDetails.push(this.currentWorkDetail);
                this.clearWorkDetailFields();
            },
            clearWorkDetailFields() {
                this.currentWorkDetail = {
                    client: { id: ''},
                    description: '',
                    hours: 0
                };

                // Reset the validation state for currentWorkDetail
                if (this.v$.currentWorkDetail) {
                    this.v$.currentWorkDetail.$reset();
            }
            },
            removeWorkDetail() {
                this.workDetails.splice(this.workDetails.length - 1, 1);
                this.clearWorkDetailFields();
            },
            async submitWork() {
                this.v$.$touch(); // Touch all fields to trigger validation
                if (this.v$.$invalid) {
                    // Prevent submission if any field is invalid
                    return;
                }

                this.workDetails.push(this.currentWorkDetail);

                const axiosOptions = { method: 'POST', url: '/Work', data: { date: this.date, workDetails: this.workDetails } };
              try {
                    await this.$axios(axiosOptions);  

                    this.$router.push({ name: 'Grid' });
              } catch (error) {
                    console.error(error);
                }
            }
        },
        computed: {
            numberOfClients() {
                return this.workDetails.length;
            },
            totalHours() {
                return this.workDetails.reduce((total, workDetail) => total + workDetail.hours, 0) || 0;
            }
        }
    };
</script>

<style scoped>
    .add-work-form {
        width: 100%;
        min-width: 500px;
        padding: 20px;
        border: 1px solid black;
        border-radius: 10px;
        background-color: white;
    }

    .form-group {
        margin-bottom: 5%;
    }

    .submit-btn {
        margin-top: 30px;
    }

    .btn-group-flex {
    display: flex;
    justify-content: space-between;
}

    .btn-group-flex .form-control {
        flex-grow: 1; /* Make buttons grow equally */
        margin: 0 5px; /* Add some space between buttons */
    }

    /* Adjust the first and last button margin for proper alignment */
    .btn-group-flex .form-control:first-child {
        margin-left: 0;
    }

    .btn-group-flex .form-control:last-child {
        margin-right: 0;
    }

    .small {
    background-color: rgba(13, 30, 31, 0.342);
    }
    /* Add styles for .is-invalid and .invalid-feedback if not already present */
    .is-invalid {
        border-color: #ff3860;
    }
    .invalid-feedback {
        display: block;
        color: #ff3860;
    }
</style>
