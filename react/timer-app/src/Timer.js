import React from 'react'
import * as helpers from './helpers'

export default class Timer extends React.Component {
    handleTimerDelete = () => this.props.onTimerDelete(this.props.id);

    handleStartClick = () => this.props.onTimerStart(this.props.id);
    
    handleStopClick = () => this.props.onTimerStop(this.props.id);

    componentDidMount = () => this.forceUpdateInterval = setInterval(() => this.forceUpdate(), 100);

    componentWillUnmount = () => clearInterval(this.forceUpdateInterval);

    render() {
        const elapsedString = helpers.renderElapsedString(this.props.elapsed, this.props.runningSince);

        let button;
        if (this.props.runningSince) {
            button = <button class="stopTimer" onClick={this.handleStopClick}>STOP</button>
        }
        else {
            button = <button class="startTimer" onClick={this.handleStartClick}>START</button>;
        }

        return (
            <div class="editableTimer">
                <div class="timer">
                    <span class="title">{this.props.title}</span>
                    <span class="project">{this.props.project}</span>
                    <div style={{ textAlign: 'center' }}>
                        <span class="elapsed">{elapsedString}</span>
                    </div>
                    <div class="timerButtonContainer">
                        <i class="fa fa-trash" onClick={this.handleTimerDelete}></i>
                        <i class="fa fa-edit" style={{ marginLeft: '10px' }}></i>
                    </div>
                    <div>
                        {button}
                    </div>
                </div>
            </div >
        );
    }
}