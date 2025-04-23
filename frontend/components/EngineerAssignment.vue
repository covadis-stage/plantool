<script setup lang="ts">
import type { SelectChangeEvent } from 'primevue';
import type { Engineer } from '~/types/Engineer';

const engineerStore = useEngineerStore();
const props = defineProps<{
    activityKey: string;
    assignAs: "delegator" | "engineer";
    current: Engineer | undefined;
}>();

const onChange = async (changeEvent: SelectChangeEvent) => {
    if (changeEvent.value == null) {
        await engineerStore.bulkDelete([props.activityKey], props.assignAs);
        return;
    }
    if (props.assignAs === "delegator") {
        await engineerStore.setDelegatorOnActivities([props.activityKey], changeEvent.value);
    } else if (props.assignAs === "engineer") {
        await engineerStore.setEngineerOnActivities([props.activityKey], changeEvent.value);
    }
}

onMounted(() => {
    engineerStore.getEngineers()
})
</script>

<template>
    <Select
        :options="assignAs === 'delegator' ? engineerStore.delegators : engineerStore.engineers"
        option-value="id"
        option-label="name"
        :placeholder="'Assign ' + props.assignAs"
        :loading="engineerStore.loading"
        :default-value="props.current?.id"
        filter
        show-clear
        @change="onChange"
    >

    </Select>
</template>

<style lang="scss" scoped></style>