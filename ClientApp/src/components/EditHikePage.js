import React from 'react';
import { connect } from 'react-redux';
import { getHikeDetails, postEditHike ,deleteData} from '../actions/EditHikeActions';
import HikeForm from "./forms/HikeForm/HikeForm"


class EditHikePage extends React.Component {

    componentWillMount() {
        this.props.dispatch(getHikeDetails(this.props.match.params.id));
    }

    componentWillUnmount() {
        this.props.dispatch(deleteData());
    }

    handleSubmit = (values) => {
        const val = {...values};
        val.date = new Date(val.date.getTime() - val.date.getTimezoneOffset()*60000)
        this.props.dispatch(postEditHike(val));
    }

    render() {
        if (this.props.hasErrored){
            return (<div>Nincs ilyen túra!</div>);
        }

        if(this.props.hike.organizer && this.props.hike.organizer.id != this.props.user.id){
            return (<div>Csak a saját szervezésű túráid adatait tudod szerkeszteni!</div>)
        }
        else{
            
            const hike = {...this.props.hike};
            hike.date = new Date(hike.date);
            return (
                <div>
                    <HikeForm 
                        onSubmit={this.handleSubmit} 
                        buttonText="Elküld"
                        title="Túra adatai" 
                        initialValues = {hike}
                         />
                </div>
            )
        }
        return (<div></div>)
        
    }
}

function mapStateToProps(state) {
    return {
        hike: state.hikeEditReducer.hike,
        hasErrored: state.hikeEditReducer.hasErrored,
        user: state.authentication.user
    };
}

export default connect(mapStateToProps)(EditHikePage);