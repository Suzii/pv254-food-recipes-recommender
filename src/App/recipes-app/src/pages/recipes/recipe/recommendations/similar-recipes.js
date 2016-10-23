import React from 'react';
import RecipeOverview from './../../../../components/recipe-overview';
import Div from '../../../../components/div';


class SimilarRecipes extends React.Component {
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
        fetch(`/api/recipes/${props.params.recipeId}`, {accept: 'application/json'})
            .then(response => response.json())
            .then(data => {
                setTimeout(() => this.recipeReceived([data, data, data, data, data, data]), 1100);
            });
    }

    recipeReceived(data) {
        console.log('Recipe: ', data);
        this.setState({
            isLoading: false,
            recipes: data
        });
    }

    render() {
        const recipes = this.state.recipes.map((recipe, index) => <RecipeOverview key={index} {...recipe} />);
        console.log(this.state.recipes);

        return (
            <div>
                <h2>Similar recipes</h2>
                <Div isLoading={this.state.isLoading} loadingOffset="100px" className="recommendations recommendations-right">
                    { recipes }
                </Div>
            </div>
        );
    }
}

export default SimilarRecipes;