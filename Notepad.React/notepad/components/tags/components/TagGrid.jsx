import PropTypes from "prop-types";

import styles from "../../../styles/Home.module.css";
import TagItem from "./TagItem";

import { Spinner } from "react-bootstrap";

function TagGrid(props) {
  const { results, isLoading, onShowEditModal } = props;

  if (isLoading) return <Spinner animation="border" />;

  if (!results || results.length === 0)
    return <div className={`${styles.grid} text-secondary`}>Empty</div>;

  return (
    <div className={styles.grid}>
      {results.map((item) => (
        <TagItem
          key={item.id}
          note={item}
          onClick={() => onShowEditModal(item)}
        />
      ))}
    </div>
  );
}

TagGrid.propTypes = {
  onShowEditModal: PropTypes.func,
  results: PropTypes.array,
  isLoading: PropTypes.bool,
};

export default TagGrid;
