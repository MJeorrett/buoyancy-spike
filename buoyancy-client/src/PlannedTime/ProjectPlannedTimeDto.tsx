export type ProjectPlannedTimeDto = {
    id: number;
    projectTitle: string;
    weeks: ProjectPlannedWeekDto[];
}

export type ProjectPlannedWeekDto = {
    weekStartingMonday: string;
    totalRequiredHours: number;
    totalPlannedHours: number;
    time: ProjectPlannedTimeEntryDto[];
}

export type ProjectPlannedTimeEntryDto = {
    id: number;
    roleName: string;
    requiredHours: number;
    plannedHours: number;
}
