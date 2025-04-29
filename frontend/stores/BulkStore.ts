import type { ProjectActivity } from "~/types/Activity";

export const useBulkStore = defineStore("BulkStore", () => {
    const activityStore = useActivityStore();
    const projectStore = useProjectStore();
    const toast = useToast();

    const firstSelected = ref<{ key: keyof ProjectActivity, value: ProjectActivity[keyof ProjectActivity], activity: ProjectActivity } | undefined>(undefined);
    const selectedActivities = ref<ProjectActivity[]>([]);
    const isSelecting = computed(() => firstSelected.value !== undefined);

    const setFirstSelected = <K extends keyof ProjectActivity>(key: K, value: ProjectActivity[K], activity: ProjectActivity) => {
        firstSelected.value = { key, value, activity };
        selectedActivities.value = [activity];
    }

    const hoverActivity = (activity: ProjectActivity) => {
        if (!isSelecting.value) return;
        if (!selectedActivities.value.find(a => a.key === activity.key)){ 
            selectedActivities.value = [...selectedActivities.value, activity];
        } else {
            // detect backward selection (remove)
            const index = selectedActivities.value.findIndex(a => a.key === activity.key);
            if (index === -1) return;
            if (index === selectedActivities.value.length - 1) return;
            // remove all activities after the selected one
            selectedActivities.value = selectedActivities.value.slice(0, index + 1);
        }
    }

    const stopSelecting = async () => {
        await applySelection();
        reset();
    }

    const applySelection = async () => {
        if (!firstSelected.value) return;
        if (selectedActivities.value.length <= 1) return;

        let result = await activityStore.setOnActivities(selectedActivities.value, firstSelected.value.key, firstSelected.value.value)
        if (result) {
            for (const activity of selectedActivities.value) {
                projectStore.updateLocalActivityState(activity.key, firstSelected.value.key, firstSelected.value.value);
            }
        } else {
            toast.add({
                summary: "Couldn't apply changes",
                detail: "There was an error applying the changes to the selected activities.",
                severity: "error",
                life: 5000,
            })
        }
    }

    const reset = () => {
        firstSelected.value = undefined;
        selectedActivities.value = [];
    }

    const isActivityLoading = (activityKey: string, column: keyof ProjectActivity) => {
        if (!activityStore.loading.value) return false;
        if (!firstSelected.value) return false;
        if (firstSelected.value.key !== column) return false;
        if (!selectedActivities.value.find(a => a.key === activityKey)) return false;
        return true;
    }


    return {
        loading: computed(() => activityStore.loading),
        firstSelected,
        selectedActivities,
        isSelecting,
        setFirstSelected,
        hoverActivity,
        stopSelecting,
        isActivityLoading,
    }
});