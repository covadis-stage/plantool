import type { ProjectActivity } from "~/types/Activity";
import type { Project } from "~/types/Project";

export const useProjectStore = defineStore("ProjectStore", () => {
    const projectMapper = useProjectMapper();
    const activitiesMapper = useActivityMapper();
    const { loading, get } = useApi();

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

    /**
     * Update a key in the local state of an activity and trigger reactivity.
     *
     * @param activityKey The activity to update (will look through all projects to find it)
     * @param key The key to update in the ProjectActivity object (e.g. "actualStartDate")
     * @param value The value to set the key to (e.g. new Date()), typing is inferred from the ProjectActivity type
     *
     * @example updateLocalActivityState('P0.01134', 'actualStartDate', new Date())
     */
    const updateLocalActivityState = <K extends keyof ProjectActivity>(activityKey: string, key: K, value: ProjectActivity[K]): void => {
        let activity: ProjectActivity | null = findActivity(activityKey);
        if (!activity) return;
        if (activity[key] === value) return;

        activity[key] = value; // Update the local state
        projects.value = [...projects.value]; // Trigger reactivity
    }

    const findActivity = (activityKey: string) => {
        for (const project of projects.value) {
            if (!project.activities) continue;
            const foundActivity = project.activities.find(activity => activity.key === activityKey);
            if (foundActivity) return foundActivity;
        }
        return null;
    }

    return {
        loading,
        projects,
        getProjects,
        getActivitiesForProject,
        updateLocalActivityState,
    }
});