import { List, Datagrid, TextField, ShowButton } from "react-admin";

const ProjectsList = () => (
  <List>
    <Datagrid rowClick="edit" bulkActionButtons={false}>
      <TextField source="id" />
      <TextField source="title" />
      <div style={{ textAlign: "right" }}>
          <ShowButton label="" />
        </div>
    </Datagrid>
  </List>
);

export default ProjectsList;
