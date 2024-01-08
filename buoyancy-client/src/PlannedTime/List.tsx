import {
  List,
  Datagrid,
  TextField,
} from "react-admin";

const PlannedTimeList = () => {
  return (
    <List>
      <Datagrid rowClick="edit" bulkActionButtons={false}>
        <TextField source="projectTitle" />
      </Datagrid>
    </List>
  );
};

export default PlannedTimeList;
