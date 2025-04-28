import type { ProjectActivity } from "~/types/Activity";
import { useEngineerMapper } from "./engineerMapper";
import { parseTimeSpan } from "~/types/TimeSpan";

export const useActivityMapper = () => {
    const engineerMapper = useEngineerMapper();

    const mapActivities = (activities: any[]): ProjectActivity[] => {
        return activities.map(activity => mapActivity(activity));
    };

    const mapActivity = (activity: any): ProjectActivity => {
        return {
            key: activity.key,
            id: activity.id,
            latestStartDate: mapDate(activity.latestStartDate),
            latestFinishDate: mapDate(activity.latestFinishDate),
            originalFinishDate: mapDate(activity.originalFinishDate),
            timeEstimated: parseTimeSpan(activity.timeEstimated),
            timeSpent: parseTimeSpan(activity.timeSpent),
            teamLeader: activity.teamLeader,
            workCenter: activity.workCenter,
            activityType: activity.activityType,
            workBreakdownStructure: activity.workBreakdownStructure,
            network: activity.network,
            project: activity.project,
            isArchived: activity.isArchived,
            generalRemark: activity.generalRemark,
            actualStartDate: mapDate(activity.actualStartDate),
            actualFinishDate: mapDate(activity.actualFinishDate),
            absoluteWorkload: parseTimeSpan(activity.absoluteWorkload),
            delegator: engineerMapper.mapEngineer(activity.delegator),
            engineer: engineerMapper.mapEngineer(activity.engineer),
        } as ProjectActivity;
    }

    const mapDate = (date?: string): Date | undefined => {
        if (!date) return undefined;
        const parsedDate = new Date(date);
        if (isNaN(parsedDate.getTime())) {
            console.error(`Invalid date format: ${date}`);
            return undefined;
        }
        return parsedDate;
    }

    return {
        mapActivities,
    };
}