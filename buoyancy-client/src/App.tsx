import { Admin, Resource } from "react-admin";
import dataProvider from "./dataProvider";
import PeopleList from "./People/List";
import PersonShow from "./People/Show";
import PersonEdit from "./People/Edit";
import PersonCreate from "./People/Create";

const App = () => {
  return (
    <Admin title="Buoyancy" dataProvider={dataProvider}>
      <Resource
        name="persons"
        list={PeopleList}
        show={PersonShow}
        edit={PersonEdit}
        create={PersonCreate}
      />
    </Admin>
  );
};

export default App;
