import React from "react";
import ReactDOM from "react-dom";
import HikeListItem from "./HikeListItem.js";
import ReactLoading from 'react-loading';
import apiFetch from "./apiFetch";

const API = 'http://localhost:4242/Hike/all';

const App = ({ data, isLoading, error }) => {
    const hikes = data || [];
  
    if (error) {
      return <p>{error.message}</p>;
    }
  
    if (isLoading) {
        return <ReactLoading type="spin" color="#000000" height={40} width={40} />
    }
  
    console.log(data);
    return hikes.map(hike =>
        (<HikeListItem key={hike.id} hike={hike} />));
  }

  const AppWithFetch = apiFetch(API)(App);


ReactDOM.render(<AppWithFetch />, document.getElementById("app"));


//.then(data=>console.log(data))