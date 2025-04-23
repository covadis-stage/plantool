import type { Engineer } from "~/types/Engineer";

export const useEngineerMapper = () => {
    const mapEngineers = (engineers: any[]): Engineer[] => {
        if (!engineers) return [];
        return engineers.map(engineer => mapEngineer(engineer)).filter((engineer): engineer is Engineer => engineer != undefined);
    };

    const mapEngineer = (engineer: any): Engineer | undefined => {
        if (!engineer) return undefined;
        return {
            id: engineer.id,
            name: engineer.name,
            canDelegate: engineer.canDelegate
        } as Engineer;
    }

    return {
        mapEngineers,
        mapEngineer,
    };
}