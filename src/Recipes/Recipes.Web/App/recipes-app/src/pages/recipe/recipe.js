import React from 'react';

const Recipe = (props) => {
    fetch('/api/recipes/6', {accept: 'application/json'})
        .then(response => response.json())
        .then(data => {
            console.log(data);
            document.getElementById('recipe').innerHTML = JSON.stringify(data, null, 2);
        });
    return (
        <div>
            <h1>Recipe {props.params.recipeId} </h1>
            <div id="recipe"></div>
        </div>);
};

export default Recipe;