import {
  EditButton,
  ReferenceField,
  Show,
  SimpleShowLayout,
  TextField,
  TopToolbar,
} from "react-admin";

const PersonShow = () => (
  <Show
    actions={
      <TopToolbar>
        <EditButton />
      </TopToolbar>
    }
  >
    <SimpleShowLayout>
      <TextField source="id" />
      <TextField source="name" />
      <ReferenceField source="roleId" reference="roles" />
    </SimpleShowLayout>
  </Show>
);

export default PersonShow;
