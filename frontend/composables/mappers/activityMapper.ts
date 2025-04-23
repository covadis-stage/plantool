import type { ProjectActivity } from "~/types/Activity";
import { useEngineerMapper } from "./engineerMapper";

export const useActivityMapper = () => {
    const engineerMapper = useEngineerMapper();

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
            absoluteWorkload: activity.absoluteWorkload,
            delegator: engineerMapper.mapEngineer(activity.delegator),
            engineer: engineerMapper.mapEngineer(activity.engineer),
        } as ProjectActivity;
    }

    return {
        mapActivities,
    };
}