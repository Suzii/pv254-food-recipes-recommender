import noImage from '../../public/img/no-image.png';

import React from 'react';
import { Link }from 'react-router';
import { getTimeForUIShort } from './../utils/utils';
import userActivityLogger from './../utils/userActivityLogger';

const YouMayLikeRecommendation = ({ id, title, cookTimeInMinutes, prepTimeInMinutes, imageUrl, isVegetarian, chef, displayedRecipeId, recommenderType, params }) => {
    var isVegetarianClass = (isVegetarian)? 'veggie' : null;

    var getObjectToLog = () => {
        return {
            displayedRecipeId: displayedRecipeId,
            clickedRecipeId: id,
            recommendedBy: [recommenderType],
            timestamp: new Date().toISOString()
        }
    };

    return (
        <div className="you-may-like__recommendation">
            <div className="col-xs-3 col-md-1">
                <img src={ imageUrl || noImage } className="img-responsive img-circle img-polaroid teaser-image" alt="recipe"/>
            </div>
            <div className="col-xs-9 col-md-3">
                <p><Link to={`/Recipes/${id}`} className="title" onClick={() => userActivityLogger(getObjectToLog())}>{ title }</Link><span className={isVegetarianClass}></span></p>
                <p> Time: { getTimeForUIShort(prepTimeInMinutes) + ' + ' + getTimeForUIShort(cookTimeInMinutes)}</p>
                <p> By: { chef }</p>
            </div>
        </div>
    );
};

export default YouMayLikeRecommendation;