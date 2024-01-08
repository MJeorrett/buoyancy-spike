export type ProjectPlannedTimeDto = {
    id: number;
    projectTitle: string;
    weeks: ProjectPlannedWeekDto[];
}

export type ProjectPlannedWeekDto = {
    weekStartingMonday: string;
    entries: ProjectPlannedTimeEntryDto[];
}

export type ProjectPlannedTimeEntryDto = {
    id: number;
    roleName: string;
    hours: number;
}
