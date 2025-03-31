<script setup lang="ts">
const { projects, loading, getProjects } = useProjects();

onMounted(() => {
    getProjects()
})
</script>

<template>
    <DataTable :value="projects" :loading="loading" show-gridlines striped-rows>
        <Column header="Project" field="customer" />
    </DataTable>
</template>

<style lang="scss" scoped></style>