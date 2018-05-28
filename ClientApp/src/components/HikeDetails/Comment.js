import React from "react";
import moment from "moment";
import {Link} from "react-router-dom";
import {config} from "../../helpers/config.js";

class Comment extends React.Component {
    render() {
        const comment = this.props.comment;
        return (
            <div>
                <Link to={"/user/"+comment.author.id}> {comment.author.name}  </Link>
                <p>{moment(comment.timeStamp).format(config.dateTimeFormat)}</p>
                <p>{comment.commentText}</p>
            </div>
        )
    }
}

export default Comment;