<script setup lang="ts">
import type { TreeNode } from 'primevue/treenode';
import type { Project } from '~/types/Project';

const { projects, loadingProjects, getProjects } = useProjects();
const { getActivitiesForProject } = useActivities();
const treeNodes = ref<TreeNode[]>([]);
const loadingMethod = ref<"mask" | "icon">("mask");

const onNodeExpand = async (node: TreeNode) => {
    console.log("Node expanded", node.data);
    if (node.data && node.children) return;
    if (!node.data) return;
    const project = node.data as Project;
    node.loading = true;
    const activities = await getActivitiesForProject(project.key);
    const activitiesNode = {
        key: `${project.key}-activities`,
        label: project.customer,
        data: activities,
        leaf: true,
    } as TreeNode;
    node.children = [activitiesNode];
    node.loading = false;
}

onMounted(() => {
    (async () => {
        await getProjects();
        loadingMethod.value = "icon";
    })();
});

watchEffect(() => {
    treeNodes.value = projects.value.map((project) => {
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
        :loading="loadingProjects"
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