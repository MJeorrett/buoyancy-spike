export type ProjectRequiredTimeDto = {
    id: number;
    projectTitle: string;
    weeks: ProjectRequiredWeekDto[];
}

export type ProjectRequiredWeekDto = {
    weekStartingMonday: string;
    totalHours: number;
    entries: ProjectRequiredTimeEntryDto[];
}

export type ProjectRequiredTimeEntryDto = {
    id: number;
    roleName: string;
    hours: number;
}
