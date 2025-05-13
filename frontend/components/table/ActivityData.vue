<script setup lang="ts">
import type { ProjectActivity } from '~/types/Activity';
import type { ActivityType } from '~/types/ActivityType';
import type { WorkCenter } from '~/types/WorkCenter';
import Popover from "primevue/popover";

const { formatDate } = useUtil();
const bulkStore = useBulkStore();
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
    let classes = [
        getClassName(activity.network),
        getClassName('KEY:' + activity.key),
    ];

    return classes.join(' ');
};

onMounted(() => {
    props.activities.forEach((activity) => {
        const element = document.querySelector<HTMLElement>(`.${getClassName('KEY:' + activity.key)}`);
        if (element) element.addEventListener('mouseenter', () => {
            bulkStore.hoverActivity(activity);
        })
    });
})
</script>

<template>
    <DataTable
        :value="activities"
        striped-rows
        :row-class="(activity) => getActivityClasses(activity)"
        scrollable
        scroll-height="80dvh"
        sort-mode="single"
        sort-field="network"
        :sort-order="-1"
        row-group-mode="subheader"
        group-rows-by="network"
        :class="{ 'disable-selection': bulkStore.isSelecting }"
    >
        <Column
            field="network"
            header="Network"
        ></Column>
        <Column
            field="generalRemark"
            header="Note"
            frozen
        >
            <template #body="slotProps">
                <GeneralRemarkButton
                    :activity="slotProps.data"
                    :remark="slotProps.data.generalRemark"
                ></GeneralRemarkButton>
            </template>
        </Column>
        <Column
            field="workBreakdownStructure"
            header="WBS"
            style="min-width: 200px"
        ></Column>
        <Column
            field="id"
            header="Activity"
            style="min-width: 90px"
        ></Column>
        <Column
            field="latestStartDate"
            header="Latest Start Date"
            style="min-width: 180px"
        >
            <template #body="slotProps">
                <DateAssignment
                    :activity="slotProps.data"
                    :default-date="slotProps.data.latestStartDate"
                    :override-date="slotProps.data.actualStartDate"
                    assign-as="actualStartDate"
                ></DateAssignment>
            </template>
        </Column>
        <Column
            field="latestFinishDate"
            header="Latest Finish Date"
            style="min-width: 180px"
        >
            <template #body="slotProps">
                <DateAssignment
                    :activity="slotProps.data"
                    :default-date="slotProps.data.latestFinishDate"
                    :override-date="slotProps.data.actualFinishDate"
                    assign-as="actualFinishDate"
                ></DateAssignment>
            </template>
        </Column>
        <Column
            field="originalFinishDate"
            header="Original Finish Date"
            style="min-width: 120px"
        >
            <template #body="slotProps">
                {{ formatDate(slotProps.data.originalFinishDate) }}
            </template>
        </Column>
        <Column
            field="timeEstimated"
            header="Time Estimated"
            style="min-width: 130px"
        >
            <template #body="slotProps">
                <ReadableTimeSpan :time-span="slotProps.data.timeEstimated"></ReadableTimeSpan>
            </template>
        </Column>
        <Column
            field="timeSpent"
            header="Time Spent"
            style="min-width: 130px"
        >
            <template #body="slotProps">
                <ReadableTimeSpan :time-span="slotProps.data.timeSpent"></ReadableTimeSpan>
            </template>
        </Column>
        <Column
            field="teamLeader"
            header="Team Leader"
            style="min-width: 200px"
        ></Column>
        <Column
            field="workCenter.key"
            header="Work Center"
        >
            <template #body="slotProps">
                <p
                    v-tooltip.top="formatWorkCenter(slotProps.data.workCenter)"
                    style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;"
                >
                    {{ slotProps.data.workCenter.readableName ?? slotProps.data.workCenter.key }}
                </p>
            </template>
        </Column>
        <Column
            field="activityType.key"
            header="Activity Type"
            style="min-width: 90px"
        >
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
        <Column
            field="delegator"
            header="Delegator"
            style="min-width: 260px"
        >
            <template #body="slotProps">
                <EngineerAssignment
                    :activity="slotProps.data"
                    assign-as="delegator"
                    :current="slotProps.data.delegator"
                >
                </EngineerAssignment>
            </template>
        </Column>
        <Column
            field="engineer"
            header="Engineer"
            style="min-width: 260px"
        >
            <template #body="slotProps">
                <EngineerAssignment
                    :activity="slotProps.data"
                    assign-as="engineer"
                    :current="slotProps.data.engineer"
                >
                </EngineerAssignment>
            </template>
        </Column>
        <template #groupheader="slotProps">
            <div class="group-header-wrapper">
                <div class="network">
                    <p class="network-name">Network</p>
                    <Button
                        :label="slotProps.data.network"
                        variant="outlined"
                        icon-pos="right"
                        icon="pi pi-chevron-down"
                        severity="secondary"
                        size="small"
                        @click="openNetworkSelect($event, slotProps.data.network)"
                    ></Button>
                </div>
            </div>
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

.disable-selection {
    user-select: none;
}

.group-header-wrapper {
    display: flex;
    justify-content: space-between;
    align-items: center;

    .network .network-name {
        font-size: 0.7rem;
        color: var(--p-gray-400);
        font-weight: 700;
        margin-bottom: 0.2rem;
        margin-left: 0.25rem;
        text-transform: uppercase;
        letter-spacing: 1px;
    }

}
</style>