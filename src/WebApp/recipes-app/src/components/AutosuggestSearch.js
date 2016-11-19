import React from 'react';
import Autosuggest from 'react-autosuggest';
import IsolatedScroll from 'react-isolated-scroll';

import PureComponent from './PureComponent';
import escapeRegex from '../utils/regex.js';
import { wrapSubstringInBoldTag } from '../utils/string.js';

class AutosuggestSearch extends React.Component {
    static propTypes = {
        allSuggestions: React.PropTypes.arrayOf(React.PropTypes.shape({
            id: React.PropTypes.number.isRequired,
            name: React.PropTypes.string.isRequired
        })),
        isFetching: React.PropTypes.bool.isRequired,
        name: React.PropTypes.string.isRequired,
        id: React.PropTypes.string.isRequired,
        className: React.PropTypes.string.isRequired,
        onSelected: React.PropTypes.func.isRequired,
        fetchAllSuggestions: React.PropTypes.func.isRequired,
        placeholder: React.PropTypes.string
    };

    constructor(props) {
        super(props);
        this.state = {
            value: '',
            suggestions: [],
        };
    }

    componentWillMount() {
        this.props.fetchAllSuggestions();
    }

    _getSuggestions(value) {
        const inputValue = escapeRegex(value.trim().toLowerCase());
        if(inputValue.length === 0) {
            return [];
        }

        const regex = new RegExp('' + inputValue, 'i');

        return this.props.allSuggestions.filter(recipe => regex.test(recipe.name));
    };

    _renderSuggestion(suggestion) {
        var suggestionText = wrapSubstringInBoldTag(suggestion.name, this.state.value);
        return (
            <div dangerouslySetInnerHTML={{__html: suggestionText}} />
        );
    }

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

    _onSuggestionSelected(event, suggestion) {
        event.stopPropagation();
        event.preventDefault();
        this.props.onSelected(suggestion.id);
    }

    render() {
        const inputProps = {
            name: this.props.name,
            id: this.props.id,
            type: 'text',
            placeholder: this.props.placeholder || 'Search...',
            className: this.props.className || 'form-control',
            value: this.state.value,
            disabled: this.props.isFetching,
            onChange: (e, { newValue }) => this.setState({value: newValue})
        };

        return (
            <Autosuggest
                suggestions={this.state.suggestions}
                onSuggestionsFetchRequested={({value}) => this.setState({suggestions: this._getSuggestions(value)})}
                onSuggestionsClearRequested={() => this.setState({suggestions: []})}
                getSuggestionValue={(suggestion) => suggestion.name}
                renderSuggestion={(suggestion) => this._renderSuggestion(suggestion)}
                renderSuggestionsContainer={this._renderSuggestionsContainer}
                inputProps={inputProps}
                onSuggestionSelected={(event, {suggestion}) => this._onSuggestionSelected(event, suggestion)}
                shouldRenderSuggestions={(value) => value.trim().length > 2}
                />
        );
    }
}

export default PureComponent(AutosuggestSearch );