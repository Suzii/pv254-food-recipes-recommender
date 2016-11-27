import React from 'react';
import {connect} from 'react-redux';
import {browserHistory} from 'react-router';

import {searchByRecipeName} from '../redux/actionCreators.js';

class NavBarRecipeSearchForm extends React.Component {

    static propTypes = {
        search: React.PropTypes.func.isRequired,
    };

    constructor(props) {
        super(props);
        this.input = null;
    }

    render() {
        return (
            <form className="navbar-form navbar-right" onSubmit={e => this.submit(e)}>
                <input
                    className="form-control"
                    type="search"
                    placeholder="Search for recipes..."
                    ref={(input) => this.input = input}
                />
            </form>
        );
    }

    submit(event) {
        event.preventDefault();

        const q = this.input.value;
        this.props.search(q);

        console.log('Submitting', q);
        browserHistory.push(`/search?q=${q}`);
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        search: (query) => dispatch(searchByRecipeName(query)),
    }
};

export default connect((store) => { return {}}, mapDispatchToProps)(NavBarRecipeSearchForm);