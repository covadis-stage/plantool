<script setup lang="ts">
import type { TimeSpan } from '~/types/TimeSpan';

defineProps<{
    timeSpan: TimeSpan;
}>();
</script>

<template>
    <p>
        <span :class="{'gray': timeSpan.days == 0 && timeSpan.hours == 0}">{{ timeSpan.days * 24 + timeSpan.hours }}h </span>
        <span :class="{'gray': timeSpan.days == 0 && timeSpan.hours == 0 && timeSpan.minutes == 0}">{{ timeSpan.minutes }}m </span>
        <span :class="{'gray': timeSpan.days == 0 && timeSpan.hours == 0 && timeSpan.minutes == 0 && timeSpan.seconds == 0}">{{ timeSpan.seconds }}s</span>
    </p>
</template>

<style lang="scss" scoped>
.gray {
    color: var(--p-neutral-300);
}
</style>