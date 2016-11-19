import React from 'react';
import RecipeOverview from '../../../../components/RecipeOverview';
import Div from '../../../../components/Div';
import PureComponent from '../../../../components/PureComponent.js';

class SimilarRecipes extends React.Component {

    render() {
        const recipesList = this.props.recipes || [];
        const recipes = recipesList.map((recipe, index) => <RecipeOverview key={index} {...recipe}  displayedRecipeId={this.props.currentRecipeId} />);
        const isLoading = !this.props.recipes || !this.props.recipes.length;

        return (
            <div>
                <h2>Similar recipes</h2>
                <Div isLoading={isLoading} loadingOffset="100px" className="recommendations recommendations-right">
                    { recipes }
                </Div>
            </div>
        );
    }
}

export default PureComponent(SimilarRecipes);