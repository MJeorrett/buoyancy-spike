import { List, useListContext } from "react-admin";
import { PersonDto } from "./PersonDto";
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

const PeopleTable = () => {
  const { data, isFetching, isLoading } = useListContext<PersonDto>();

  if (isLoading || isFetching) {
    return "Loading...";
  }

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <th>Name</th>
            <th>Role</th>
            {data[0].plannedWeeks.map((_) => (
              <th style={{ whiteSpace: "nowrap" }}>{_.weekStartingMonday}</th>
            ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <TableRow key={row.id}>
              <TableCell>{row.name}</TableCell>
              <TableCell>{row.roleName}</TableCell>
              {row.plannedWeeks.map((week) => (
                <TableCell key={week.weekStartingMonday}>
                  <Chip
                    variant="outlined"
                    sx={{
                      bgcolor:
                        row.weeklyCapacityHours > week.totalPlannedHours
                          ? "red"
                          : row.weeklyCapacityHours < week.totalPlannedHours ? "orange" : "white",
                      color:
                        row.weeklyCapacityHours != week.totalPlannedHours
                          ? "white"
                          : "black",
                      fontWeight:
                        row.weeklyCapacityHours != week.totalPlannedHours
                          ? "bold"
                          : "normal",
                    }}
                    label={`${week.totalPlannedHours}/${row.weeklyCapacityHours}`}
                  />
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

const PeopleList = () => (
  <List>
    <PeopleTable />
  </List>
);

export default PeopleList;
