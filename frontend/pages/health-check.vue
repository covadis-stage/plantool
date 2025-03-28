<script setup lang="ts">
import { useHealthCheck } from '~/composables/useHealthCheck';

const { isHealthy, error, checkHealth } = useHealthCheck();
</script>

<template>
    <h1>Health Check</h1>
    <button @click="checkHealth">Check Health</button>
    <div v-if="isHealthy">The system is healthy!</div>
    <div v-else-if="error">{{ error }}</div>
</template>

<style lang="scss" scoped>

</style>