<script setup>
    import { RouterView } from 'vue-router';
</script>

<template>
    <header>
        <!--<img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

        <div class="wrapper">
            <HelloWorld msg="You did it!" />
        </div>-->
    </header>

    <main>
        <RouterView v-slot="{ Component }">
            <Transition name="fade" mode="out-in">
                <component :is="Component" />
            </Transition>
        </RouterView>
    </main>
</template>

<style scoped>
    header {
        line-height: 1.5;
    }

    .logo {
        display: block;
        margin: 0 auto 2rem;
    }

    @media (min-width: 1024px) {
        header {
            display: flex;
            place-items: center;
            padding-right: calc(var(--section-gap) / 2);
        }

        main {
            display: flex;
            place-items: center;
            padding: 0 var(--section-gap);
        }

        .logo {
            margin: 0 2rem 0 0;
        }

        header .wrapper {
            display: flex;
            place-items: flex-start;
            flex-wrap: wrap;
        }
    }
</style>

<style>
    body {
        background-color: whitesmoke; /* Set your desired background color or image here */
        font-size: larger;
    }
    .fade-enter-active, .fade-leave-active {
    transition: opacity 0.5s ease;
}

.fade-enter, .fade-leave-to /* .fade-leave-active in <2.1.8 */ {
    opacity: 0;
}
</style>
