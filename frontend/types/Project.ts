import type { ProjectActivity } from "./Activity";

export interface Project {
    key: string; // Equivalent to ProjectId
    customer?: string | null;
    projectManager?: string | null;
    activities: ProjectActivity[];
    isArchived: boolean; // Updated automatically
}