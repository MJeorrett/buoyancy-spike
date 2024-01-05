import { Create, ReferenceInput, SimpleForm, TextInput } from "react-admin";

const PersonCreate = () => (
  <Create>
    <SimpleForm>
      <TextInput source="name" />
      <ReferenceInput source="roleId" reference="roles" />
    </SimpleForm>
  </Create>
);

export default PersonCreate;
