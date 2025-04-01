<script setup lang="ts">
import type { TreeNode } from 'primevue/treenode';

defineProps<{
    treeNode: TreeNode
}>()

</script>

<template>
    <ProjectData v-if="!treeNode.leaf" :project="treeNode.data" />
    <ActivityData v-else :activities="treeNode.data" />
</template>

<style lang="scss" scoped>

</style>