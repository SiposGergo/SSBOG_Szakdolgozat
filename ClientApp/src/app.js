import React from "react";
import ReactDOM from "react-dom";

const API = 'http://localhost:4242/Hike/all';

class App extends React.Component {
    state = {hikes:[{name:"lol", id:1}]};

  componentDidMount() {
     fetch(API)
       .then(response => response.json())
       .then(data => this.setState({ hikes: data.result }));
  }
  render(){
      return this.state.hikes.map(hike => (<p key={hike.id.toString()}>{hike.name}</p>));
  }
}

ReactDOM.render(<App/>, document.getElementById("app"));
