import { useEngineerMapper } from "~/composables/mappers/engineerMapper";
import type { Engineer } from "~/types/Engineer";

export const useEngineerStore = defineStore("EngineerStore", () => {
    const engineerMapper = useEngineerMapper();
    const { loading, get } = useApi();

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

    return {
        loading,
        engineers,
        delegators,
        getEngineers,
    }
});