import React from 'react';

const Recipes = (props) => {
    return (
        <div className="container">
            {props.children}
        </div>);
};

export default Recipes;