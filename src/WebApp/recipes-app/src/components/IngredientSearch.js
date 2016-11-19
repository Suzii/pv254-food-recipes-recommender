import React from 'react';
import {connect} from 'react-redux';
import AutosuggestSearch from './AutosuggestSearch';

import PureComponent from './PureComponent';
import {fetchIngredientDatabaseIfNeeded} from '../redux/actionCreators';
import {isNullOrEmpty} from './../utils/arrays.js';

class IngredientSearch extends React.Component {
    static propTypes = {
        ingredientDatabase: React.PropTypes.arrayOf(React.PropTypes.shape({
            id: React.PropTypes.number.isRequired,
            name: React.PropTypes.string.isRequired
        })).isRequired,
        isFetching: React.PropTypes.bool.isRequired,
        fetchIngredientDatabase: React.PropTypes.func.isRequired,
    };

    _onIngredientSelected(id) {
        // TODO
        console.log('ingredient selected', id);
    }

    render() {
        return (
            <AutosuggestSearch
                isFetching={this.props.isFetching}
                onSelected={(id) => this._onIngredientSelected(id)}
                fetchAllSuggestions={ this.props.fetchIngredientDatabase }
                allSuggestions={this.props.ingredientDatabase}
                placeholder="Spaghetti..."
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

const IngredientSearchWrapper = connect(mapStateToProps, mapDispatchToProps)(IngredientSearch);

export default PureComponent(IngredientSearchWrapper);