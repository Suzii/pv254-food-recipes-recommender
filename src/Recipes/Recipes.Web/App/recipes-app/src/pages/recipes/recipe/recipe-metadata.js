import React from 'react';

const RecipeMetadata = ({ cookTimeInMinutes, prepTimeInMinutes, chef, programmeName, recipeYield, isVegetarian }) => {

    let calculateTime = (timeInMinutes) => {
        if(timeInMinutes === 60) {
            return 'up to 1 hour';
        }

        if(timeInMinutes >= 60) {
            let time = Math.floor(timeInMinutes/60);
            return `${time} to ${time+1} hours`;
        }

        return `less than ${timeInMinutes} minutes`

    };

    let preparationTime = calculateTime(prepTimeInMinutes);
    let cookTime = calculateTime(cookTimeInMinutes);
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
