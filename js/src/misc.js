export default function someFunct() {
    console.log('someFunct');
}

export function sum(x, y) { 
    return x + y;
}

function sub(x, y) { 
    return x - y;
}

export var pi = 3.141593;

// give an alias while exporting
export {
    sum as add,
    sub as diff,
    pi as pi_num, // trailing comma is optional
};

export function evalAndDemo() {
    eval('console.log(2 * 4)');
    const times1 = new Function('a', 'b', 'console.log(a * b)');
    times1(2,3);
}

export function controlFlowDemo() {

    // ---- break
    for (const x of ['a', 'b', 'c', 'd']) 
    {
        if (x == 'b') continue; 
        if (x === 'c') break;
        console.log(x);
    }

    foo: { // label
        if (true) break foo; // labeled break
        console.log("won't be visble");
    }

    //---- switch
    let x = 'hello';
    switch (x) {
        case 'hello':
            console.log('bonjour');
            break;
        case 'goodbye':
            console.log('au revoir');   // will be printed without break;
            break;
        default:
            throw Error('Unknown value %s', x);
    }

    // ---- for loops
    for (let i=0; i<3; i++) {
        console.log(i);
    }

    const iterable = ['hello', 'world'];
    for (const elem of iterable) {
        console.log(elem);
    }
    let elem;
    for (elem of iterable) {
        console.log(elem);
    }

    const arr = ['a', 'b', 'c'];
    for (const [index, elem] of arr.entries()) {
        console.log(`${index} -> ${elem}`);
    }

    for (const key in {a:'a', b:'b'}) {
        console.log(key);
    }

    try {
        throw new Error('desc');
    } 
    catch (err) {
        console.log(err.message);
        console.log(err.stack);
    }
    finally {
        console.log('finally')
    }
}

export function destructuringAssignmentDemo() {
    // array destructuring
    let [a, , b] = [1,2,3]; 
    console.log("a:", a, "b:", b);

    // setting 7, 42 as default values
    let list = [ 7, 42 ]; 
    let [c = 1, d = 2, e] = list;
    console.log("c:", c, "d:", d, "e:", e);
 
    // object destructuring
    var {foo, bar} = {foo: 'lorem', bar: 'ipsum', choo: 'uhoh'}; 
    console.log("foo:", foo, "bar:", bar);

    let cust = { address: { street: "1001 Oak Drive", state: "Summerville" } }; 
    let { address: {city: city}, address: {state: state} } = cust; 
    console.log("City:", city, "\nState:", state);

    // function arguments
    function f ([ name, val ]) { console.log(name, val); } 
    function g ({ name: n, val: v }) { console.log(n, v); }
    function h ({ name, val }) { console.log(name, val); }
     
    f([ "bar", 42 ]); 
    g({ name: "foo", val: 7 }); 
    h({ name: "bar", val: 42 });
}