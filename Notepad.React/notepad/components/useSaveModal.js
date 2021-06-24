import { useState } from "react";

const useSaveModal = (createService, updateService, mapSaveData) => {
  const [show, setShow] = useState(false);
  const [currentData, setInitial] = useState(null);

  const handleClose = () => {
    setShow(false);
    setInitial(null);
  };
  const handleShow = (data) => {
    setShow(true);
    setInitial(data);
  };

  const isNew = !currentData || !currentData.id;

  const handleSave = async (data) => {
    const save = isNew ? createService : updateService;

    const mappedData = mapSaveData ? mapSaveData(data) : data;

    await save(mappedData);
    handleClose();
  };

  return { show, handleClose, handleShow, handleSave, currentData, isNew };
};

export default useSaveModal;
