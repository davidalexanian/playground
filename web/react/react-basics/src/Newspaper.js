import React from 'react'

const Newspaper = props => {
    return (
        <Container>
            <Article headline="Article1" content="content 1..."/>
            <Article headline="Article2" content="content 2..."/>
        </Container>
    )
}
export default Newspaper;

// here just to draw a div around childs
function Container(props) {
    return (<div className="container">{props.children}</div>);
}
function Article(props) {
    return (
        <div>
            <h3>{props.headline}</h3>
            <span>{props.content}</span>
        </div>
    );
}