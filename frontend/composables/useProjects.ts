export const useProjects = () => {
    const config = useRuntimeConfig();
    const apiUrl = config.public.apiUrl;

    const projects = ref([]);
    const loading = ref(false);

    const getProjects = async () => {
        loading.value = true;
        try {
            if (!apiUrl) {
                throw new Error('API URL is not defined in the environment variables.');
            }
            const response = await fetch(`${apiUrl}/Projects`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                },
            });
            if (!response.ok) {
                throw new Error(`Error fetching projects: ${response.statusText}`);
            }
            projects.value = await response.json();
        } catch (err) {
            console.error(err);
        } finally {
            loading.value = false;
        }
    }

    return {
        projects,
        loading,
        getProjects,
    }
}