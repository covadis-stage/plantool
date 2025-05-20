import type { Engineer } from "~/types/Engineer";

type UpdateRequestKey = "delegator" | "engineer" | "actualFinishDate" | "actualStartDate";

export const useUpdateRequestMapper = () => {
    const engineerStore = useEngineerStore();
    const { mapDate } = useUtil();

    const mapKey = (backendKey: string): UpdateRequestKey | null => {
        const keyMap: Record<string, UpdateRequestKey> = {
            DelegatorId: "delegator",
            EngineerId: "engineer",
            ActualFinishDate: "actualFinishDate",
            ActualStartDate: "actualStartDate",
        };
        return keyMap[backendKey] ?? null;
    }

    const mapValue = (key: UpdateRequestKey, value: string | null | undefined): Date | Engineer | undefined => {
        if (!value) return undefined;
        if (key === "delegator") {
            return engineerStore.delegators.find((delegator) => delegator.id === value);
        } else if (key === "engineer") {
            return engineerStore.engineers.find((engineer) => engineer.id === value);
        } else if (key === "actualFinishDate" || key === "actualStartDate") {
            return mapDate(value);
        }
    }

    return {
        mapKey,
        mapValue,
    }
}