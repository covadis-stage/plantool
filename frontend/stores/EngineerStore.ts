import { useEngineerMapper } from "~/composables/mappers/engineerMapper";
import type { Engineer } from "~/types/Engineer";

export const useEngineerStore = defineStore("EngineerStore", () => {
    const engineerMapper = useEngineerMapper();
    const { loading, get, put, del } = useApi();

    const engineers = ref<Engineer[]>([]);
    const delegators = computed(() => {
        return engineers.value.filter(engineer => engineer.canDelegate);
    });

    const getEngineers = async () => {
        if (loading.value) return [];
        if (engineers.value.length > 0) return engineers.value;

        try {
            const response = await get('Engineers');
            engineers.value = engineerMapper.mapEngineers(response);
            if (!response) return [];
            return engineers.value;
        } catch (err) {
            console.error(err);
            return [];
        }
    }

    const setDelegatorOnActivities = async (activities: string[], delegatorId: string) => {
        try {
            await put('Activities/bulk-update', {
                activityKeys: activities,
                delegatorId: delegatorId,
            })
            return true
        } catch (err) {
            console.error(err);
            return false
        }
    }

    const setEngineerOnActivities = async (activities: string[], engineerId: string) => {
        try {
            await put('Activities/bulk-update', {
                activityKeys: activities,
                engineerId: engineerId,
            })
            return true
        } catch (err) {
            console.error(err);
            return false
        }
    }

    const bulkDelete = async (activities: string[], toDelete: "delegator" | "engineer") => {
        try {
            await del('Activities/bulk-delete', {
                activityKeys: activities,
                [toDelete]: true,
            })
            return true
        } catch (err) {
            console.error(err);
            return false
        }
    }


    return {
        loading,
        engineers,
        delegators,
        getEngineers,
        setDelegatorOnActivities,
        setEngineerOnActivities,
        bulkDelete,
    }
});