function Item({ name, isPacked }) {
    // save JSX to a variable and include it in JSX using curly braces.
    let itemContent = isPacked ? (
        <del>
            {name + " ✅"}
        </del>
    ) : name;

    return (
        <li className="item">
            {itemContent}
        </li>
    );
}

export function PackingList() {
    return (
        <section>
            <h1>Sally Ride's Packing List</h1>
            <ul>
                <Item
                    isPacked={true}
                    name="Space suit"
                />
                <Item
                    isPacked={true}
                    name="Helmet with a golden leaf"
                />
            </ul>
        </section>
    );
}