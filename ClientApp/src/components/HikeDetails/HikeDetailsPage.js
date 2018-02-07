import React from "react";
import { locale } from "moment";
import ReactLoading from "react-loading";

class HikeDetailsPage extends React.Component {

    state = {
        hike: {},
        isLoading: false,
        error: ""
    }

    componentDidMount() {
        const API = "http://localhost:4242/Hike/details/" + this.props.match.params.id;
        console.log(this.props)
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

        return (
            <div>
                {this.state.hike.name}
            </div>
        )


    }

}

export default HikeDetailsPage;