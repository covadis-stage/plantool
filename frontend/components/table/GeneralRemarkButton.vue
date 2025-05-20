<script setup lang="ts">
import type { ProjectActivity } from '~/types/Activity';

const activityStore = useActivityStore();
const projectStore = useProjectStore();
const toast = useToast();

const props = defineProps<{
    activity: ProjectActivity;
    remark?: string;
}>();

const isVisible = ref(false);
const isLoading = ref(false);
const writtenRemark = ref<string | undefined>(props.remark);

const show = () => isVisible.value = true;

const save = async () => {
    if (writtenRemark.value?.trim() === props.remark) return;
    if ((writtenRemark.value?.trim() === '' || writtenRemark.value == undefined) && props.remark == undefined) return;
    if (writtenRemark.value?.trim() === '' || writtenRemark.value == null) writtenRemark.value = undefined;
    isLoading.value = true;

    let result = await activityStore.setGeneralRemark(props.activity.key, writtenRemark.value?.trim());
    let message = "Remark saved";
    if (!result) message = "Could not save remark, please try again";
    if (result && writtenRemark.value == undefined) message = "Remark removed successfully";
    toast.add({
        severity: result ? 'success' : 'error',
        summary: result ? 'Success' : 'Error',
        detail: message,
        life: 3000,
    })

    if (result) projectStore.updateLocalActivityState(props.activity.key, "generalRemark", writtenRemark.value);

    isLoading.value = false;
}

watchEffect(() => {
    if (props.remark) writtenRemark.value = props.remark;
});
</script>

<template>
    <Button
        :class="{ 'remark-button-active': remark && remark.trim() !== '', 'remark-button': !remark || remark.trim() === '' }"
        :icon="isLoading ? 'pi pi-spin pi-spinner' : 'pi pi-comment'"
        :rounded="!isVisible"
        :raised="isVisible"
        @click="show"
    />

    <Dialog
        v-model:visible="isVisible"
        modal
        header="General remark"
        @hide="save"
    >
        <div class="remark-project-information">
            <div class="remark-project-information-property">
                <p class="header">Network</p>
                <p class="text">{{ activity.network }}</p>
            </div>
            <div class="remark-project-information-property">
                <p class="header">WBS</p>
                <p class="text">{{ activity.workBreakdownStructure }}</p>
            </div>
            <div class="remark-project-information-property">
                <p class="header">Activity</p>
                <p class="text">{{ activity.id }}</p>
            </div>
        </div>
        <Textarea
            v-model="writtenRemark"
            rows="5"
            cols="50"
        ></Textarea>
        <div class="close-to-save-wrap">
            <small class="close-to-save">Close to save</small>
        </div>
    </Dialog>
</template>

<style lang="scss">
.remark-project-information {
    display: flex;
    flex-direction: row;
    gap: 2rem;
    margin-bottom: 2rem;

    .remark-project-information-property {
        display: flex;
        flex-direction: column;

        .header {
            font-weight: 600;
            font-size: 0.85rem;
            letter-spacing: 0.7px;
            color: var(--p-neutral-400);
        }

        .text {
            font-weight: 400;
            font-size: 1.2rem;
            line-height: 1.2rem;
        }
    }
}

.close-to-save-wrap {
    display: flex;
    justify-content: center;
    margin-top: 1rem;

    .close-to-save {
        font-size: 0.8rem;
        color: var(--p-gray-400);
        font-weight: 600;
        text-align: center;
    }
}

.remark-button-active {
    background-color: var(--p-orange-500);
    border: 1px solid var(--p-orange-500);
}

.remark-button-active:not(:disabled):hover {
    background-color: var(--p-orange-600);
    border: 1px solid var(--p-orange-600);
}

.remark-button-active:not(:disabled):active {
    background-color: var(--p-orange-700);
    border: 1px solid var(--p-orange-700);
}

.remark-button {
    background-color: var(--p-orange-100);
    border: 1px solid var(--p-orange-100);
    color: var(--p-orange-500);
}

.remark-button:not(:disabled):hover {
    background-color: var(--p-orange-200);
    border: 1px solid var(--p-orange-200);
}

.remark-button:not(:disabled):active {
    background-color: var(--p-orange-300);
    border: 1px solid var(--p-orange-300);
}
</style>