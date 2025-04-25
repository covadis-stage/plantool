
export const useActivityStore = defineStore("ActivityStore", () => {
    const { loading, put, del } = useApi();
    const { getDateAsISOString } = useUtil();

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

    const setActualStartDateOnActivities = async (activities: string[], actualStartDate: Date) => {
        try {
            await put('Activities/bulk-update', {
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
            await put('Activities/bulk-update', {
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
        setDelegatorOnActivities,
        setEngineerOnActivities,
        setActualStartDateOnActivities,
        setActualFinishDateOnActivities,
        bulkDelete,
    }
});