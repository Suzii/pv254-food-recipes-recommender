import React from 'react';
import YouMayLike from './recipe/recommendations/you-may-like';
import IngredientBasedSearch from './../../components/ingredient-based-search';

const Index = (props) => {
    return (
        <div>
            <h1 className="text-center text-capitalize">Find the right recipe for you</h1>

            <div className="row">
                <div className="col-xs-12 col-sm-8 col-md-8">
                    <IngredientBasedSearch/>
                </div>

                <div className="col-xs-12 col-sm-4 col-md-4">
                    <YouMayLike params={ { recipeId: "0" } }/>
                </div>
            </div>
        </div>
    );
};

export default Index;