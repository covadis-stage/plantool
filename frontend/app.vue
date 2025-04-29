<script setup lang="ts">
const bulkStore = useBulkStore();
onMounted(() => {
    window.addEventListener('mouseup', (event) => {
        bulkStore.stopSelecting();
    });
});
</script>

<template>
    <Toast></Toast>
    <NuxtLayout>
        <NuxtPage />
    </NuxtLayout>
</template>
