import React from 'react';
import Div from '../../components/Div';

const Ingredients = ({ ingredients }) => {
    // make important part of ingredient bold
    const getIngredient = (ingredient, index) => {
        return <li key={index}>{ ingredient.fullText }</li>;
    }

    const ingredientsCode = (ingredients)
        ? <ul>{ ingredients.map((ingredient, index) => getIngredient(ingredient, index)) }</ul>
        : 'Nothing found... :(';

    return (
        <div className="ingredients" id="ingredients">
            <h2>Ingredients </h2>
            { ingredientsCode }
        </div>
    );
}

const Instructions = ({ instructions }) => {
    const instructionsCode = (instructions)
        ? <ol> { instructions.map((instruction, index) => <li key={index}>{instruction} </li>) } </ol>
        : 'No instructions found... :(';

    return (
        <div className="instructions" id="instructions">
            <h2>Instructions</h2>
            { instructionsCode }
        </div>
    );
}

const RecipeMetadata = ({ cookTimeInMinutes, prepTimeInMinutes, chef, programmeName, recipeYield, isVegetarian }) => {

    let calculateTime = (timeInMinutes) => {
        if(timeInMinutes >= 60) {
            let time = timeInMinutes%60;
            return `${time} to ${time+1} hours`;
        } else {
            return `less than ${timeInMinutes} minutes`
        }
    }

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
    )
}

class Recipe extends React.Component {

    static propTypes = {
        params: React.PropTypes.shape({ recipeId: React.PropTypes.number })
    }
    state = {
        isLoading: true
    }

    constructor(props) {
        super(props);
        console.log(props);
        fetch(`/api/recipes/${props.params.recipeId}`, {accept: 'application/json'})
            .then(response => response.json())
            .then(data => {
                setTimeout(() => this.recipeReceived(data), 1100);
            });
    }

    recipeReceived(data) {
        console.log(data);
        this.setState({
            isLoading: false,
            recipe: data
        });
    }

    render() {
        var recipe = this.state.recipe;
        console.log(recipe);

        if (!recipe) {
            return <Div isLoading={true}/>
        }

        return (
            <Div isLoading={this.state.isLoading} className="recipe" id="recipe">
                <h1>{ recipe.title }</h1>
                <div className="row">
                    <div className="col-xs-12 col-sm-8 col-md-8 col-lg-6">

                        <Ingredients ingredients={ recipe.inredients }/>

                    </div>

                    <div className="col-xs-12 col-sm-4 col-md-4 col-lg-6">
                        <RecipeMetadata { ...recipe } />
                    </div>
                </div>

                <div className="row">
                    <div className="col-xs-12">
                        <Instructions instructions={ recipe.instructions }/>
                    </div>
                </div>
            </Div>);
    }
}

export default Recipe;