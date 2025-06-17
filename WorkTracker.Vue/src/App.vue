<template>
    <header>
        <nav>
            <RouterLink to="/" exact class="router-link">Add Work</RouterLink>
            <RouterLink to="/list" class="router-link">Work lists</RouterLink>
            <RouterLink to="/clients" class="router-link">Clients</RouterLink>
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
    background-color: rgb(241, 241, 240) !important;
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
        z-index: 1000; /* Ensure header is above other content */
    }

    nav {
        display: flex;
        gap: 1rem;
        font-size: x-large;
        justify-content: space-between;
        width: 600px;
        margin-top: 20px;
        border-bottom: 2px solid #ccc; /* Tab underline */
    }

   .router-link {
        display: inline-block;
        padding: 10px 30px;
        border: 1px solid #ccc;
        border-bottom: none;
        background: #f9f9f9;
        color: #333;
        text-decoration: none;
        border-radius: 8px 8px 0 0;
        margin-right: 2px;
        transition: background 0.2s;
        position: relative;
        top: 2px;
   }

    .router-link-active {
        background: #fff;
        color: #1976d2;
        font-weight: bold;
        border-bottom: 2px solid #fff;
        z-index: 1;
    }

    .router-link:last-child {
        margin-right: 0;
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
