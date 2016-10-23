import React from 'react';

const Ingredient = ({ id, subcategory, freeText, ingredient }) => {
    var ingredientName = ingredient.name;
    let text = freeText.replace(ingredientName, `<b>${ingredientName}</b>`);

    return (
        <li dangerouslySetInnerHTML={ {__html: text} }/>
    );
};

const Ingredients = ({ ingredients }) => {
    const ingredientsCode = (ingredients)
        ? <ul>{ ingredients.map((ingredient, index) => <Ingredient key={index} { ...ingredient } />) }</ul>
        : 'Nothing found... :(';

    return (
        <div className="ingredients" id="ingredients">
            <h2>Ingredients </h2>
            { ingredientsCode }
        </div>
    );
};

export default Ingredients;