import React from 'react';
import TimerForm from './TimerForm'

export default class ToggleableTimerForm extends React.Component {
    state = {
        isOpen: false,
    }

    handleFormOpen = () => this.setState({ isOpen: true });
    
    handleFormClose = () => this.setState({ isOpen: false });

    handleFormSubmit = (id, title, project) => {
        this.props.onFormSubmit(id, title, project);
        this.handleFormClose();
    }

    render() {
        if (this.state.isOpen) {
            return (
                <TimerForm
                    onClose={this.handleFormClose}
                    onSubmit={this.handleFormSubmit} />);
        }
        else {
            return (
                <div class="toggleableTimerForm">
                    <a class="plusSign" onClick={this.handleFormOpen}>
                        <i class="fa fa-plus"></i>
                    </a>
                </div>
            );
        }
    }
}