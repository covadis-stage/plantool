<script setup lang="ts">
import type { Project } from '~/types/Project';

defineProps<{
    project: Project
}>()
</script>

<template>
    <div class="project-data">
        <p class="customer">{{ project.customer }}</p>
        <p>{{ project.key }}</p>
    </div>
</template>

<style lang="scss" scoped>
.project-data {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;

    .customer {
        font-weight: bold;
        font-size: 1.2rem;
    }

    p {
        margin: 12px 0px;
    }
}
</style>