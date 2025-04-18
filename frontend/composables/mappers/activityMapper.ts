import type { ProjectActivity } from "~/types/Activity";

export const useActivityMapper = () => {
    const mapActivities = (activities: any[]): ProjectActivity[] => {
        return activities.map(activity => mapActivity(activity));
    };

    const mapActivity = (activity: any): ProjectActivity => {
        return {
            key: activity.key,
            id: activity.id,
            latestStartDate: activity.latestStartDate,
            latestFinishDate: activity.latestFinishDate,
            originalFinishDate: activity.originalFinishDate,
            timeEstimated: activity.timeEstimated,
            timeSpent: activity.timeSpent,
            teamLeader: activity.teamLeader,
            workCenter: activity.workCenter,
            activityType: activity.activityType,
            workBreakdownStructure: activity.workBreakdownStructure,
            network: activity.network,
            project: activity.project,
            isArchived: activity.isArchived,
            generalRemark: activity.generalRemark,
            actualStartDate: activity.actualStartDate,
            actualFinishDate: activity.actualFinishDate,
            absoluteWorkload: activity.absoluteWorkload
        };
    }

    return {
        mapActivities,
    };
}