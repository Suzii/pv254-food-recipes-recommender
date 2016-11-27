import React from 'react';
import { getTimeForUILonger } from './../../../utils/utils';

const RecipeMetadata = ({ cookTimeInMinutes, prepTimeInMinutes, chef, programmeName, recipeYield, isVegetarian }) => {

    let preparationTime = getTimeForUILonger(prepTimeInMinutes);
    let cookTime = getTimeForUILonger(cookTimeInMinutes);
    let diet = (isVegetarian)
        ? <p className="veggie"> Vegetarian </p>
        : null;
    let serves = <p className="serves">{ recipeYield } portions</p>;

    return (
        <div className="metadata" id="metadata">
            <dl>
                <dt>Preparation time</dt>
                <dd>{ preparationTime }</dd>

                <dt>Cook time</dt>
                <dd>{ cookTime }</dd>

                <dt>By</dt>
                <dd>{ chef }</dd>

                <dt>From</dt>
                <dd> { programmeName } </dd>
            </dl>

            { serves }
            { diet }
        </div>
    );
};

export default RecipeMetadata;
