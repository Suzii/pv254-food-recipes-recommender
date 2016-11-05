import shallowEquals from 'shallow-equals';

function shouldComponentUpdate(nextProps, nextState) {
    var shouldUpdate = !shallowEquals(nextProps, this.props) || !shallowEquals(nextState, this.state);
    return shouldUpdate;
}

function PureComponent(target) {
    target.prototype.shouldComponentUpdate = shouldComponentUpdate;
    return target;
}

export default PureComponent;