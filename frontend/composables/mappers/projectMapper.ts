import type { Project } from "~/types/Project";

export const useProjectMapper = () => {
    const mapProjects = (projects: any[]): Project[] => {
        return projects.map((project) => mapProject(project));
    }

    const mapProject = (project: any): Project => {
        return {
            key: project.key,
            customer: project.customer,
            projectManager: project.projectManager,
            activities: [],
            isArchived: project.isArchived,
        };
    };

    return {
        mapProjects,
        mapProject,
    };
}