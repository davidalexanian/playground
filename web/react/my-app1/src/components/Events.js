function Button({ onClick, children }) {
    return (
        <button onClick={e => {
            // all events propagate in React except onScroll, which only works on the JSX tag you attach it to.
            e.stopPropagation();
            onClick();  // callt the function passed as a prop
        }}>
            {children}
        </button>
    );
}

export function Toolbar() {
    return (
        <div className="Toolbar" onClick={() => {
            alert('You clicked on the toolbar!');
        }}>
            <Button onClick={() => alert('Playing!')}>
                Play Movie
            </Button>
            <Button onClick={() => alert('Uploading!')}>
                Upload Image
            </Button>
        </div>
    );
}

export function Signup() {
    return (
        <form onSubmit={e => {
            // Some browser events have default behavior associated with them
            e.preventDefault();
            alert('Submitting!');
        }}>
            <input />
            <button>Send</button>
        </form>
    );
}
