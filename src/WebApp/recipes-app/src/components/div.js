import React from 'react';
import Spinner from 'react-spinkit';
import classNames from 'classnames';

const Div = (props) => {
    const overlayStyles = {};

    var hasContent = React.Children.count(props.children);
    if(hasContent) {
        overlayStyles.position = 'absolute';
        overlayStyles.zIndex = '10';
        overlayStyles.top = props.loadingOffset;
        overlayStyles.display = 'block';
    } else {
        overlayStyles.marginTop = props.loadingOffset;
        overlayStyles.marginBottom = props.loadingOffset;
    }

    const classnames = classNames(props.className, {faded: hasContent && props.isLoading});

    const spinner = (props.isLoading)? (<div className="text-center" style={ overlayStyles }><Spinner spinnerName='tree-bounce' noFadeIn={true} /></div>) : null;

    return (
        <div className={classnames}>
            {spinner}
            {props.children}
        </div>

    );
};

export default Div;