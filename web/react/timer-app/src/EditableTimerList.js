import React from 'react';
import EditableTimer from './EditableTimer'

export default class EditableTimerList extends React.Component {
    render() {
        const timers = this.props.timers.map((timer) => (
            <EditableTimer
                key={timer.id}
                id={timer.id}
                title={timer.title}
                project={timer.project}
                elapsed={timer.elapsed}
                runningSince={timer.runningSince}
                onFormSubmit={this.props.onFormSubmit}
                onTimerDelete={this.props.onTimerDelete}
                onTimerStart={this.props.onTimerStart}
                onTimerStop={this.props.onTimerStop}/>
        ));
        return (
            <div class="editableTimerList">
                {timers}
            </div>
        );
    }
}