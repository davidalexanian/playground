// all props passed as a siinglr object
export function Profile1(props) {
    return (
        <div>
            <h1>{props.name}</h1>
            <img
                className="avatar"
                src={props.url}
                alt={'Photo of ' + props.name}
                style={{
                    width: props.size,
                    height: props.size
                }}
            />
        </div>
    );
}

// or the props can be passed deconstructed
export function Profile2({ name, url, size = 100 }) {
    return (
        <Profile1 name={name} url={url} size={size} />
    );
}

// passiing JSX to another component with special props called children
export function ProfileCard({ children }) {
    return (
        <div className="profile-card">
            <h2>Profile card</h2>
            {children}
        </div>
    );
}