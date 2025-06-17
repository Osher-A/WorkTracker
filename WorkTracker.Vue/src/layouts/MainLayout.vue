<template>
    <div class="container" :style="{width: width}">
        <slot></slot>
    </div>

</template>
<script>
    export default {
        name: 'MainLayout',
        props: {
            width: {
                type: String,
                default: '30%'
            }
        }
    };
</script>
<style scoped>
    .container {
        display: flex;
        flex-direction:  column;
        margin-bottom: 30px;
        min-width: 500px;
        max-width: 800px;
        margin-top: 150px;;
    }
</style>