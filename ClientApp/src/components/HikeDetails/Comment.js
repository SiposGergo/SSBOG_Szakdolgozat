import React from "react";
import moment from "moment";

class Comment extends React.Component {
    render() {
        const comment = this.props.comment;
        return (
            <div>
                <p>{moment(comment.timeStamp).format('YYYY MM DD HH:mm')}</p>
                <p>{comment.author.name} : {comment.commentText}</p>
            </div>
        )
    }
}

export default Comment;