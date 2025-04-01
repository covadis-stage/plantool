import type { ActivityType } from "~/types/ActivityType";
import { useActivityMapper } from "./mappers/activityMapper";

export const useActivities = () => {
    const { loading, get, post } = useApi();
    const activityMapper = useActivityMapper();

    const activities = ref<ActivityType[]>([]);

    const getActivitiesForProject = async (projectKey: string) => {
        try {
            const response = await get(`Activities/project/${projectKey}`);
            if (!response) return [];
            activities.value = activityMapper.mapActivities(response);
            return activities.value;
        } catch (err) {
            console.error(err);
            return [];
        }
    }

    return {
        loadingActivities: loading,
        activities,
        getActivitiesForProject,
    }
}