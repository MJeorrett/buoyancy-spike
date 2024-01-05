import { Edit, ReferenceInput, SimpleForm, TextInput } from "react-admin";

const PersonEdit = () => (
  <Edit>
    <SimpleForm>
      <TextInput source="name" />
      <ReferenceInput source="roleId" reference="roles" />
    </SimpleForm>
  </Edit>
);

export default PersonEdit;
