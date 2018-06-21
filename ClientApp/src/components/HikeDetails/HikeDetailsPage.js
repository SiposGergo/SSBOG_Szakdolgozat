import React from "react";
import ReactLoading from "react-loading";
import HikeDetailsCompoent from "./HikeDetailsCompoent";
import OrganizerDetailsComponent from "./OrganizerDetailsComponent";
import Comment from "./Comment";
import CourseDetails from "./CourseDetails";
import { Tab } from 'semantic-ui-react';
import CommentForm from "./CommentForm";
import { connect } from 'react-redux';
import { getHikeDetails, postComent } from "../../actions/HikeDetailsActions";
import { SendDanger } from "../../services/NotificationSender";
import { change } from 'redux-form';


export class HikeDetailsPage extends React.Component {

    componentDidMount() {
        this.props.dispatch(getHikeDetails(this.props.match.params.id));
    }

    submitComment = (value) => {
        if (!this.props.user) {
            this.props.dispatch(SendDanger("Lépj be a kommenteléshez!"));
        } else {
            this.props.dispatch(postComent(this.props.hike.id, this.props.user.id, value.message));
            this.props.dispatch(change('CommentForm', 'message', ''));
        }
    }

    render() {
        if (this.props.hasErrored) {
            return <p>Hiba!</p>;
        }

        if (this.props.isLoading) {
            return <ReactLoading type="spin" color="#000000" height={40} width={40} />
        }

        const hike = this.props.hike;

        const panes = hike.courses
            .map((course) => {
                return {
                    menuItem: course.name,
                    render: () => (<Tab.Pane><CourseDetails hikeId={hike.id} key={course.id} course={course} /></Tab.Pane>)
                }
            });

        return (
            <div style={{width:"90%"}}>
                <HikeDetailsCompoent hike={hike} />
                <OrganizerDetailsComponent organizer={hike.organizer} />
                <div >
                    <Tab menu={{ color:"green", size:"huge", width:"2", inverted: true, attached: true }} panes={panes} />
                </div>
                <div>
                {
                    hike.comments.length == 0 ?
                        <p>Nincsenek hozzászólások</p> :
                        hike.comments.map((comment) => <Comment key={comment.id} comment={comment}  user={this.props.user}/>)
                }
                </div>
                <CommentForm onSubmit={this.submitComment} />
            </div>
        )
    }
}

function mapStateToProps(state) {
    const { hasErrored, isLoading, hike } = state.hikeDetailsReducer;
    return {
        hasErrored,
        isLoading,
        hike,
        user: state.authentication.user
    };
}

export default connect(mapStateToProps, null, null, { pure: false })(HikeDetailsPage);