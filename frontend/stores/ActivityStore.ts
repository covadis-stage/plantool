import type { ProjectActivity } from "~/types/Activity";
import type { Engineer } from "~/types/Engineer";

export const useActivityStore = defineStore("ActivityStore", () => {
    const api = useApi();
    const { getDateAsISOString } = useUtil();

    const setGeneralRemark = async (activityKey: string, remark: string | undefined) => {
        try {
            await api.put(`Activities/${activityKey}/general-remark?generalRemark=${remark}`);
            return true
        } catch (err) {
            console.error(err);
            return false
        }
    }

    // BULK ACTIONS:

    const setOnActivities = async (activites: ProjectActivity[], toSet: keyof ProjectActivity, value: ProjectActivity[keyof ProjectActivity]) => {
        if (activites.length === 0) return false;
        let activityKeys: string[] = activites.map(a => a.key);
        if (value === undefined) {
            if (["delegator", "engineer", "actualStartDate", "actualFinishDate"].includes(toSet)) {
                return bulkDelete(activityKeys, toSet as "delegator" | "engineer" | "actualStartDate" | "actualFinishDate");
            }
            return false;
        }
        if (toSet === "delegator") return setDelegatorOnActivities(activityKeys, (value as Engineer).id);
        if (toSet === "engineer") return setEngineerOnActivities(activityKeys, (value as Engineer).id);
        if (toSet === "actualStartDate") return setActualStartDateOnActivities(activityKeys, value as Date);
        if (toSet === "actualFinishDate") return setActualFinishDateOnActivities(activityKeys, value as Date);
        return false;
    }

    const setDelegatorOnActivities = async (activities: string[], delegatorId: string) => {
        try {
            await api.put('Activities/bulk-update', {
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
            await api.put('Activities/bulk-update', {
                activityKeys: activities,
                engineerId: engineerId,
            })
            return true
        } catch (err) {
            console.error(err);
            return false
        }
    }

    const setActualStartDateOnActivities = async (activities: string[], actualStartDate: Date) => {
        try {
            await api.put('Activities/bulk-update', {
                activityKeys: activities,
                actualStartDate: getDateAsISOString(actualStartDate),
            })
            return true
        }
        catch (err) {
            console.error(err);
            return false
        }
    }

    const setActualFinishDateOnActivities = async (activities: string[], actualFinishDate: Date) => {
        try {
            await api.put('Activities/bulk-update', {
                activityKeys: activities,
                actualFinishDate: getDateAsISOString(actualFinishDate),
            })
            return true
        }
        catch (err) {
            console.error(err);
            return false
        }
    }

    const bulkDelete = async (activities: string[], toDelete: "delegator" | "engineer" | "actualStartDate" | "actualFinishDate") => {
        try {
            await api.del('Activities/bulk-delete', {
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
        loading: computed(() => api.loading),
        setGeneralRemark,
        setOnActivities,
        setDelegatorOnActivities,
        setEngineerOnActivities,
        setActualStartDateOnActivities,
        setActualFinishDateOnActivities,
        bulkDelete,
    }
});