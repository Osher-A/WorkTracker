<template>
    <header>
        <nav>
            <RouterLink to="/" exact>Add Work</RouterLink>
            <RouterLink to="/list">Work lists</RouterLink>
            <RouterLink to="/clients">Clients</RouterLink>
        </nav>
    </header>

    <main>
        <RouterView v-slot="{ Component }">
            <Transition name="fade" mode="out-in">
                <component :is="Component"/>
            </Transition>
        </RouterView>
    </main>
</template>

<script setup>
    import { RouterView, RouterLink } from 'vue-router';
</script>

<style>
   body {
    font-family:Arial, Helvetica, sans-serif;
   }
</style>

<style scoped>
    header {
        display: flex;
        justify-content: center; /* Center horizontally */
        align-items: center; /* Align items vertically */
        height: 20px; /* Adjust height as needed */
        width: 100%; /* Ensure header spans full width */
        margin-top: 20px; /* Adjust top margin as needed */
        position: fixed; /* Fix header at the top */
        top: 0;
        left: 0;
        background-color: white; /* Set background color */
        z-index: 1000; /* Ensure header is above other content */
    }

    nav {
        display: flex;
        gap: 1rem;
        font-size: x-large;
    }

    .router-link-active {
        font-weight: bold;
        color: blue;
    }

    @media (min-width: 1024px) {
        header .wrapper {
            display: flex;
            place-items: flex-start;
            flex-wrap: wrap;
        }
    }
</style>

<style>
    .fade-enter-active, .fade-leave-active {
        transition: opacity 0.5s  ease;
    }

    .fade-enter, .fade-leave-to /* .fade-leave-active in <2.1.8 */ {
        opacity: 0;
    }

</style>
