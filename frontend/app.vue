<script setup lang="ts">

const bulkStore = useBulkStore();
const realtime = useRealtime();
const projectStore = useProjectStore();
const updateRequestMapper = useUpdateRequestMapper();

onMounted(() => {
    window.addEventListener('mouseup', (event) => {
        bulkStore.stopSelecting();
    });

    realtime.startConnection();
    realtime.onActivityUpdate((activityKey, key, value) => {
        console.log('activity update', activityKey, key, value);

        let mappedKey = updateRequestMapper.mapKey(key);
        if (mappedKey == null) return;
        let mappedValue = updateRequestMapper.mapValue(mappedKey, value);
        projectStore.updateLocalActivityState(activityKey, mappedKey, mappedValue);
    });
});
</script>

<template>
    <Toast></Toast>
    <NuxtLayout>
        <NuxtPage />
    </NuxtLayout>
</template>
