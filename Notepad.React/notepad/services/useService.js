import { useEffect, useState } from "react";

const useService = (func, defaultData, retrigger) => {
  const [inProgress, setStatus] = useState(false);
  const [results, setData] = useState(defaultData);

  useEffect(() => {
    if (!func) return;
    const fetchData = async () => {
      setStatus(true);
      const data = await func();
      setData(data);
      setStatus(false);
    };

    fetchData();
  }, [func, retrigger]);

  return { inProgress, results };
};

export default useService;
