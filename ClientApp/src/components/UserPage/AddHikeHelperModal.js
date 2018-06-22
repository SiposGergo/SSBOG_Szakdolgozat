import React from 'react';
import ReactDOM from 'react-dom';
import Modal from 'react-modal';

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)'
    }
};

class AddHikeHelperModal extends React.Component {

    render() {
        return (
            <div className="modal">
                <Modal 
                    className= "my-modal"
                    overlayClassName= "my-overlay"
                    isOpen={this.props.modalIsOpen}
                    style={customStyles}
                    ariaHideApp={false}
                    contentLabel="Example Modal"
                >
                    <button className="btn btn-close" onClick={this.props.closeModal}>X</button>
                    <h3>Segítő hozzáadása</h3>

                    <form onSubmit={this.props.onSubmit}>
                        <label htmlFor="userName">Felhasználónév:</label>
                        <input className="form-control" type="text" name="userName" autoFocus />
                        <input type="number" name="hikeId" defaultValue={this.props.hikeId} hidden={true} />
                        <input type="submit" value="Hozzáad" className="btn btn-green" />
                    </form>
                </Modal>
            </div>
        );
    }
}

export default AddHikeHelperModal;