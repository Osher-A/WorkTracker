<template>
    <div class="client-section">
        <div class="form-group">
            <label for="clientName">Client Name:</label>
                <select v-if="isEditMode" v-model="value.client.id" class="form-control">
                    <option value=0 disabled selected="true">Select a client</option>
                    <option v-for="client in clients" :key="client.id" :value="client.id">
                        {{ client.name }}
                    </option>
                </select>
                <input v-else type="text" v-model="value.client.name" class="form-control" disabled>
                <div class="invalid-feedback" v-if="v$.value.client.id.$error">Client is required.</div>
        </div>

        <!-- Work Description -->
        <div class="form-group">
            <label for="workDescription">Work Description:</label>
            <textarea id="workDescription" v-model="value.description" class="form-control" :disabled="!isEditMode"></textarea>
        </div>

        <!-- Hours of Work -->
        <div class="form-group">
            <label for="hoursWorked">Hours of Work:</label>
            <input type="number" id="hoursWorked" v-model="value.hours" min="0" class="form-control" :disabled="!isEditMode">
            <div class="invalid-feedback" v-if="v$.value.hours.$error">Hours of work is required.</div>
        </div>

        <div v-if="isEditMode" class="btn-group-flex" role="group">
            <!-- remove work detail-->
            <button class="btn small" @click="$emit('remove')" title="Remove work detail">
            <i class="fas fa-trash"></i>
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
import { useEditModeStore } from '@/stores/editModeStore'; // Import the store

export default {
    emits: ['update:modelValue', 'remove', 'add'],
    // By using the default prop name `modelValue` and event name `update:modelValue`, we can use `v-model` on the parent component.
    // This will automatically bind the value to the `modelValue` prop in the child component and update it when the value changes using the `update:modelValue` event.
    // If we were to use a different prop name or event name, we would need to specify the custom event name in the parent component, e.g., `v-model:customEventName`.
    // In the child component, we would then need to manually emit the custom event using `this.$emit('update:customEventName', value)`.
    // Alternatively, we can use the `defineModel` function in the Composition API to handle custom prop and event names more easily.

    // TO CLARIFY:
    // v-model provides two-way binding between a value and an input/component.
    //
    // --- Native Inputs ---
    // For native HTML inputs (like <input>, <textarea>, <select>):
    //   - v-model binds the input's value to a data property in the same component (getter).
    //   - When the user changes the input, native events (like 'input' or 'change') update the data property (setter).
    //
    // --- Custom Components ---
    // For custom components (like <MyComponent v-model="someValue" />):
    //   - The parent passes its value to the child via the 'modelValue' prop (getter).
    //   - In the child, you can use a computed property with a getter (returns 'modelValue') and a setter (emits 'update:modelValue' with the new value).
    //   - Alternatively, you can use v-model on native inputs inside the child, binding them to a local property, and then emit 'update:modelValue' when you want to sync with the parent.
    //   - In Vue 3.4+, you can also use the defineModel macro for simpler syntax.
    //   - The parent listens for 'update:modelValue' and updates its own data property accordingly.
    //
    // Summary:
    //   - Native inputs: v-model uses the value attribute and native events within the same component.
    //   - Custom components: v-model uses the 'modelValue' prop and 'update:modelValue' event for two-way binding between parent and child. The child can use a computed property, v-model on its own inputs, or defineModel to manage this.
    props: {
        modelValue: {
        type: Object,
        required: true
        }
    },

    data() {
        return {
           clients: []
        }
    },
    setup() {
            const v$ = useVuelidate();
            const isEditMode = useEditModeStore().getEditMode; // Use the store
            return { v$ , isEditMode};
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
            addWorkDetail() {
                this.$emit('add');
            }
        },
        computed: {
            value: {
                get() {
                    return this.modelValue;
                },
                set(value) {
                    this.v$.$touch(); // Trigger validation
                    if (!this.v$.$invalid) {
                        // by using the event name - update:modelValue, we can use v-model on the parent without specifying the event name if we use a different name than we would need to specify the event name in the parent component e.g. v-model:eventName
                        // since we want to add validations we need to add this computed property and emit the event only if the form is valid, if not for the validations we would not need to emit the event manually we can just rely on v-model to update the value
                        this.$emit('update:modelValue', value);
                    }
                }
            }
        }
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