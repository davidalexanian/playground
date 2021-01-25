import React from 'react';
import Clock from './Clock.js';
import { WelcomeFunction, WelcomeClass } from './FunctionComponents.js'
import Choice from './Choice.js'
import Counter from './Counter.js'
import Newspaper from './Newspaper.js'

export default function App() {
  return (
    <div className="App">
      <WelcomeFunction name="John" />
      <WelcomeClass name="Jack" />
      <Clock/>
      <Choice/>
      <Counter initialValue={12} />
      <Newspaper/>
    </div>
  );
}



