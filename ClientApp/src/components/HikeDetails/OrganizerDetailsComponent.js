import React from "react";

class OrganizerDetailsComponent extends React.Component {
    render() {
        const organizer = this.props.organizer;
        return (
            <div>
            <p>Főrendező: {organizer.name}</p>
            <p>E-mail: {organizer.email}</p>
            <p>Telefonszám: {organizer.phoneNumber}</p>
            </div>
        )
    }
}

export default OrganizerDetailsComponent;