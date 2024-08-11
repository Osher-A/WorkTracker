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
                default: '50%'
            }
        }
    };
</script>
<style scoped>
    .container {
        display: flex;
        flex-direction:  column;
        justify-content: center; 
        align-items: center; 
        margin-top: 60px; /* The height of the header */
    }
</style>