import React from 'react';
import {Link} from 'react-router';
import './App.css';

const App = (props) => {
        return (
            <div>
                <h1>App</h1>
                <ul>
                    <li><Link to="/Home">Home</Link></li>
                    <li><Link to="/Recipes">Recipes</Link></li>
                </ul>

                {props.children}
            </div>
        )
};


export default App;
