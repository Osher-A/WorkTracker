<template>
    <div class="header" :style="{width: width}">
      <h1>{{ title }}</h1>
    </div>
</template>
<script>
    export default {
        name: 'ClientManager',
        props: {
            title: {
                type: String,
                required: true
            },
            width: {
                type: String,
                default: '100%'
            }
        },
    };
</script>

<style scoped>
h1 {
    margin-top: 15%;
    margin-bottom: 10%;
    color: grey;
    text-align: center;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}
</style>