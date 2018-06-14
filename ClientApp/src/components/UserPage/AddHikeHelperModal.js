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
                    isOpen={this.props.modalIsOpen}
                    style={customStyles}
                    ariaHideApp={false}
                    contentLabel="Example Modal" 
                >
                    <h2>Segítő hozzáadása</h2>
                    <button onClick={this.props.closeModal}>close</button>
                    <form onSubmit={this.props.onSubmit}>
                        <label htmlFor="userName">Felhasználónév:</label>
                        <input type="text" name="userName" />
                        <input type="number" name="hikeId" defaultValue={this.props.hikeId} hidden={true} />
                        <input type="submit" value="Hozzáad" />
                    </form>
                </Modal>
            </div>
        );
    }
}

export default AddHikeHelperModal;