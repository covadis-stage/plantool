<script setup lang="ts">
const projectStore = useProjectStore();

onMounted(() => {
    (async () => {
        await projectStore.getProjects();
    })();
});

</script>

<template>
    <div
        class="loading"
        v-if="projectStore.loading && projectStore.projects.length === 0"
    >
        <i class="pi pi-spin pi-spinner"></i>
    </div>
    <VirtualScroller
        v-else-if="projectStore.projects.length > 0"
        :items="projectStore.projects"
        :itemSize="50"
        scroll-height="100%"
    >
        <template v-slot:item="{ item }">
            <ProjectData :project="item"></ProjectData>
        </template>
    </VirtualScroller>
</template>

<style lang="scss" scoped>
.loading {
    position: fixed;
    top: 0;
    left: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100dvw;
    height: 100dvh;
    pointer-events: none;
}

i {
    font-size: 2rem;
}
</style>