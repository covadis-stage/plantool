import type { ActivityType } from "./ActivityType";
import type { Project } from "./Project";

export interface ProjectActivity {
    key: string; // = ProjectId + NetworkId + ActivityId
    id: string; // "Activity"(Id) in SAP
    latestStartDate?: string; // ISO date string
    latestFinishDate?: string; // ISO date string
    originalFinishDate?: string; // ISO date string
    timeEstimated?: string; // ISO 8601 duration
    timeSpent?: string; // ISO 8601 duration
    teamLeader?: string; // = Product Lead

    activityType?: ActivityType;

    workBreakdownStructure?: string;
    network?: string;

    project?: Project;

    isArchived: boolean;

    generalRemark?: string;
    actualStartDate?: string; // ISO date string
    actualFinishDate?: string; // ISO date string
    absoluteWorkload?: string; // ISO 8601 duration
}
