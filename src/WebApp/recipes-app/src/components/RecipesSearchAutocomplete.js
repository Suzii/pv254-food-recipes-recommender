import React from 'react';
import {connect} from 'react-redux';
import AutosuggestSearch from './AutosuggestSearch';
import {browserHistory} from 'react-router';

import PureComponent from './PureComponent';
import {fetchRecipeDatabaseIfNeeded} from '../redux/actionCreators';
import userActivityLogger from './../utils/userActivityLogger';
import * as RecommenderTypes from '../utils/recommenderTypes.js';
import {isNullOrEmpty} from '../utils/arrays.js';

class RecipeSearchAutocomplete extends React.Component {
    static propTypes = {
        recipeDatabase: React.PropTypes.arrayOf(React.PropTypes.shape({
            id: React.PropTypes.number.isRequired,
            name: React.PropTypes.string.isRequired
        })).isRequired,
        isFetching: React.PropTypes.bool.isRequired,
        fetchRecipeDatabase: React.PropTypes.func.isRequired,
    };

    _onRecipeSelected(id) {
        userActivityLogger(null, id, [RecommenderTypes.RECIPE_SEARCH])
        browserHistory.push(`/recipes/${id}`);
    }

    render() {
        return (
            <form className="form" onSubmit={e => e.preventDefault()}>
                    <label hidden htmlFor="searchByName">Search by name autocomplete</label>
                    <AutosuggestSearch
                        name="searchByName"
                        id="searchByName"
                        className="form-control col-lg-3"
                        isFetching={this.props.isFetching}
                        onSelected={(id) => this._onRecipeSelected(id)}
                        fetchAllSuggestions={ this.props.fetchRecipeDatabase }
                        allSuggestions={this.props.recipeDatabase}
                        placeholder="Search for recipes..."
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
        isFetching: isNullOrEmpty(store.recipeDatabase),
        recipeDatabase: store.recipeDatabase,
    }
};

const RecipeSearchAutocompleteWrapper = connect(mapStateToProps, mapDispatchToProps)(RecipeSearchAutocomplete);

export default PureComponent(RecipeSearchAutocompleteWrapper);