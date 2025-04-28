<script setup lang="ts">
const props = defineProps<{
    activityKey: string;
    defaultDate?: Date;
    overrideDate?: Date;
    assignAs: "actualFinishDate" | "actualStartDate";
}>();

const { formatDate } = useUtil();
const activityStore = useActivityStore();
const projectStore = useProjectStore();
const toastService = useToast();

const isLoading = ref(false);
const date = ref<Date | undefined>(props.overrideDate || props.defaultDate);
const error = ref<boolean>(false);

const onChange = async (value: Date | Date[] | (Date | null)[] | null | undefined) => {
    if (Array.isArray(value)) return;
    if (!(value instanceof Date) && value != null && value != undefined) return;
    if ((value == null || value == undefined) && props.overrideDate == null) {
        // Wait for the next tick to ensure date is synced correctly
        // then set date back to the default date to prevent clearing the input
        await new Promise((resolve) => setTimeout(resolve, 0));
        date.value = props.defaultDate;
        return;
    };

    isLoading.value = true;
    error.value = false;
    let result = false;
    if (value == null || value == undefined) {
        value = undefined;
        result = await activityStore.bulkDelete([props.activityKey], props.assignAs);
    } else if (props.assignAs === "actualFinishDate") {
        result = await activityStore.setActualFinishDateOnActivities([props.activityKey], value);
    } else if (props.assignAs === "actualStartDate") {
        result = await activityStore.setActualStartDateOnActivities([props.activityKey], value);
    }

    if (result) {
        projectStore.updateLocalActivityState(props.activityKey, props.assignAs, value);
        date.value = value || props.defaultDate;
    } else {
        error.value = true;
        toastService.add({
            severity: "error",
            summary: "Date not set",
            detail: "Couldn't set the date, check your network and try again",
            life: 4000,
        })
    }

    isLoading.value = false;
};
</script>

<template>
    <DatePicker
        class="date-assignment"
        :class="{ 'overridden-date': overrideDate }"
        v-model="date"
        size="small"
        show-button-bar
        v-tooltip.top="overrideDate ? 'SAP: ' + formatDate(defaultDate) : ''"
        @value-change="onChange"
        :show-icon="isLoading"
        icon-display="input"
        icon="pi pi-spin pi-spinner"
        :invalid="error"
    >
        <template #date="slotProps">
            <span
                v-if="overrideDate
                    && slotProps.date.year == defaultDate?.getFullYear()
                    && slotProps.date.month == defaultDate?.getMonth()
                    && slotProps.date.day == defaultDate?.getDate()"
                class="sap-selected-date"
            >
                {{ slotProps.date.day }}
            </span>
            <template v-else>{{ slotProps.date.day }}</template>
        </template>
    </DatePicker>
</template>

<style lang="scss">
.overridden-date {
    .p-inputtext {
        background-color: var(--p-orange-100) !important;
    }
}

.date-assignment {
    .p-inputtext {
        padding-right: 25px !important;
    }
}

.sap-selected-date {
    font-weight: 700;
    font-style: italic;
    text-decoration: underline;
    padding: 1rem;
}
</style>