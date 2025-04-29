<script setup lang="ts">
import type { ProjectActivity } from '~/types/Activity';

const props = defineProps<{
    activity: ProjectActivity;
    keyOfActivity: keyof ProjectActivity;
    value: ProjectActivity[keyof ProjectActivity];
}>();

const bulkStore = useBulkStore();

const onMouseDown = (event: MouseEvent) => {
    event.preventDefault();
    bulkStore.setFirstSelected(props.keyOfActivity, props.value, props.activity);
};
</script>

<template>
    <div
        ref="bulkActionShortcut"
        class="bulk-action-shortcut"
        :class="{   'mouse-down': bulkStore.isSelecting && bulkStore.firstSelected?.key === keyOfActivity,
                    'is-first-selected': bulkStore.isSelecting && bulkStore.firstSelected?.key === keyOfActivity && bulkStore.firstSelected?.activity.key === props.activity.key,
                    'selected': bulkStore.isSelecting && bulkStore.firstSelected?.key === keyOfActivity && bulkStore.selectedActivities.some((a) => a.key === props.activity.key) }"
        @mousedown="onMouseDown"
    >
        <i class="pi pi-plus"></i>
    </div>
</template>

<style lang="scss" scoped>
.bulk-action-shortcut {
    border-radius: 50%;
    background-color: rgba(0, 0, 0, 0);
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 1.3rem;
    min-width: 1.3rem;
    max-height: 1.3rem;
    max-width: 1.3rem;

    i {
        font-size: 0.7rem;
    }

    &:hover {
        background-color: var(--p-blue-100);
        cursor: pointer;
    }

    &.mouse-down {
        background-color: var(--p-blue-100);
    }

    &.selected {
        background-color: var(--p-blue-500);
        i {
            color: #fff;
        }
    }

    &.is-first-selected {
        background-color: var(--p-blue-200) !important;
        i {
            color: #fff;
        }
    }
}
</style>