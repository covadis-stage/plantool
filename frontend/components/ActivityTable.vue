<script setup lang="ts">
const projectStore = useProjectStore();

onMounted(() => {
    (async () => {
        await projectStore.getProjects();
    })();
});

</script>

<template>
    <div class="loading" v-if="projectStore.loading">
        <i class="pi pi-spin pi-spinner"></i>
    </div>
    <ProjectData
        v-for="project in projectStore.projects"
        :key="project.key"
        :project="project"
    >
    </ProjectData>
</template>

<style lang="scss" scoped>
.loading {
    position: fixed;
    top: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100dvw;
    height: 100dvh;
}
i {
    font-size: 2rem;
}
</style>