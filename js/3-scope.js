function letAndConstScopesDemo() {
    console.log();
    console.log('---------------- letAndConstScopesDemo ----------------');

    {
        // x is Accessible, it is not out of scope
        const x = 7;
        {
            // Scope B. Accessible: x, y
            const y = 1;
            const x = 15;  // shadow x from outer scope
            console.log(`x:${x}`, ` y:${y}`);
        }

        // y is not in scope
        try {
            console.log(y);
        }
        catch (err) {
            console.error('Error accessing y in this scope');
        }

        // global variables are available in nested scopes (has to be declared prior calling the function)
        console.log(`globalLet:${globalLet}`);
    }
}

let globalLet = 'let';
letAndConstScopesDemo();

// functions are early activated, regardless of where it is located
console.log(foo()); // OK
function foo() { return 'foo'; }

// functions declared with const/let and classes are not early activated, but can be referenced
let f = () => bar();        // ok ref but not call
const bar = () => 'bar';
console.log(f());

