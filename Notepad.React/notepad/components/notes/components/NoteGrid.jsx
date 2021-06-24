import PropTypes from "prop-types";

import styles from "../../../styles/Home.module.css";
import NoteItem from "./NoteItem";

import { Spinner } from "react-bootstrap";

function NoteGrid(props) {
  const { results, isLoading, onShowEditModal } = props;

  if (isLoading) return <Spinner animation="border" />;

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
  onShowEditModal: PropTypes.func,
  results: PropTypes.array,
  isLoading: PropTypes.bool,
};

export default NoteGrid;
