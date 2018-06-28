import ReactLoading from "react-loading";
import React from "react";

const LoadSpinner = (props) =>
    (<div className="loading">
        <ReactLoading type="spin" color="#000000" height={100} width={100} />
    </div>)

    export default LoadSpinner;