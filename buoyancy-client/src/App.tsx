import { Admin, Resource, ListGuesser, EditGuesser } from "react-admin";
import dataProvider from "./dataProvider";

const App = () => {
  return (
    <Admin title="Buoyancy" dataProvider={dataProvider}>
      <Resource name="persons" list={ListGuesser} create={EditGuesser} />
    </Admin>
  );
};

export default App;
