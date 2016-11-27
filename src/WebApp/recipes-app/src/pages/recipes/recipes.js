import React from 'react';

const Recipes = (props) => {
    return (
        <div className="recipes" id="rcipes">
            {props.children}
        </div>);
};

export default Recipes;