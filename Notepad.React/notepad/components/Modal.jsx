import { Button, Modal as BsModal } from "react-bootstrap";

export default function Modal({ title, body, footer, show, onHide }) {
  return (
    <BsModal show={show} onHide={onHide}>
      <BsModal.Header closeButton>
        <BsModal.Title>{title}</BsModal.Title>
      </BsModal.Header>
      <BsModal.Body>{body}</BsModal.Body>
      <BsModal.Footer>{footer}</BsModal.Footer>
    </BsModal>
  );
}
