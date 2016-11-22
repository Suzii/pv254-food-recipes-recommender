import noImage from '../../public/img/no-image.png';

import React from 'react';
import { Link }from 'react-router';
import { getTimeForUILonger } from './../utils/utils';
import userActivityLogger from './../utils/userActivityLogger';

const RecipeOverview = ({ id, title, cookTimeInMinutes, prepTimeInMinutes, imageUrl, isVegetarian, displayedRecipeId, recommendedBy, params }) => {
    var isVegetarianClass = (isVegetarian)? 'veggie' : null;
    var recommenderTypes = recommendedBy.map(t => (<div key={t} className={`recommender-type recommender-type-${t}`}>&nbsp;</div>));

    if(recommendedBy.length > 1) {
        console.log('Hurray, recipe with overlapping recommendations', displayedRecipeId, 'recommends', id);
    }

    return (
        <div className="row recommendation">
            <div className="col-xs-3">
                {recommenderTypes}
                <img src={ imageUrl || noImage } className="img-responsive teaser-image" alt="recipe"/>
            </div>
            <div className="col-xs-9">
                <p><Link to={`/Recipes/${id}`} className="title" onClick={() => userActivityLogger(displayedRecipeId, id, recommendedBy)}>{ title }</Link><span className={isVegetarianClass}></span></p>
                <p> Cooking: { getTimeForUILonger(cookTimeInMinutes) }</p>
                <p> Preparation: { getTimeForUILonger(prepTimeInMinutes) }</p>
            </div>
        </div>
    );
};

export default RecipeOverview;