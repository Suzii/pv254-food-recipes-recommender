function shouldComponentUpdate(nextProps, nextState) {
    //return !shallowEquals(nextProps, this.props) || !shallowEquals(nextState, this.state);
    var shouldUpdate = (nextProps !== this.props) || (nextState !== this.state);
    console.log('should component update', shouldUpdate, this);
    return shouldUpdate;
}

function PureComponent(target) {
    target.prototype.shouldComponentUpdate = shouldComponentUpdate;
    return target;
}

export default PureComponent;