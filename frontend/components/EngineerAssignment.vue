<script setup lang="ts">
import type { SelectChangeEvent } from 'primevue';
import type { ProjectActivity } from '~/types/Activity';
import type { Engineer } from '~/types/Engineer';

const engineerStore = useEngineerStore();
const activityStore = useActivityStore();
const bulkStore = useBulkStore();

const props = defineProps<{
    activity: ProjectActivity;
    assignAs: "delegator" | "engineer";
    current: Engineer | undefined;
}>();
const isLoading = ref(false);

const engineerId = ref<string>(props.current?.id ?? '');
const selectedEngineer = computed(() => {
    if (props.assignAs === "delegator") {
        return engineerStore.delegators.find((engineer) => engineer.id === engineerId.value);
    } else if (props.assignAs === "engineer") {
        return engineerStore.engineers.find((engineer) => engineer.id === engineerId.value);
    }
    return null;
});

const onChange = async (changeEvent: SelectChangeEvent) => {
    isLoading.value = true;

    if (changeEvent.value == null) {
        await activityStore.bulkDelete([props.activity.key], props.assignAs);
    } else if (props.assignAs === "delegator") {
        await activityStore.setDelegatorOnActivities([props.activity.key], changeEvent.value);
    } else if (props.assignAs === "engineer") {
        await activityStore.setEngineerOnActivities([props.activity.key], changeEvent.value);
    }

    isLoading.value = false;
    engineerId.value = changeEvent.value;
}

onMounted(() => {
    engineerStore.getEngineers()
})

watchEffect(() => {
    if (props.current) {
        engineerId.value = props.current.id;
    } else {
        engineerId.value = '';
    }
});
</script>

<template>
    <div class="engineer-assignment-wrapper">
        <BulkActionShortcut
            :activity="props.activity"
            :keyOfActivity="props.assignAs"
            :value="selectedEngineer ?? undefined"
        ></BulkActionShortcut>
        <Select
            class="engineer-assignment"
            :options="assignAs === 'delegator' ? engineerStore.delegators : engineerStore.engineers"
            option-value="id"
            option-label="name"
            :placeholder="'Assign ' + props.assignAs"
            :loading="isLoading || bulkStore.isActivityLoading(props.activity.key, props.assignAs)"
            v-model="engineerId"
            filter
            show-clear
            @change="onChange"
            style="width: 200px"
            size="small"
        ></Select>
    </div>
</template>

<style lang="scss">
.p-select.engineer-assignment {
    .p-select-clear-icon {
        display: none;
    }

    .p-select-label {
        padding-inline-end: 0;
    }
}

.p-select.engineer-assignment:hover {
    .p-select-clear-icon {
        display: block;
    }

    .p-select-label {
        padding-inline-end: 28px;
    }
}

.engineer-assignment-wrapper {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 0.25rem;
}
</style>