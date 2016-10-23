import React from 'react';
import NavTab from './components/nav-tab';

const App = (props) => {
    return (
        <div className="container">
            <NavTab/>

            {props.children}
        </div>
    )
};

export default App;
