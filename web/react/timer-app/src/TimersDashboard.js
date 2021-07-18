import React from 'react';
import EditableTimerList from './EditableTimerList'
import ToggleableTimerForm from './ToggleableTimerForm'
import './index.css';
import { v4 as uuidv4, v4 } from 'uuid';

export default class TimersDashboard extends React.Component {
    state = {
        timers: [
            {
                title: 'Practice squat',
                project: 'Gym Chores',
                id: uuidv4(),
                elapsed: 5456099,
                runningSince: Date.now(),
            },
            {
                title: 'Bake squash',
                project: 'Kitchen Chores',
                id: uuidv4(),
                elapsed: 1273998,
                runningSince: null,
            },
        ],
    };

    handleFormSubmit = (id, title, project) => {
        if (id) {
            let arr = [];
            for (const t of this.state.timers) {
                if (t.id === id) {
                    let tt = Object.assign({}, t, {
                        title: title,
                        project: project
                    });
                    arr.push(tt);
                }
                else {
                    arr.push(t);
                }
            }
            this.setState({ timers: arr });
        }
        else {
            const t = {
                title: title || 'title',
                project: project || 'project',
                id: uuidv4(),
                runningSince: null,
                elapsed: 0
            };
            this.setState({ timers: this.state.timers.concat(t) });
        }
    }

    handleTimerDelete = (id) => {
        const arr = this.state.timers.filter(t => t.id !== id);
        this.setState({ timers: arr });
    }

    handleTimerStart = (id) => {
        let t = this.state.timers.find(t => t.id === id);
        t.runningSince = Date.now();
        this.forceUpdate();
    }
    
    handleTimerStop = (id) => {
        let t = this.state.timers.find(t => t.id === id);
        t.elapsed += Date.now() - t.runningSince;
        t.runningSince = null;
        this.forceUpdate();
    }

    render() {
        return (
            <div class="timersDashboard">
                <EditableTimerList
                    timers={this.state.timers}
                    onFormSubmit={this.handleFormSubmit}
                    onTimerDelete={this.handleTimerDelete}
                    onTimerStart={this.handleTimerStart}
                    onTimerStop={this.handleTimerStop} />

                <ToggleableTimerForm 
                    isOpen={false} 
                    onFormSubmit={this.handleFormSubmit} />
            </div>
        );
    }
}
