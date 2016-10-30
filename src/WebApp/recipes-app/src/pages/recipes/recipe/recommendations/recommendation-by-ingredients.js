import React from 'react';
import RecipeOverview from './../../../../components/recipe-overview';
import Div from '../../../../components/div';
import * as AjaxUtils from '../../../../utils/ajax';

class RecommendationByIngredients extends React.Component {
    static propTypes = {
        params: React.PropTypes.shape({recipeId: React.PropTypes.string})
    }
    state = {
        isLoading: true,
        recipes: []
    }

    constructor(props) {
        super(props);
        //TODO change to proper endpoint and move to Redux
        let params = {
            currentRecipeId: props.params.recipeId,
            pageSize: 5
        };

        let paramsString = AjaxUtils.getQueryParameters(params);

        fetch(`/api/recommendations/IngredientBased?${paramsString}`, {accept: 'application/json'})
            .then(AjaxUtils.processStatus)
            .then(AjaxUtils.parseJson)
            .then(data => {
                setTimeout(() => this.recipeReceived(data), 1100);
            })
            .catch(AjaxUtils.logNetworkError);
    }

    recipeReceived(data) {
        this.setState({
            isLoading: false,
            recipes: data
        });
    }

    render() {
        const recipes = this.state.recipes.map((recipe, index) => <RecipeOverview key={index} {...recipe} />);

        return (
            <div>
                <h2>Similar ingredients</h2>
                <Div isLoading={this.state.isLoading} loadingOffset="100px" className="recommendations recommendations-right">
                    { recipes }
                </Div>
            </div>
        );
    }
}

export default RecommendationByIngredients;