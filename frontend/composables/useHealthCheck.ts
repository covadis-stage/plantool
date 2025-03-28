import { ref } from 'vue';

export const useHealthCheck = () => {
    const apiUrl = import.meta.env.VITE_API_URL;
    const isHealthy = ref(false);
    const error = ref<string | null>(null);

    const checkHealth = async () => {
        try {
            const response = await fetch(`${apiUrl}/HealthCheck`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                },
            });
            isHealthy.value = response.ok;
            error.value = null;
        } catch (err) {
            isHealthy.value = false;
            error.value = `Error while accessing "${apiUrl}/HealthCheck": ${err instanceof Error ? err.message : 'Unknown error'}`;
        }
    };

    return {
        isHealthy,
        error,
        checkHealth,
    };
}
