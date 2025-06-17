<template>
    <main-layout>
        <div class="work-tracker-form">
            <!-- Date Picker -->
            <div class="form-group">
                <label for="date">Date:</label>
                <input type="date" id="date" v-model="formattedDate" class="form-control" :disabled="!isEditMode">
            </div>

            <work-detail v-for="(workDetail, index) in work.workDetails"
                         :key="index"
                         v-model="work.workDetails[index]"
                         @remove="removeWorkDetail(index)"
                         @add="addWorkDetail" />
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
            <button v-if="isEditMode" class="btn btn-success form-control submit-btn" style="background-color: black;" @click="submitWork">Submit</button>
        </div>
    </main-layout>
</template>

<script>
    import { required } from '@vuelidate/validators';
    import { useVuelidate } from '@vuelidate/core';
    import WorkDetail from '@/components/WorkDetail.vue';
    import { useEditModeStore } from '@/stores/editModeStore';

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
            const isEditMode = useEditModeStore().getEditMode;
            return { v$ , isEditMode };
        },
        methods: {
            removeWorkDetail(index) {
                this.work.workDetails.splice(index, 1);
            },

            addWorkDetail() {
                this.work.workDetails.push({ client: { id: 0 }, description: '', hours: 0 });
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
            },

        },
        computed: {
            title() {
                return this.isEditMode ? 'Edit Work' : 'View Work';
            },
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
        width: 100%;
        min-width: 500px;
        margin: 0, auto;
        padding: 20px;
        background-color: white;
        border: 1px solid black;
        border-radius: 10px;
    }

    .form-group {
        margin-bottom: 5%;
    }

    .submit-btn {
        margin-top: 30px;
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