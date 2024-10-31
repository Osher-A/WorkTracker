import { defineStore } from 'pinia'

export const useEditModeStore = defineStore('editMode', {
    state: () => ({
        editMode: false,
    }),
    getters: {
        getEditMode() {
            return this.editMode
        },
    },
    actions: {
        setEditMode(value) {
            this.editMode = value
        },
    },
})