import React from 'react';

const isNonsenseString = (s) => !s || s === 'null';

const Ingredient = ({ id, subCategory, freeText, ingredient }) => {
    var ingredientName = ingredient.name;
    let text = freeText.replace(ingredientName, `<b>${ingredientName}</b>`);

    return (
        <li dangerouslySetInnerHTML={ {__html: text} }/>
    );
};

const IngredientsInSubCategory = ({ subCategory, ingredients }) => {
    const ingredientsCode = (ingredients)
        ? <ul>{ ingredients.map((ingredient, index) => <Ingredient key={index} { ...ingredient } />) }</ul>
        : 'Nothing found... :(';

    const subCategoryHeading = (isNonsenseString(subCategory))? 'Other' : subCategory;

    return (
        <div>
            <h4>{ subCategoryHeading }</h4>
            { ingredientsCode }
        </div>
    );
}

function sortIngredientsBySubCategories(ingredients) {
    const ingredientsBySubCategory = {};
    for(let i = 0; i < ingredients.length; i++) {
        let ingredient = ingredients[i];
        let ingredientsInSubcategory = ingredientsBySubCategory[ingredient.subCategory];
        if(!ingredientsInSubcategory) {
            ingredientsBySubCategory[ingredient.subCategory] = [];
        }

        ingredientsBySubCategory[ingredient.subCategory].push(ingredient);
    }

    return ingredientsBySubCategory;
}

const Ingredients = ({ ingredients }) => {
    const ingredientsBySubCategories = sortIngredientsBySubCategories(ingredients);
    const ingredientsBySubCategoryCode = (!ingredientsBySubCategories)? null : Object.keys(ingredientsBySubCategories).map((subCategory, index) => (
        <IngredientsInSubCategory subCategory={subCategory} ingredients={ingredientsBySubCategories[subCategory]} key={index} />
    ));

    return (
        <div className="ingredients" id="ingredients">
            <h2>Ingredients </h2>
            { ingredientsBySubCategoryCode }
        </div>
    );
};

export default Ingredients;