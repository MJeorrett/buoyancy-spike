import { Edit, SimpleForm, TextInput } from "react-admin";

const ProjectEdit = () => (
  <Edit>
    <SimpleForm>
      <TextInput source="title" />
    </SimpleForm>
  </Edit>
);

export default ProjectEdit;
