<script setup lang="ts">
import type { ProjectActivity } from '~/types/Activity';
import type { ActivityType } from '~/types/ActivityType';
import Popover from "primevue/popover";
import type DataTable from 'primevue/datatable';

const props = defineProps<{
    activities: ProjectActivity[];
}>();

const currentNetwork = ref<string | null>(null);
const popover = ref<InstanceType<typeof Popover>>();

const formatActivityType = (activityType: ActivityType) => {
    return `${activityType.key} - ${activityType.description}\n\n${activityType.operationShortText}`;
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
    return string?.replace(/[^a-zA-Z0-9]/g, '-').toLowerCase() || '';
}

const columnStyle = {
};
</script>

<template>
    <DataTable
        :value="activities"
        removable-sort
        striped-rows
        :row-class="(activity) => getClassName(activity.network)"
        scrollable
        scroll-height="80dvh"
        sort-mode="single"
        sort-field="network"
        :sort-order="-1"
        row-group-mode="subheader"
        group-rows-by="network"
    >
        <Column field="network" header="Network" :style="columnStyle"></Column>
        <Column field="workBreakdownStructure" header="WBS" :style="columnStyle"></Column>
        <Column field="id" header="Activity" :style="columnStyle"></Column>
        <Column field="latestStartDate" header="Latest Start Date" :style="columnStyle"></Column>
        <Column field="latestFinishDate" header="Latest Finish Date" :style="columnStyle"></Column>
        <Column field="originalFinishDate" header="Original Finish Date" :style="columnStyle"></Column>
        <Column field="timeEstimated" header="Time Estimated" :style="columnStyle"></Column>
        <Column field="timeSpent" header="Time Spent" :style="columnStyle"></Column>
        <Column field="teamLeader" header="Team Leader" :style="columnStyle"></Column>
        <Column field="activityType.key" header="Activity Type" :style="columnStyle">
            <template #body="slotProps">
                <div class="activity-type">
                    <p>{{ slotProps.data.activityType.key }}</p>
                    <i
                        class="pi pi-info-circle"
                        v-tooltip.top="formatActivityType(slotProps.data.activityType)"
                    ></i>
                </div>
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

.network-list {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    max-height: 300px;
    overflow-y: auto;
}
</style>