import React from "react";
import ReactLoading from "react-loading";
import HikeDetailsCompoent from "./HikeDetailsCompoent";
import OrganizerDetailsComponent from "./OrganizerDetailsComponent";
import Comment from "./Comment";
import CourseDetails from "./CourseDetails";
import { Tab } from 'semantic-ui-react';

class HikeDetailsPage extends React.Component {

    state = {
        hike: { comments: [], organizer: {}, courses: [] },
        isLoading: false,
        error: ""
    }

    componentDidMount() {
        const API = "http://localhost:4242/Hike/details/" + this.props.match.params.id;
        this.setState({ isLoading: true });
        fetch(API)
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error();
                }
            })
            .then(data => this.setState({ hike: data, isLoading: false }))
            .catch(error => this.setState({ error, isLoading: false }));
    }


    render() {
        if (this.state.error) {
            return <p>Hiba!</p>;
        }

        if (this.state.isLoading) {
            return <ReactLoading type="spin" color="#000000" height={40} width={40} />
        }

        const hike = this.state.hike;

        const panes = hike.courses
            .map((course) => {
                return {
                    menuItem: course.name,
                    render: () => (<Tab.Pane><CourseDetails key={course.id} course={course} /></Tab.Pane>)
                }
            });

        return (
            <div>
                <HikeDetailsCompoent hike={hike} />
                <OrganizerDetailsComponent organizer={hike.organizer} />
                <div>
                    <Tab panes={panes} />
                </div>

                {
                    hike.comments.length == 0 ?
                        <p>Nincsenek hozzászólások</p> :
                        hike.comments.map((comment) => <Comment key={comment.id} comment={comment} />)
                }

            </div>
        )
    }

}

export default HikeDetailsPage;