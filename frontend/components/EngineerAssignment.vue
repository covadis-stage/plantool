<script setup lang="ts">
import type { SelectChangeEvent } from 'primevue';
import type { Engineer } from '~/types/Engineer';

const engineerStore = useEngineerStore();
const props = defineProps<{
    activityKey: string;
    assignAs: "delegator" | "engineer";
    current: Engineer | undefined;
}>();

const previousValue = ref<string>(props.current?.id ?? '');

const onChange = async (changeEvent: SelectChangeEvent) => {
    if (changeEvent.value == previousValue.value) return;

    if (changeEvent.value == null) {
        await engineerStore.bulkDelete([props.activityKey], props.assignAs);
    } else if (props.assignAs === "delegator") {
        await engineerStore.setDelegatorOnActivities([props.activityKey], changeEvent.value);
    } else if (props.assignAs === "engineer") {
        await engineerStore.setEngineerOnActivities([props.activityKey], changeEvent.value);
    }
    previousValue.value = changeEvent.value;
}

onMounted(() => {
    engineerStore.getEngineers()
})
</script>

<template>
    <Select
        class="engineer-assignment"
        :options="assignAs === 'delegator' ? engineerStore.delegators : engineerStore.engineers"
        option-value="id"
        option-label="name"
        :placeholder="'Assign ' + props.assignAs"
        :loading="engineerStore.loading"
        :default-value="props.current?.id"
        filter
        show-clear
        @change="onChange"
        style="width: 200px"
        size="small"
    >

    </Select>
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
</style>