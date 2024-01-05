import { Create, SimpleForm, TextInput } from "react-admin";

const ProjectCreate = () => (
  <Create>
    <SimpleForm>
      <TextInput source="title" />
    </SimpleForm>
  </Create>
);

export default ProjectCreate;
