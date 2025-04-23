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
    if (props.assignAs === "delegator") {
        await engineerStore.setDelegatorOnActivities([props.activityKey], changeEvent.value);
    } else if (props.assignAs === "engineer") {

    }
}

onMounted(() => {
    engineerStore.getEngineers()
})
</script>

<template>
    <Select
        :options="engineerStore.delegators"
        option-value="id"
        option-label="name"
        :placeholder="'Assign ' + props.assignAs"
        :loading="engineerStore.loading"
        :default-value="props.current?.id"
        filter
        @change="onChange"
    >

    </Select>
</template>

<style lang="scss" scoped></style>