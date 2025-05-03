export function PeopleList() {
    const people = [
        'Creola Katherine Johnson: mathematician',
        'Mario José Molina-Pasquel Henríquez: chemist',
        'Mohammad Abdus Salam: physicist',
        'Percy Lavon Julian: chemist',
        'Subrahmanyan Chandrasekhar: astrophysicist'
    ];

    const peopleJsx = people.map((person, index) => {
        return <li key={index}>{person}</li>;
    });
    return <ul> {peopleJsx} </ul>;
}
