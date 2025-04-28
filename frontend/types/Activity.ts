import type { ActivityType } from "./ActivityType";
import type { Engineer } from "./Engineer";
import type { Project } from "./Project";
import type { TimeSpan } from "./TimeSpan";
import type { WorkCenter } from "./WorkCenter";

export interface ProjectActivity {
    key: string; // = ProjectId + NetworkId + ActivityId
    id: string; // "Activity"(Id) in SAP
    latestStartDate?: Date;
    latestFinishDate?: Date;
    originalFinishDate?: Date;
    timeEstimated?: TimeSpan;
    timeSpent?: TimeSpan;
    teamLeader?: string; // = Product Lead

    workCenter?: WorkCenter;
    activityType?: ActivityType;

    workBreakdownStructure?: string;
    network?: string;

    project?: Project;

    isArchived: boolean;

    generalRemark?: string;
    actualStartDate?: Date;
    actualFinishDate?: Date;
    absoluteWorkload?: TimeSpan;
    delegator?: Engineer; // = PL. Workcenter
    engineer?: Engineer;
}
