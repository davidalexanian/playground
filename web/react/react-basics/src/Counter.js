import React from 'react'

export default class Counter extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: parseInt(props.initialValue)
        }
        this.increment = this.increment.bind(this);
        this.decrement = this.decrement.bind(this);
    }

    increment() {
        this.setState((prevState, props) => {
            return {
                value : ++prevState.value
            }            
        });
    }
    decrement() {
        this.setState(function(prevState, prop) {
            return {
                value : --prevState.value
            }
        });
    }

    render() {
        return (
            <div>
                <span>{this.state.value}</span>
                <button onClick={this.increment}>+</button>
                <button onClick={this.decrement}>-</button>
            </div>
        )
    }
}