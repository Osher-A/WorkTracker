<template>
    <div class="work-tracker-form">
        <!-- Date Picker -->
        <div class="form-group">
            <label for="date">Date:</label>
            <input type="date" id="date" v-model="formattedDate" class="form-control">
        </div>

        <!-- Client Information -->
        <work-detail v-for="(workDetail, index) in work.workDetails" 
            :key="index"
            v-model="work.workDetails[index]"
            @remove="removeWorkDetail(index)" />
        <div style="display: flex; justify-content: space-between; align-items: center;">
            <!-- Number of Clients -->
            <div class="number-of-clients" style="flex-grow: 1; text-align: left;">
                <label style="font-weight:bold;">Number of Clients:</label>
                <span style="padding: 4px; font-weight: bold;">{{ numberOfClients }}</span>
            </div>
            <!-- Total Hours Worked -->
            <div class="total-hours" style="flex-grow: 1; text-align: right;">
                <label style="font-weight:bold;">Total Hours Worked:</label>
                <span style="padding: 4px; font-weight: bold;">{{ totalHours }}</span>
            </div>
        </div>
        <!-- Submit Button -->
        <button class="btn btn-success form-control submit-btn" style="background-color: black;" @click="submitWork">Submit</button>
    </div>
</template>

<script>
    import { required } from '@vuelidate/validators';
    import { useVuelidate } from '@vuelidate/core';
    import WorkDetail from '@/components/WorkDetail.vue';

    export default {
        props: ['id'],
        components: {
            WorkDetail
        },
        data() {
            return {
                work: {
                    workDetails: []
                    }
                };
        },
        validations() {
            return {
                work: {
                    date: { required }
                }
            };
       },
        setup() {
            const v$ = useVuelidate();
            return { v$ };
        },
        methods: {
            editWork() {
                this.v$.$touch(); // Touch all fields to trigger validation
                if (this.v$.$invalid) {
                    // Prevent submission if any field is invalid
                    return;
                }
                this.clearWorkFields();
            },
            clearWorkFields() {
                this.work = {};
                // Reset the validation state for currentWorkDetail
                if (this.v$.work) {
                    this.v$.work.$reset();
                }
            },
            removeWorkDetail(index) {
                this.work.workDetails.splice(index, 1);
            },
            async submitWork() {
                console.log(this.work);
                this.v$.$touch(); // Touch all fields to trigger validation
                console.log(this.v$);
                if (this.v$.$invalid) {
                    // Prevent submission if any field is invalid
                    return;
                }

                const axiosOptions = {
                     method: 'PUT', url: 'Work', 
                    data: { id: this.id, date: this.work.date, workDetails: this.work.workDetails}
                };
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
                return this.work.workDetails?.length || 0;
            },
            totalHours() {
                return this.work.workDetails?.reduce((total, workDetail) => total + workDetail.hours, 0) || 0;
            },
            formattedDate: {
            get() {
                // Return the date part if work.date is defined and in the correct format
                return this.work.date ? this.work.date.split('T')[0] : '';
            },
            set(newValue) {
                // Update work.date when the user selects a new date
                this.work.date = newValue;
            },
        },
    },

        async created() {
            try {
                const result = await this.$axios.get(`/work/${this.id}`);
                this.work = result.data;
            } catch (error) {
                console.error('Error fetching work details:', error);
            }
        }
    };
</script>

<style scoped>
    .work-tracker-form {
        max-width: 800px;
        min-width: 500px;
        margin: 0, auto;
        padding: 20px;
        background-color: #ffffff;
        border: 1px solid #e0e0e0;
        border-radius: 10px;
    }

    .form-group {
        margin-bottom: 20px;
    }

    .client-section {
        border: 1px solid #e0e0e0;
        padding: 20px;
        margin-bottom: 20px;
        border-radius: 10px;
    }

    .remove-client-btn {
        margin-top: 10px;
    }

    .add-client-btn {
        margin-top: 20px;
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