import noImage from '../../public/img/no-image.png';

import React from 'react';
import { Link }from 'react-router';
import { getTimeForUI } from './../utils/utils';
import userActivityLogger from './../utils/userActivityLogger';

const RecipeOverview = ({ id, title, cookTimeInMinutes, prepTimeInMinutes, imageUrl, isVegetarian, displayedRecipeId, recommenderType, params }) => {
    var isVegetarianClass = (isVegetarian)? 'veggie' : null;


    var getObjectToLog = () => {
        console.log('getObjectToLog.props', params)
        return {
            displayedRecipeId: displayedRecipeId,
            clickedRecipeId: id,
            recommendedBy: recommenderType,
            timestamp: new Date().toISOString()
        }
    };

    return (
        <div className="row recommendation">
            <div className="col-xs-3">
                <img src={ imageUrl || noImage } className="img-responsive img-circle img-polaroid teaser-image" alt="recipe"/>
            </div>
            <div className="col-xs-9">
                <p><Link to={`/Recipes/${id}`} className="title" onClick={() => userActivityLogger(getObjectToLog())}>{ title }</Link><span className={isVegetarianClass}></span></p>
                <p> Cooking: { getTimeForUI(cookTimeInMinutes) }</p>
                <p> Preparation: { getTimeForUI(prepTimeInMinutes) }</p>
            </div>
        </div>
    );
};

export default RecipeOverview;