import React from 'react';
import SafeDiv from './../../components/SafeDiv';

const Ingredients = (props) => {
    if(!props.ingredients) {
        return null;
    }

    const getIngredient = (ingredient) => {
        return <li>{ ingredient.fullText }</li>;
    }

    return (
        <ul>
            { props.ingredients.map(i => getIngredient(i)) }
        </ul>
    );
}

const Instructions = (props) => {
    if(!props.instructions) {
        return null;
    }

    const getInstruction = (instruction, index) => {
        return <li key={index}>{instruction} </li>;
    }

    return (
        <ol>
            {props.instructions.map((instruction, index) => getInstruction(instruction, index))}
        </ol>
    );
}


class Recipe extends  React.Component {

    static propTypes = {
        recipeId: React.PropTypes.number.isRequired
    }
    static defaultProps = {
        recipeId: 6
    }
    state = {
        isLoading: true
    }

    constructor(props) {
        super(props);
        fetch(`/api/recipes/${props.recipeId}`, {accept: 'application/json'})
            .then(response => response.json())
            .then(data => {
                setTimeout(() => this.recipeRecieved(data), 1100);
            });
    }

    recipeRecieved(data) {
        console.log(data);
        this.setState({
            isLoading: false,
            recipe: data
        });
    }

    render() {
        var recipe = this.state.recipe;
        console.log(recipe);

        if(!recipe) {
            return <SafeDiv isLoading={true}/>
        }

        return (
            <SafeDiv isLoading={this.state.isLoading}>
                <h1>{ recipe.title }</h1>
                <div>
                    <h2>Ingredients </h2>
                    <Ingredients ingredients={ recipe.inredients }/>


                    <h2>Instructions</h2>
                    <Instructions instructions={ recipe.instructions }/>
                </div>


            </SafeDiv>);
    }
};

export default Recipe;