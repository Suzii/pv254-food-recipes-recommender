import React from 'react';
import RecipeOverview from './../../../../components/recipe-overview';
import Div from '../../../../components/div';

class YouMayLike extends React.Component {

    render() {
        const recipes = this.props.recipes.map((recipe, index) => <RecipeOverview key={index} {...recipe}  displayedRecipeId={this.props.currentRecipeId} />);
        const isLoading = !this.props.recipes || !this.props.recipes.length;
        return (
            <div>
                <h2>You may like</h2>
                <Div isLoading={isLoading} loadingOffset="100px" className="recommendations recommendations-right">
                    { recipes }
                </Div>
            </div>
        );
    }
}

export default YouMayLike;