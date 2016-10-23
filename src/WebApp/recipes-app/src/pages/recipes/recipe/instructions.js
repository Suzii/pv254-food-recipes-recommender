import React from 'react';

const Instructions = ({ instructions }) => {
    const instructionsCode = (instructions)
        ? <ol> { instructions.map((instruction, index) => <li key={index}>{instruction} </li>) } </ol>
        : 'No instructions found... :(';

    return (
        <div className="instructions" id="instructions">
            <h2>Instructions</h2>
            { instructionsCode }
        </div>
    );
};

export default Instructions;
