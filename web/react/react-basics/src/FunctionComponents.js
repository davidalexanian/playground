import React from 'react'

export function WelcomeFunction(props) {
    return <h3>Hello, {props.name}</h3>;
}

export class WelcomeClass extends React.Component {
    render() {
        return <h3>Hello, {this.props.name || 'unknown'}</h3>;
    }
}

export default WelcomeClass;