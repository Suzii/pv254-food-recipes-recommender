import React from 'react';
import RecipeOverview from './../../../../components/YouMayLikeRecommendation';
import Div from '../../../../components/div';
import PureComponent from '../../../../components/PureComponent.js';

class YouMayLike extends React.Component {
    render() {
        const recipesList = this.props.recipes || [];
        const recipes = recipesList.map((recipe, index) => <RecipeOverview key={index} {...recipe}  displayedRecipeId={this.props.currentRecipeId} />);
        const isLoading = !this.props.recipes || !this.props.recipes.length;
        return (
            <div className="you-may-like__recommendations">
                <div className="row">
                    <h2 className="text-center">You may like</h2>
                    <Div isLoading={isLoading} loadingOffset="100px" className="">
                        <div className="row">
                            { recipes.slice(0,3) }
                        </div>
                        <div className="row">
                            { recipes.slice(3,6) }
                        </div>
                    </Div>
                </div>
            </div>
        );
    }
}

export default PureComponent(YouMayLike);