export const roleAbbreviations = {
    "Developer - Full Stack": "FSD",
    "Developer - Front End": "FED",
    "Developer - Back End": "BED",
    "Developer - Mobile": "MD",
    "Tech Lead": "TL",
    "Designer": "D",
    "Tester": "T",
};

export const getRoleAbbreviation = (roleName: string) => {
    return roleAbbreviations[roleName as keyof typeof roleAbbreviations];
}