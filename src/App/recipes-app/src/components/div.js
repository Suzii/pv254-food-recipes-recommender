import React from 'react';
import Spinner from 'react-spinkit';
import classNames from 'classnames';

const Div = (props) => {
    const classnames = classNames(props.className);
    const overlayStyles = {
        paddingTop: props.loadingOffset,
        paddingBottom: props.loadingOffset
    };

    const overlay = (props.isLoading)
        ? (<div className="text-center" style={ overlayStyles }>
        <Spinner spinnerName='tree-bounce' noFadeIn={true}/>
    </div>)
        : null;

    return (
        <div className={classnames}>
            {overlay}
            {props.children}
        </div>

    );
}

export default Div;