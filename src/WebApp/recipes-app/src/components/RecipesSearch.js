import React from 'react';
import {connect} from 'react-redux';
import Autosuggest from 'react-autosuggest';
import {browserHistory} from 'react-router';


import PureComponent from './PureComponent';
import {fetchRecipeDatabaseIfNeeded} from '../redux/actionCreators';
import userActivityLogger from './../utils/userActivityLogger';
import * as RecommenderTypes from '../utils/recommenderTypes.js';

class RecipeSearch extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: '',
            suggestions: [],
        };
    }

    componentWillMount() {
        this.props.fetchRecipeDatabase();
    }

    _getSuggestions(value) {
        const inputValue = value.trim().toLowerCase();
        const inputLength = inputValue.length;

        return inputLength === 0 ? [] : this.props.recipeDatabase.filter(recipe =>
            recipe.name.toLowerCase().slice(0, inputLength) === inputValue
        );
    };

    _getSuggestionValue(suggestion) {
        return suggestion.name;
    }

    _renderSuggestion(suggestion) {
        return (
            <div>
                {suggestion.name}
            </div>
        );
    }

    _onChange = (event, {newValue}) => {
        this.setState({
            value: newValue
        });
    };

    _onSuggestionsFetchRequested = (value) => {
        this.setState({
            suggestions: this._getSuggestions(value)
        });
    };

    _onSuggestionsClearRequested = () => {
        this.setState({
            suggestions: []
        });
    };

    //
    _onRecipeSelected(id) {
        // note that 6 is id of first recipe in db and this cannot be null,
        // coz I cant apply stupid migration to make the column nullable
        userActivityLogger(6, id, [RecommenderTypes.RECIPE_SEARCH])
        browserHistory.push(`/recipes/${id}`);
    }

    render() {
        const inputProps = {
            placeholder: 'Search for recipe...',
            className: 'form-control',
            value: this.state.value,
            onChange: (e, args) => this._onChange(e, args)
        };

        return (
            <form className="navbar-form navbar-right" onSubmit={e => e.preventDefault()}>
                <Autosuggest
                    suggestions={this.state.suggestions}
                    onSuggestionsFetchRequested={({value}) => this._onSuggestionsFetchRequested(value)}
                    onSuggestionsClearRequested={() => this._onSuggestionsClearRequested}
                    getSuggestionValue={(suggestion) => this._getSuggestionValue(suggestion)}
                    renderSuggestion={(suggestion) => this._renderSuggestion(suggestion)}
                    inputProps={inputProps}
                    onSuggestionSelected={(event, { suggestion }) => this._onRecipeSelected(suggestion.id)}
                />
            </form>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        fetchRecipeDatabase: () => dispatch(fetchRecipeDatabaseIfNeeded()),
    }
};

const mapStateToProps = (store) => {
    return {
        isFetching: store.currentRecipe.isFetching,
        recipeDatabase: store.recipeDatabase,
    }
};

const RecipeSearchWrapper = connect(mapStateToProps, mapDispatchToProps)(RecipeSearch);

export default PureComponent(RecipeSearchWrapper);