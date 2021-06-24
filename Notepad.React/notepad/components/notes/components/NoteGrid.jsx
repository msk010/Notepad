import PropTypes from "prop-types";

import styles from "../../../styles/Home.module.css";
import NoteItem from "./NoteItem";
import NoteService from "services/NoteService";

import useService from "services/useService";
import { Spinner } from "react-bootstrap";

const emptyArray = [];
function NoteGrid(props) {
  const { shouldRefresh, onShowEditModal } = props;
  const { inProgress, results } = useService(
    NoteService.getAll,
    emptyArray,
    shouldRefresh
  );

  if (inProgress) return <Spinner animation="border" />;

  return (
    <div className={styles.grid}>
      {results.map((item) => (
        <NoteItem
          key={item.id}
          note={item}
          onClick={() => onShowEditModal(item)}
        />
      ))}
    </div>
  );
}

NoteGrid.propTypes = {
  shouldRefresh: PropTypes.bool,
  onShowEditModal: PropTypes.func,
};

export default NoteGrid;
