export type PersonDto = {
    id: number,
    name: string,
    roleName: string,
    weeklyCapacityHours: number,
    plannedWeeks: PersonPlannedWeekDto[],
}

export type PersonPlannedWeekDto = {
    weekStartingMonday: string,
    totalPlannedHours: number,
}