import React from 'react';
import { connect } from 'react-redux';
import { getHikeDetails } from '../actions/HikeDetailsActions';
import HikeForm from "./forms/HikeForm"


class EditHikePage extends React.Component {

    componentWillMount() {
        this.props.dispatch(getHikeDetails(this.props.match.params.id));
    }

    handleSubmit = (values) => {
        const { dispatch } = this.props;
        //dispatch(userActions.update(values));
        console.log(values)
    }

    render() {
        return (
            <div>
                <HikeForm 
                    onSubmit={this.handleSubmit} 
                    buttonText="Elküld"
                    title="Túra adatai" 
                     />
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        hike: state.hikeDetailsReducer.hike
    };
}

export default connect(mapStateToProps)(EditHikePage);