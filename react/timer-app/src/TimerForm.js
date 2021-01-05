import React from 'react'
import './index.css'

export default class TimerForm extends React.Component {
    state = {
        title: this.props.title || '',
        project: this.props.project || '',
    };

    handleTitleChange = (e) => this.setState({ title: e.target.value });

    handleProjectChange = (e) => this.setState({ project: e.target.value });

    handleSubmit = ()=> this.props.onSubmit(this.props.id, this.state.title, this.state.project);

    render() {
        const submitText = this.props.id ? 'UPDATE' : 'CREATE';

        return (
            <div className="editableTimer">
                <div class="TimerFormContainer">
                    <label class="label">Title</label>
                    <input type='text' value={this.state.title} onChange={this.handleTitleChange}/>
                    <label class="label" style={{ marginTop: '0.8em' }}>Project</label>
                    <input type='text' value={this.state.project} onChange={this.handleProjectChange}/>
                    <div class="buttonContainer">
                        <button class="update" onClick={this.handleSubmit}>{submitText}</button>
                        <button class="cancel" onClick={this.props.onClose}>CANCEL</button>
                    </div>
                </div>
            </div>
        );
    }
}