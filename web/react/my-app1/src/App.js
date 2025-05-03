import './App.css';
import React, { useState } from 'react';
import { ProductList } from './components/Intro.js';
import { Profile1, Profile2, ProfileCard } from './components/Props.js';
import * as Conditional from './components/Conditional.js';
import * as Lists from './components/Lists.js';
import * as Events from './components/Events.js';
import * as State from './components/State.js';

export function Tabs() {
  const [activeTab, setActiveTab] = useState('Tab1');

  const renderTabContent = () => {
    const parentDiv = document.querySelector(".tab");
    if (parentDiv) {
      parentDiv.querySelectorAll('button').forEach(btn => btn.classList.remove("active"));
      document.getElementById(activeTab).classList += ' active';
    }

    switch (activeTab) {
      case 'Tab1':
        return <div>
          <ProductList />
        </div>;
      case 'Tab2':
        return <div>
          <Profile1 name='John Doe' url='https://i.imgur.com/1bX5QH6.jpg' size={50} />
          <Profile2 name='Sam Smith' url='https://i.imgur.com/1bX5QH6.jpg' />
          <ProfileCard>
            <Profile2 name='Max Plank' url='https://i.imgur.com/1bX5QH6.jpg' size={150} />
          </ProfileCard>
        </div>;
      case 'Tab3':
        return <div>
          <Conditional.PackingList />
        </div>;
      case 'Tab4':
        return <div>
          <Lists.PeopleList />
        </div>;
      case 'Tab5':
        return <div>
          <Events.Toolbar />
        </div>;
      case 'Tab6':
        return <div>
          <State.StateDemoGallery />
          <br />
          <hr />
          <State.SharingStateBetweenComponentsDemoButton/>
          <br />
          <State.StateValueUpdaterCounterDemo/>
          <br />
          <State.ObjectAsStateDemo/>
          <br />
          <State.StateKeyDemoWithScoreboard />
        </div>;
      default:
        return null;
    }
  };

  return (
    <div>
      <div className='tab' style={{ marginBottom: '10px' }}>
        <button id='Tab1' onClick={() => setActiveTab('Tab1')}>Intro</button>
        <button id='Tab2' onClick={() => setActiveTab('Tab2')}>Props</button>
        <button id='Tab3' onClick={() => setActiveTab('Tab3')}>Conditional JSX</button>
        <button id='Tab4' onClick={() => setActiveTab('Tab4')}>Lists</button>
        <button id='Tab5' onClick={() => setActiveTab('Tab5')}>Events</button>
        <button id='Tab6' onClick={() => setActiveTab('Tab6')}>State</button>
      </div>
      <div>{renderTabContent()}</div>
    </div>
  );
}

export default function App() {
  return <div>
    <h2>Learning React</h2>
    <Tabs />
  </div>
}

