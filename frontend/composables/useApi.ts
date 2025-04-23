export const useApi = () => {
    const config = useRuntimeConfig();
    const apiUrl = config.public.apiUrl;

    const loading = ref(false);

    const get = async (url: string) => {
        loading.value = true;
        if (!apiUrl) throw new Error('API URL is not defined in the environment variables.');
        const response = await fetch(`${apiUrl}/${url}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
        });
        if (!response.ok) throw new Error(`Error during GET to ${apiUrl}/${url}: ${response.statusText}`);
        const json = await response.json();
        loading.value = false;
        return json;
    }

    const post = async (url: string, body: any) => {
        loading.value = true;
        if (!apiUrl) throw new Error('API URL is not defined in the environment variables.');
        const response = await fetch(`${apiUrl}/${url}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(body),
        });
        if (!response.ok) throw new Error(`Error during POST: ${response.statusText}`);
        const json = await response.json();
        loading.value = false;
        return json;
    }

    const put = async (url: string, body: any) => {
        loading.value = true;
        if (!apiUrl) throw new Error('API URL is not defined in the environment variables.');
        const response = await fetch(`${apiUrl}/${url}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(body),
        });
        if (!response.ok) throw new Error(`Error during PUT: ${response.statusText}`);
        const json = await response.json();
        loading.value = false;
        return json;
    }

    return {
        loading,
        get,
        post,
        put
    }
}