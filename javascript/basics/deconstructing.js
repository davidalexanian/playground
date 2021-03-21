let zz;

(function destructuringAssignmentDemo() {
    // array destructuring
    let [a, , b] = [1, 2, 3];
    console.log("a:", a, "b:", b);

    // setting 7, 42 as default values
    let list = [7, 42];
    let [c = 1, d = 2, e] = list;
    console.log("c:", c, "d:", d, "e:", e);

    // object destructuring
    var { foo, bar } = { foo: 'lorem', bar: 'ipsum', choo: 'uhoh' };
    console.log("foo:", foo, "bar:", bar);

    let cust = { address: { street: "1001 Oak Drive", state: "Summerville" } };
    let { address: { city: city }, address: { state: state } } = cust;
    console.log("City:", city, "\nState:", state);

    // function arguments
    function f([name, val]) { 
        console.log(name, val); 
    }
    function g({ name: n, val: v }) { 
        console.log(n, v); 
    }
    function h({ name, val }) { 
        console.log(name, val); 
    }

    f(["bar", 42]);
    g({ name: "foo", val: 7 });
    h({ name: "bar", val: 42 });
});