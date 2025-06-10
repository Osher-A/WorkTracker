import { defineStore } from 'pinia' 

export const useSearchRangeStore = defineStore('searchDateRange', {
    state: () => ({
        searchFromDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1, new Date().getTimezoneOffset() / -60).toISOString().slice(0, 10),
        searchToDate: new Date().toISOString().slice(0, 10),
    }),
    getters: {
        getSearchFromDate() {
            return this.searchFromDate
        },
        getSearchToDate() {
            return this.searchToDate
        },
    },
    actions: {
        setSearchFromDate(value) {
            this.searchFromDate = value
        },
        setSearchToDate(value) {
            this.searchToDate = value
        },
    },
})