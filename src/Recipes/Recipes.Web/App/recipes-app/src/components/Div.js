import React from 'react';
import Spinner from 'react-spinkit';
import classnames from 'classnames';

const Div = (props) => {
    const classnames = null;//classnames(props.className);
    console.log(props);
    const overlay = (props.isLoading)
        ? (<div className="text-center">
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