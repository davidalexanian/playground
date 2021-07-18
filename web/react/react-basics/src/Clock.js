import React from 'react';
import PropTypes from 'prop-types';

export default class Clock extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            greetingMessage: props.greetingMessage,
            user: props.user,
            date: new Date()
        };
    }
    componentDidMount() {
        this.intervalId = setInterval(() => {
            this.setState({ date: new Date() });
        }, 1000);

    }
    componentWillUnmount() {
        clearInterval(this.intervalId);
    }
    render() {
        return (
            <div>
                <span>{this.state.greetingMessage} - &nbsp;</span>
                <span>{this.state.user}.</span>
                <span>&nbsp; It is {this.state.date.toLocaleTimeString()} o'clock</span>
            </div>
        );
    }
}

Clock.defaultProps = {
    greetingMessage: 'Hello',
    user : 'unknown',
};

Clock.propTypes = {
    greetingMessage: PropTypes.string && PropTypes.element.isRequired,
    user: PropTypes.string,
};