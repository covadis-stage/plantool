<script setup lang="ts">
import type { ProjectActivity } from '~/types/Activity';
import type { ActivityType } from '~/types/ActivityType';

defineProps<{
    activities: ProjectActivity[];
}>();

const formatActivityType = (activityType: ActivityType) => {
    return `${activityType.key} - ${activityType.description}\n\n${activityType.operationShortText}`;
};
</script>

<template>
    <DataTable
        :value="activities"
        sort-mode="multiple"
        removable-sort
        striped-rows
        paginator
        :rows="activities.length < 10 ? activities.length : 10"
        :rows-per-page-options="[10, 20, 50, activities.length]"
    >
        <Column field="network" header="Network" sortable show-clear-button></Column>
        <Column field="workBreakdownStructure" header="Work Breakdown Structure" sortable show-clear-button></Column>
        <Column field="id" header="Activity" sortable show-clear-button></Column>
        <Column field="latestStartDate" header="Latest Start Date" sortable show-clear-button></Column>
        <Column field="latestFinishDate" header="Latest Finish Date" sortable show-clear-button></Column>
        <Column field="originalFinishDate" header="Original Finish Date" sortable show-clear-button></Column>
        <Column field="timeEstimated" header="Time Estimated" sortable show-clear-button></Column>
        <Column field="timeSpent" header="Time Spent" sortable show-clear-button></Column>
        <Column field="teamLeader" header="Team Leader" sortable show-clear-button></Column>
        <Column field="activityType.key" header="Activity Type" sortable show-clear-button>
            <template #body="slotProps">
                <div class="activity-type">
                    <p>{{ slotProps.data.activityType.key }}</p>
                    <i class="pi pi-info-circle" v-tooltip.top="formatActivityType(slotProps.data.activityType)"></i>
                </div>
            </template>
        </Column>
    </DataTable>
</template>

<style lang="scss" scoped>
.activity-type {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 0.5rem;

    i {
        font-size: 0.8rem;
        color: var(--p-gray-400)
    }
}
</style>