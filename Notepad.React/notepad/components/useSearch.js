import { useEffect, useState } from "react";
import { startSubmit, stopSubmit } from "redux-form";
import { useDispatch } from "react-redux";

const defaultResults = [];
const defaultParameters = {};

const useSearch = (formName, searchFunc, retrigger) => {
  const [inProgress, setStatus] = useState(false);
  const [results, setData] = useState(defaultResults);
  const [parameters, setParameters] = useState(defaultParameters);
  const dispatch = useDispatch();

  useEffect(() => {
    if (!searchFunc) return;
    const fetchData = async () => {
      dispatch(startSubmit(formName));
      setStatus(true);
      try {
        const searchResults = await searchFunc(parameters);
        setData(searchResults);
        setStatus(false);
        dispatch(stopSubmit(formName));
      } catch (exc) {
        if (exc.errors) {
          dispatch(stopSubmit(formName, exc.errors));
        } else throw exc;
      }
    };

    fetchData();
  }, [retrigger, parameters]);

  const onSearch = (values) => setParameters(values);

  return { inProgress, results, onSearch };
};

export default useSearch;
