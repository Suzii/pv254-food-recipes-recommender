import React from 'react';
import IngredientBasedSearch from '../../components/IngredientBasedSearchForm';
import RecipeSearchAutocomplete from '../../components/RecipesSearchAutocomplete';

const Index = (props) => {
    return (
        <div>
            <h1 className="text-center text-capitalize">Find the right recipe for you</h1>

            <div className="row">
                <div className="col-xs-12 col-sm-8 col-md-8">
                    <IngredientBasedSearch/>
                </div>

                <div className="col-xs-12 col-sm-4 col-md-4">
                    TODO top 10
                </div>
            </div>
        </div>
    );
};

export default Index;