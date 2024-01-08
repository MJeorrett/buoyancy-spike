import { List, useListContext } from "react-admin";
import { ProjectPlannedTimeDto } from "./ProjectPlannedTimeDto";
import { getRoleAbbreviation } from "../constants";
import {
  Chip,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";

const PlannedTimeTable = () => {
  const {
    data: listData,
    isFetching,
    isLoading,
  } = useListContext<ProjectPlannedTimeDto>();

  if (isLoading || isFetching) {
    return <div>Loading...</div>;
  }

  const headers = [
    "project",
    ...listData[0].weeks.map((_) => _.weekStartingMonday),
  ];
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            {headers.map((header) => (
              <th key={header}>{header}</th>
            ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {listData.map((row) => (
            <TableRow key={row.id}>
              <TableCell style={{ minWidth: "150px" }}>
                {row.projectTitle}
              </TableCell>
              {row.weeks.map((week) => (
                <TableCell key={week.weekStartingMonday}>
                  {week.entries.map(
                    (entry) =>
                      entry.hours > 0 && (
                        <Chip
                          key={entry.id}
                          label={`${getRoleAbbreviation(entry.roleName)}: ${
                            entry.hours
                          }`}
                        />
                      )
                  )}
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

const PlannedTimeList = () => {
  return (
    <List>
      <PlannedTimeTable />
    </List>
  );
};

export default PlannedTimeList;
