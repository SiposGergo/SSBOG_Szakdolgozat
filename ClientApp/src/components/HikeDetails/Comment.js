import React from "react";
//import moment from "moment";
import moment from "moment-timezone"
import { Link } from "react-router-dom";
import { config } from "../../helpers/config.js";

class Comment extends React.Component {
    render() {
        const comment = this.props.comment;
        return (
            <div className={"comment-box" +(this.props.user && comment.author.id == this.props.user.id ? " comment-box-own" : "") }>
                <Link className="link" to={"/user/" + comment.author.id}> {comment.author.name}  </Link>
                <div>{moment.utc(comment.timeStamp).local().format(config.dateTimeFormat)}</div>
                <p>{comment.commentText}</p>

            </div>
        )
    }
}

export default Comment;