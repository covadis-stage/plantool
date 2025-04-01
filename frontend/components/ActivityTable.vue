<script setup lang="ts">
import type { TreeNode } from 'primevue/treenode';
import type { Project } from '~/types/Project';

const projectStore = useProjectStore();
const treeNodes = ref<TreeNode[]>([]);
const loadingMethod = ref<"mask" | "icon">("mask");

const onNodeExpand = async (node: TreeNode) => {
    if (node.data && node.children) return;
    if (!node.data) return;
    node.loading = true;

    const project = node.data as Project;
    const activities = await projectStore.getActivitiesForProject(project.key);
    const activitiesNode = {
        key: `${project.key}-activities`,
        label: project.customer,
        data: activities,
        leaf: true,
    } as TreeNode;

    // Add the activities in 1 node so the component can decide how to display them
    node.children = [activitiesNode];
    node.loading = false;
}

onMounted(() => {
    (async () => {
        await projectStore.getProjects();
        loadingMethod.value = "icon";
    })();
});

watchEffect(() => {
    treeNodes.value = projectStore.projects.map((project) => {
        return {
            key: project.key,
            label: project.customer,
            data: project,
            leaf: false,
        } as TreeNode;
    });
});
</script>

<template>
    <Tree
        :value="treeNodes"
        @node-expand="onNodeExpand"
        :loading="projectStore.loading"
        :loading-mode="loadingMethod"
        class="activity-table"
    >
        <template #default="{ node }">
            <ProjectOrActivityTreeNode :treeNode="node" />
        </template>
    </Tree>
</template>

<style lang="scss">
.activity-table {
    .p-tree-node-content {
        display: flex;
    }

    .p-tree-node-content .p-tree-node-label {
        flex: 1;
    }
}
</style>

<style lang="scss" scoped></style>