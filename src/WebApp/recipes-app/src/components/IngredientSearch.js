import React from 'react';
import {connect} from 'react-redux';
import AutosuggestSearch from './AutosuggestSearch';

import PureComponent from './PureComponent';
import {fetchIngredientDatabaseIfNeeded} from '../redux/actionCreators';
import {isNullOrEmpty} from './../utils/arrays.js';

class IngredientSearchAutocomplete extends React.Component {
    static propTypes = {
        ingredientDatabase: React.PropTypes.arrayOf(React.PropTypes.shape({
            id: React.PropTypes.number.isRequired,
            name: React.PropTypes.string.isRequired
        })).isRequired,
        isFetching: React.PropTypes.bool.isRequired,
        fetchIngredientDatabase: React.PropTypes.func.isRequired,
        onIngredientSelected: React.PropTypes.func.isRequired
    };

    render() {
        return (
            <AutosuggestSearch
                isFetching={this.props.isFetching}
                onSelected={(id, name) => this.props.onIngredientSelected(id, name)}
                fetchAllSuggestions={ this.props.fetchIngredientDatabase }
                allSuggestions={this.props.ingredientDatabase}
                placeholder="Avocado, beef and tortillas..."
                name="ingredient-search"
                id="ingredient-search"
                className="form-control"
                clearAfterSelected={true}
                />
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        fetchIngredientDatabase: () => dispatch(fetchIngredientDatabaseIfNeeded()),
    }
};

const mapStateToProps = (store) => {
    return {
        isFetching: isNullOrEmpty(store.ingredientDatabase),
        ingredientDatabase: store.ingredientDatabase,
    }
};

const IngredientSearchAutocompleteWrapper = connect(mapStateToProps, mapDispatchToProps)(IngredientSearchAutocomplete);

export default PureComponent(IngredientSearchAutocompleteWrapper);