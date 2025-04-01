import type { Project } from "~/types/Project";

export const useProjectStore = defineStore("ProjectStore", () => {
    const projectMapper = useProjectMapper();
    const activitiesMapper = useActivityMapper();
    const { loading, get, post } = useApi();

    const projects = ref<Project[]>([]);

    const getProjects = async () => {
        try {
            const response = await get('Projects');
            projects.value = projectMapper.mapProjects(response);
            if (!response) return [];
            return projects.value;
        } catch (err) {
            console.error(err);
            return [];
        }
    }

    const getActivitiesForProject = async (projectKey: string) => {
        try {
            if (projects.value.length === 0) return []

            const project = projects.value.find(project => project.key === projectKey);
            if (!project) return []
            if (project.activities && project.activities.length > 0) return project.activities

            const response = await get(`Activities/project/${projectKey}`);
            if (!response) return [];

            const activities = activitiesMapper.mapActivities(response);
            project.activities = activities;
            return activities;
        }
        catch (err) {
            console.error(err);
            return [];
        }
    }

    return {
        loading,
        projects,
        getProjects,
        getActivitiesForProject,
    }
});