import React from 'react'
const choice1 = 'BITCOIN', choice2 = 'USD';

export default class Choice extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selected: choice1
        }
    }

    static get choice1() {
        return choice1;
    }
    static get choice2() {
        return choice2;
    }

    select(value) {
        return (v) => {
            this.setState({
                selected: value
            });
        }
    };

    renderChoice(value) {
        let classes = ["choiceItem"];
        if (value === this.state.selected) {
            classes.push('active');
        }
        classes = classes.join(" ");
        return (
            <div className={classes} onClick={this.select(value)}>{value}</div>
        )
    }

    render() {
        return (
            <div className="choice">
                {this.renderChoice(Choice.choice1)}
                {this.renderChoice(Choice.choice2)}
            </div>
        )
    }
}