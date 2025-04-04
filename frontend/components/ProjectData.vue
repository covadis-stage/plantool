<script setup lang="ts">
import type { Project } from '~/types/Project';

const props = defineProps<{
    project: Project
}>()

const projectStore = useProjectStore();
const isOpen = ref<boolean | null>(false);

const toggleOpen = () => {
    if (isOpen.value === null) return;
    if (isOpen.value === false) {
        open();
    } else if (isOpen.value === true) {
        close();
    }
}

const open = async () => {
    isOpen.value = null;
    await projectStore.getActivitiesForProject(props.project.key);
    isOpen.value = true;
}

const close = () => {
    isOpen.value = false;
}

</script>

<template>
    <Container>
    <div class="project-data" @click="toggleOpen">
        <div class="left-side">
            <i v-if="isOpen === false" class="pi pi-chevron-right"></i>
            <i v-else-if="isOpen === true" class="pi pi-chevron-down"></i>
            <i v-else class="pi pi-spin pi-spinner"></i>
            <p class="customer">{{ project.customer }}</p>
        </div>
        <p>{{ project.key }}</p>
    </div>
    <ActivityData v-if="isOpen" :activities="project.activities" />
    </Container>
</template>

<style lang="scss" scoped>
.project-data {
    width: 100%;
    height: 50px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-radius: 8px;
    padding: 0 16px;
    user-select: none;

    .left-side {
        display: flex;
        align-items: center;
        gap: 16px;
    }

    .customer {
        font-weight: bold;
        font-size: 1.2rem;
    }

    i {
        color: var(--p-neutral-400);
    }

    p {
        margin: 0;
    }
}

.project-data:hover {
    background-color: var(--p-neutral-100);
    cursor: pointer;
}

.project-data:active {
    background-color: var(--p-neutral-200);
}
</style>