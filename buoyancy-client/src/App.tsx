import { Admin, Resource } from "react-admin";
import dataProvider from "./dataProvider";
import PeopleList from "./People/List";
import PersonShow from "./People/Show";
import PersonEdit from "./People/Edit";
import PersonCreate from "./People/Create";
import ProjectsList from "./Projects/List";
import ProjectShow from "./Projects/Show";
import ProjectEdit from "./Projects/Edit";
import ProjectCreate from "./Projects/Create";
import PlannedTimeList from "./PlannedTime/List";

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
      <Resource
        name="projects"
        list={ProjectsList}
        show={ProjectShow}
        edit={ProjectEdit}
        create={ProjectCreate}
      />
      <Resource name="plannedtimes" list={PlannedTimeList} />
    </Admin>
  );
};

export default App;
