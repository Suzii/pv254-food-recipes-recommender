import React from 'react';
import Spinner from 'react-spinkit';

const SafeDiv = (props) => {
    const overlay = (props.isLoading)?
        (<div className="text-center">
                <Spinner spinnerName='tree-bounce' noFadeIn={true}/>
            </div>
        ) : null;

    return (
        <div>
            {overlay}
            {props.children}
        </div>

    );
}

export default SafeDiv;