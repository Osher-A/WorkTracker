import { defineStore } from 'pinia';

export const useSelectedGridItemStore = defineStore('selectedGridItem', {
    state: () => ({
        selectedGridItem: null,
    }),
    getters: {
        getSelectedGridItem() {
            return this.selectedGridItem
        },
    },
    actions: {
        setSelectedGridItem(value) {
            this.selectedGridItem = value
        },
    },
})