import type { Project } from "~/types/Project";
import { useProjectMapper } from "./mappers/projectMapper";

export const useProjects = () => {
    const { loading, get, post } = useApi();
    const projectMapper = useProjectMapper();

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

    return {
        loadingProjects: loading,
        projects,
        getProjects,
    }
}