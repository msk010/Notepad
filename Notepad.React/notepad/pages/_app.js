import "bootstrap/dist/css/bootstrap.css";

import "../styles/globals.css";
import { Provider } from "react-redux";

import { store } from "../store/store";
import axios from "axios";
import { configureValidationErrors } from "utils/axios";

configureValidationErrors(axios);

function MyApp({ Component, pageProps }) {
  return (
    <Provider store={store}>
      <Component {...pageProps} />
    </Provider>
  );
}

export default MyApp;
