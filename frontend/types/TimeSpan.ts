export interface TimeSpan {
    days: number;
    hours: number;
    minutes: number;
    seconds: number;
    milliseconds: number;
}

// Example of input: "1.02:30:45.1234567"
export function parseTimeSpan(timeSpanStr?: string): TimeSpan | undefined {
    if (!timeSpanStr) return undefined;

    const regex = /^((\d+)\.)?(\d{2}):(\d{2}):(\d{2})(?:\.(\d+))?$/;
    const match = RegExp(regex).exec(timeSpanStr);

    if (!match) return undefined;

    const days = parseInt(match[2] ?? "0", 10);
    const hours = parseInt(match[3], 10);
    const minutes = parseInt(match[4], 10);
    const seconds = parseInt(match[5], 10);
    const milliseconds = match[6] ? parseInt(match[6].substring(0, 3), 10) : 0; // take only first 3 digits for ms

    return { days, hours, minutes, seconds, milliseconds } as TimeSpan;
}

export function timeSpanToMilliseconds(timeSpan: TimeSpan): number {
    return ((timeSpan.days * 24 + timeSpan.hours) * 60 + timeSpan.minutes) * 60 * 1000 +
        timeSpan.seconds * 1000 + timeSpan.milliseconds;
}

export function timeSpanToString(timeSpan: TimeSpan): string {
    return `${timeSpan.hours + timeSpan.days * 24}h ${timeSpan.minutes}m ${timeSpan.seconds}s`;
}