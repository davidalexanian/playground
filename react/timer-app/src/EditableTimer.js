import React from 'react';
import Timer from './Timer';
import TimerForm from './TimerForm';


export default class EditableTimer extends React.Component {
    state = {
        editFormOpen: false,
    };
    handleClose = () => this.setState({editFormOpen : false});

    handleSubmit = (id, title, project) => {
        this.props.onFormSubmit(id, title, project);
        this.handleClose();
    }

    render = () => (this.state.editFormOpen ?
        <TimerForm
            id={this.props.id}
            title={this.props.title}
            project={this.props.project} 
            onClose={this.handleClose}
            onSubmit={this.handleSubmit}/> :
        <Timer
            id={this.props.id}
            title={this.props.title}
            project={this.props.project}
            elapsed={this.props.elapsed}
            runningSince={this.props.runningSince} 
            onClose={this.handleClose}
            onTimerDelete={this.props.onTimerDelete}
            onTimerStart={this.props.onTimerStart}
            onTimerStop={this.props.onTimerStop}/>);
}