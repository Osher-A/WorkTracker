<template>
    <div class="client-section">
        <div class="form-group">
            <label for="clientName">Client Name:</label>
                <select v-model="value.client.id" class="form-control">
                    <option value=0 disabled selected="true">Select a client</option>
                    <option v-for="client in clients" :key="client.id" :value="client.id">
                        {{ client.name }}
                    </option>
                </select>
                <div class="invalid-feedback" v-if="v$.value.client.id.$error">Client is required.</div>
        </div>

        <!-- Work Description -->
        <div class="form-group">
            <label for="workDescription">Work Description:</label>
            <textarea id="workDescription" v-model="value.description" class="form-control"></textarea>
        </div>

        <!-- Hours of Work -->
        <div class="form-group">
            <label for="hoursWorked">Hours of Work:</label>
            <input type="number" id="hoursWorked" v-model="value.hours" min="0" class="form-control">
            <div class="invalid-feedback" v-if="v$.value.hours.$error">Hours of work is required.</div>
        </div>

        <div class="btn-group-flex" role="group">
            <!-- remove work detail-->
            <button class="btn small" @click="$emit('remove')" title="Remove work detail">
            <i class="fas fa-undo"></i>
            </button>
            <!-- Save changes -->
            <button class="btn small" @click="editWork" title="Save changes">
            <i class="fas fa-save"></i>
            </button>
            <!--Add Work button-->
            <button class="btn small" @click="addWorkDetail" title="Add work detail">
            <i class="fas fa-plus"></i>
            </button>
        </div>
    </div>
</template>
<script>
import { required, minValue, numeric} from '@vuelidate/validators';
import { useVuelidate } from '@vuelidate/core';
export default {
    emits: ['update:modelValue', 'remove', 'add'],
    // by using modelValue, we can use v-model on the parent component and it would automaticlly bind the value to the modelValue prop 
    // if we were to use a differnt name we would have to add in the parent compenent v-bind (or the equivilent shorthand) to bind it manually in the parent component
    props: {
        modelValue: {
        type: Object,
        required: true
        }
    },

    data() {
        return {
           value: this.modelValue,
           clients: []
        }
    },
    setup() {
            const v$ = useVuelidate();
            return { v$ };
        },

    validations() {
        return {
            value: {
                hours: { required, numeric, minValue: minValue(0.1)},
                client: {
                    id: { required, minValue: minValue(1)}, // Client is required and must be greater than 0
                }
            },
            }
        },
    async created() {
        const response = await this.$axios.get('/Client');
        this.clients = response.data;
    },


    methods: {
        editWork() {
            this.v$.$touch(); // Touch all fields to trigger validation
                    if (this.v$.$invalid) {
                        // Prevent submission if any field is invalid
                        return;
                    }
                // by using the event name - update:modelValue, we can use v-model on the parent without specifying the event name if we use a different name than we would need to specify the event name in the parent component v-model:eventName
                this.$emit('update:modelValue', this.modelValue);
            },
            addWorkDetail() {
                this.$emit('add');
            }
        },
}
</script>

<style scoped>
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