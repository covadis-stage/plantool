<script setup lang="ts">

</script>

<template>
    <div class="layout-default">
        <slot></slot>
    </div>
</template>

<style lang="scss" scoped>
.layout-default {
    height: 100%;
    display: flex;
    flex-direction: column;
}
</style>