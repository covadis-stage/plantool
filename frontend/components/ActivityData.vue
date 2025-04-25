<script setup lang="ts">
import type { ProjectActivity } from '~/types/Activity';
import type { ActivityType } from '~/types/ActivityType';
import Popover from "primevue/popover";
import type { WorkCenter } from '~/types/WorkCenter';

const { formatDate } = useUtil();
const props = defineProps<{
    activities: ProjectActivity[];
}>();

const currentNetwork = ref<string | null>(null);
const popover = ref<InstanceType<typeof Popover>>();

const formatActivityType = (activityType: ActivityType) => {
    return `${activityType.key} - ${activityType.description}\n\n${activityType.operationShortText}`;
};

const formatWorkCenter = (workCenter: WorkCenter) => {
    return `${workCenter.key} = ${workCenter.readableName ?? 'No alternative name set'}`;
};

const networks = computed(() => {
    const networks = new Set<string>();
    props.activities.forEach((activity) => {
        if (activity.network) networks.add(activity.network);
    });
    return Array.from(networks).sort();
});

const openNetworkSelect = (event: MouseEvent, network: string) => {
    currentNetwork.value = network;
    popover.value?.toggle(event, event.currentTarget);
};

const scrollToNetwork = (network: string) => {
    const networkElement = document.querySelector(`.${getClassName(network)}`);
    if (!networkElement) return;
    networkElement.scrollIntoView({
        behavior: 'smooth',
        block: 'center',
    });
    popover.value?.hide();
};

const getClassName = (string: string | undefined) => {
    return (string?.replace(/[^a-zA-Z0-9]/g, '-').toLowerCase() || '');
}

const getActivityClasses = (activity: ProjectActivity) => {
    let classes = [getClassName(activity.network)];

    return classes.join(' ');
};
</script>

<template>
    <DataTable
        :value="activities"
        removable-sort
        striped-rows
        :row-class="(activity) => getActivityClasses(activity)"
        scrollable
        scroll-height="80dvh"
        sort-mode="single"
        sort-field="network"
        :sort-order="-1"
        row-group-mode="subheader"
        group-rows-by="network"
    >
        <Column field="network" header="Network"></Column>
        <Column field="workBreakdownStructure" header="WBS" style="width: 210px"></Column>
        <Column field="id" header="Activity" style="width: 80px"></Column>
        <Column field="latestStartDate" header="Latest Start Date" style="min-width: 150px" >
            <template #body="slotProps">
                <DateAssignment
                    :activity-key="slotProps.data.key"
                    :default-date="slotProps.data.latestStartDate"
                    :override-date="slotProps.data.actualStartDate"
                    assign-as="actualStartDate"
                ></DateAssignment>
            </template>
        </Column>
        <Column field="latestFinishDate" header="Latest Finish Date" style="min-width: 150px">
            <template #body="slotProps">
                <DateAssignment
                    :activity-key="slotProps.data.key"
                    :default-date="slotProps.data.latestFinishDate"
                    :override-date="slotProps.data.actualFinishDate"
                    assign-as="actualFinishDate"
                ></DateAssignment>
            </template>
        </Column>
        <Column field="originalFinishDate" header="Original Finish Date" style="min-width: 120px">
            <template #body="slotProps">
                {{ formatDate(slotProps.data.originalFinishDate) }}
            </template>
        </Column>
        <Column field="timeEstimated" header="Time Estimated" style="width: 100px"></Column>
        <Column field="timeSpent" header="Time Spent" style="width: 100px"></Column>
        <Column field="teamLeader" header="Team Leader" style="width: 200px"></Column>
        <Column field="workCenter.key" header="Work Center" style="width: 240px">
            <template #body="slotProps">
                <p v-tooltip.top="formatWorkCenter(slotProps.data.workCenter)" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                    {{ slotProps.data.workCenter.readableName ?? slotProps.data.workCenter.key }}
                </p>
            </template>
        </Column>
        <Column field="activityType.key" header="Activity Type" style="width: 90px">
            <template #body="slotProps">
                <div class="text-with-icon">
                    <p>{{ slotProps.data.activityType.key }}</p>
                    <i
                        class="pi pi-info-circle"
                        v-tooltip.top="formatActivityType(slotProps.data.activityType)"
                    ></i>
                </div>
            </template>
        </Column>
        <Column field="delegator" header="Delegator" style="width: 120px">
            <template #body="slotProps">
                <EngineerAssignment
                    :activity-key="slotProps.data.key"
                    assign-as="delegator"
                    :current="slotProps.data.delegator"
                >
                </EngineerAssignment>
            </template>
        </Column>
        <Column field="engineer" header="Engineer" style="width: 120px">
            <template #body="slotProps">
                <EngineerAssignment
                    :activity-key="slotProps.data.key"
                    assign-as="engineer"
                    :current="slotProps.data.engineer"
                >
                </EngineerAssignment>
            </template>
        </Column>
        <template #groupheader="slotProps">
            <Button
                :label="slotProps.data.network"
                variant="outlined"
                icon-pos="right"
                icon="pi pi-chevron-down"
                @click="openNetworkSelect($event, slotProps.data.network)"
            ></Button>
        </template>
    </DataTable>
    <Popover ref="popover">
        <div class="network-list">
            <Button
                v-for="network in networks"
                :key="network"
                :label="network"
                style="flex: 1 0 auto"
                severity="secondary"
                @click="scrollToNetwork(network)"
            ></Button>
        </div>
    </Popover>
</template>

<style lang="scss" scoped>
.text-with-icon {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 0.5rem;

    i {
        font-size: 0.8rem;
        color: var(--p-gray-400)
    }
}


.network-list {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    max-height: 300px;
    overflow-y: auto;
}
</style>