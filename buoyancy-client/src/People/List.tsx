import { List, Datagrid, TextField, ReferenceField, ShowButton } from "react-admin";

const PeopleList = () => (
  <List>
    <Datagrid rowClick="edit" bulkActionButtons={false}>
      <TextField source="id" />
      <TextField source="name" />
      <ReferenceField source="roleId" reference="roles" />
      <div style={{ textAlign: "right" }}>
          <ShowButton label="" />
        </div>
    </Datagrid>
  </List>
);

export default PeopleList;
