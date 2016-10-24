import noImage from '../../public/img/no-image.png';

import React from 'react';
import { Link }from 'react-router';
import { getTimeForUI } from './../utils/utils';

const RecipeOverview = ({ id, title, cookTimeInMinutes, prepTimeInMinutes, imageUrl, isVegetarian }) => {
    var isVegetarianClass = (isVegetarian)? 'veggie' : null;

    return (
        <div className="row recommendation">
            <div className="col-xs-3">
                <img src={ imageUrl || noImage } className="img-responsive img-circle img-polaroid teaser-image" alt="recipe"/>
            </div>
            <div className="col-xs-9">
                <p><Link to={`/Recipes/${id}`} className="title">{ title }</Link><span className={isVegetarianClass}></span></p>
                <p> Cooking: { getTimeForUI(cookTimeInMinutes) }</p>
                <p> Preparation: { getTimeForUI(prepTimeInMinutes) }</p>
            </div>
        </div>
    );
};

export default RecipeOverview;