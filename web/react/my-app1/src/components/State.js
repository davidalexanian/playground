import { sculptureList } from './Data.js';
import { useState } from 'react';

// useState Hook provides those two things:
//  A state variable to retain the data between renders.
//  A state setter function to update the variable and trigger React to re-render

// call at the top of the component, React matches them up by their order.
// on subsequent re-renders, React needs to call the same Hooks in the exact same order.
// This allows React to know which piece of state or effect corresponds to which previous call.

// React waits until all code in the event handlers has run before processing your state updates.
// State updates are queued and processed in batches. This is to avoid unnecessary re-renders and improve performance.
export function StateDemoGallery() {
    const [index, setIndex] = useState(0);
    const [showMore, setShowMore] = useState(false);

    const disableNext = (index == sculptureList.length - 1);
    const disablePrev = (index == 0);

    function handleClickNext() {
        setIndex(index + 1);
    }

    function handleClickPrev() {
        setIndex(index - 1);
    }

    function handleClickShowMore() {
        setShowMore(showMore => !showMore);
    }

    let sculpture = sculptureList[index];
    return (
        <>
            <button onClick={handleClickNext} disabled={disableNext}>Next</button>
            <button onClick={handleClickPrev} disabled={disablePrev}>Prev</button>
            <h2>
                <i>{sculpture.name} </i>
                by {sculpture.artist}
            </h2>
            <h3>
                ({index + 1} of {sculptureList.length})
            </h3>
            <img src={sculpture.url} alt={sculpture.alt} />
            <p>
                {showMore && sculpture.description}
            </p>
            <button onClick={handleClickShowMore}>
                {showMore ? 'Show Less' : 'Show More'}
            </button>
        </>
    );
}

// State values during render never change and is fixed.
export function StateValueUpdaterCounterDemo() {
    const [number, setNumber] = useState(0);

    return (
        <div>
            <h1>{number}</h1>
            <button onClick={() => {
                setNumber(n => n + 1);
                setNumber(n => n + 1);  // updater function, called twice
            }}>+2</button>
            <button onClick={() => {
                setNumber(number + 1);  // same as calling twice the setNumber(0+1)
                setNumber(number + 1);
            }}>+1</button>
            <button onClick={() => {
                setNumber(number + 1);  // same as calling first setNumber(0+1) and then setNumber(1+1)
                setNumber(n => n + 1);
            }}>+2</button>
        </div>
    )
}

export function ObjectAsStateDemo() {
    const [person, setPerson] = useState({
        firstName: 'Barbara',
        lastName: 'Hepworth'
    });

    return (
        <div>
            <h3>do not mutate the state object/array directly (as it wont trigger render), always create a new object with the updated values</h3>
            <h3>{person.firstName} {person.lastName} {person.email}</h3>
            <input
                type="text"
                value={person.firstName}
                onChange={(e) => {
                    setPerson({ ...person, firstName: e.target.value });
                }} />
            <input
                type="text"
                value={person.lastName}
                onChange={(e) => {
                    setPerson({ ...person, lastName: e.target.value });
                }} />
        </div>
    );
}

export function SharingStateBetweenComponentsDemoButton() {
    const [count, setCount] = useState(0);
    const buttonCLick = () => {
        setCount(count + 1);
    };

    return (
        <div>
            <MyButton count={count} onClick={buttonCLick} />
            <MyButton count={count} onClick={buttonCLick} />
        </div>

    );
}
function MyButton({ count, onClick }) {
    return (
        <button onClick={onClick}>
            Clicked {count} times
        </button>
    );
}

// States are isolated between components. React keeps track of which state belongs to which component based on their place in the UI tree (not the JSX).
// React will keep the state around for as long as you render the same component at the same position in the tree. If it gets removed, or a different
// component gets rendered at the same position, React discards its state. As a rule of thumb, if you want to preserve the state between re-renders,
// the structure of your tree needs to “match up” from one render to another. If the structure is different, the state gets destroyed because React
// destroys state when it removes a component from the tree. There are two ways to reset state when switching between them:
// 1. Render components in different positions
// 2. Give each component an explicit identity with key. Specifying a key tells React to use the key itself as part of the position,
// instead of their order within the parent
export function StateKeyDemoWithScoreboard() {
    const [isPlayerA, setIsPlayerA] = useState(true);
    return (
        <div>
            {isPlayerA ? (
                <Counter key="Taylor" person="Taylor" />
            ) : (
                <Counter key="Sarah" person="Sarah" />
            )}
            <button onClick={() => {
                setIsPlayerA(!isPlayerA);
            }}>
                Next player!
            </button>
        </div>
    );
}
function Counter({ person }) {
    const [score, setScore] = useState(0);
    const [hover, setHover] = useState(false);

    let className = 'counter';
    if (hover) {
        className += ' hover';
    }

    return (
        <div
            className={className}
            onPointerEnter={() => setHover(true)}
            onPointerLeave={() => setHover(false)}
        >
            <h1>{person}'s score: {score}</h1>
            <button onClick={() => setScore(score + 1)}>
                Add one
            </button>
        </div>
    );
}