import React from 'react';
import {connect} from 'react-redux';
import Autosuggest from 'react-autosuggest';
import IsolatedScroll from 'react-isolated-scroll';
import {browserHistory} from 'react-router';

import PureComponent from './PureComponent';
import {fetchRecipeDatabaseIfNeeded} from '../redux/actionCreators';
import userActivityLogger from './../utils/userActivityLogger';
import escapeRegex from '../utils/regex.js';
import { wrapSubstringInBoldTag } from '../utils/string.js';
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
        const inputValue = escapeRegex(value.trim().toLowerCase());
        const regex = new RegExp('' + inputValue, 'i');
        const inputLength = inputValue.length;

        return inputLength === 0 ? [] : this.props.recipeDatabase.filter(recipe => regex.test(recipe.name));
    };

    _getSuggestionValue(suggestion) {
        return suggestion.name;
    }

    _renderSuggestion(suggestion) {
        var suggestionText = wrapSubstringInBoldTag(suggestion.name, this.state.value);
        return (
            <div dangerouslySetInnerHTML={{__html: suggestionText}} />
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

    _renderSuggestionsContainer({ref, ...rest}) {
        const callRef = isolatedScroll => {
            if (isolatedScroll !== null) {
                ref(isolatedScroll.component);
            }
        };

        return (
            <IsolatedScroll {...rest} ref={callRef}/>
        );
    }

    _onRecipeSelected(id) {
        // note that 6 is id of first recipe in db and this cannot be null,
        // coz I cant apply stupid migration to make the column nullable
        userActivityLogger(6, id, [RecommenderTypes.RECIPE_SEARCH])
        browserHistory.push(`/recipes/${id}`);
    }

    _shouldRenderSuggestions(value) {
        return value.trim().length > 2;
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
                    renderSuggestionsContainer={this._renderSuggestionsContainer}
                    inputProps={inputProps}
                    onSuggestionSelected={(event, {suggestion}) => this._onRecipeSelected(suggestion.id)}
                    shouldRenderSuggestions={this._shouldRenderSuggestions}
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