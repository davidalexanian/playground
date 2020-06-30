export function consoleDemo() {
    console.log();
    console.log('---------------- console demo ----------------');

    // %s converts the corresponding value to a string
    // %o inserts a string representation of an object
    // %j converts a value to a JSON string and inserts it
    // %% inserts a single %
    console.log('abc', 123, true);
    console.log('abc', 123, true);
    console.log('Test: %s %j', 123, 'abc');
    console.log('%o', {foo: 123, bar: 'abc'});
    console.log('%j', {foo: 123, bar: 'abc'});
    console.log('%s%%', 99);
    console.error("error");
}

export function letAndConst() {
    console.log();
    console.log('---------------- let and const ----------------');

    let i = 10;
    //let i =2 - duplicate declaration error
    i = 100;
    console.log(i); //100

    // ref is const, but obj props can change
    const obj = { prop: 0 };
    obj.prop = 10;
    console.log(obj.prop);
    // obj = {} - error

    // ok on for-of but must use let in plain for
    const arr = ['hello', 'world'];
    for (const elem of arr) {
        console.log(elem);
    }
    for (let i=0; i<arr.length; i++) {
        console.log(arr[i]);
    }
}

export function letAndConstScopesDemo() {
    console.log();
    console.log('---------------- letAndConstScopesDemo ----------------');

    { 
        // Scope A. x is Accessible, it is not out of scope
        const x = 7;
        { 
            // Scope B. Accessible: x, y
            const y = 1;
            const x = 15;  // shadow x from outer scope
            console.log('x:' + x, ', y:' + y);
        }

        try {
            console.log(y);
        }
        catch(err) {
            console.error('Error accessing y in this scope');
        }
    }
}

export function typesDemo() {
    console.log();
    console.log('---------------- typesDemo.name ----------------');

    // typeof
    const x = {};
    let i = 100;
    console.log(typeof x);  // object
    console.log(typeof i);  // number
    
            // instanceof
    console.log((function () { }) instanceof Function);   // true
        console.log(({}) instanceof Object);  // true
            console.log([] instanceof Array);     // true
                    console.log(125 instanceof Number);   // false

    // primitive type constructors
    console.log(new Number('123') == 123);              // true
    console.log(new Number('123') === 123);             // false
    console.log(new Number(123).valueOf() === 123);     // true
    console.log(Number.isInteger(125));                 // true
    console.log((125).toString == Number.prototype.toString);   // true

    console.log('type conversion');
    console.log(typeof Object(10));                 // object
    console.log(Object(10).valueOf());              // 10
    console.log(typeof (Object(10).valueOf()));     // number
    
    console.log('7' * '3');             // 21
    console.log(typeof ('7' * '3'));    // number
}

export function operatorsDemo() {
    // type coercion by operators
    const obj = {};
    obj['true'] = 123; 
    console.log(obj[true]); // 123

    console.log([1,2] + [3,4]);  //'1,2,3,4
    console.log({a:14} + 'abc');    //[object Object]abc

    // assignment
    let str = 'str';
    str += 'more';

    // == equality with coercion
    console.log('123' == 123);  // true
    console.log(false == 0);    // true
    console.log('' == 0);       // true    
    console.log(['1', '2', '3'] == '1,2,3');
    console.log(undefined == null);

    Object.is(123,124);         // false
    Object.is('abc', 'abc');    // true
    Object.is(NaN, NaN);        // true

    console.log('1','2');
    console.log(void(10));  // undefined
}

export function stringDemo() {
    // show function body
    console.log(String(function f() {return 4}));

    // print each character
    for (const ch of 'abc')
        console.log(ch);

    // object.toString
    console.log(String({a: 1}));

    const obj = {
        toString() {
          return 'hello';
        }
    };
    console.log(obj.toString());
    console.log(JSON.stringify({a: 1}));

    // code points
    console.log('\u{1F642}');
    console.log(String.fromCodePoint(0x1F642));
    
    console.log('abc'.slice(1, 3));             // bc    
    console.log('aa | bb | cc'.split('|'));     // [ 'aa ', ' bb ', ' cc' ]
    console.log('a : b : c'.split(/ *: */));    // ['a', 'b', 'c']
    console.log('abc'.replace('bc','de'));      // ade

    // string template literals
    console.log(`Max-${Number.MAX_VALUE}, backslash-\\`);
    console.log(String.raw `\back`, '\\back');
    const str = `multiline 
                text`;
}

export function symbolDemo() {
    const s1 = Symbol('description')
    const s2 = Symbol('description')
    console.log(s1==s2);    // false
    console.log(typeof s1); // symbol

    // constant values
    const COLOR_BLUE = Symbol('Blue');
    const MOOD_BLUE = Symbol('Blue');
    console.log(COLOR_BLUE === MOOD_BLUE);  // false

    // Unique property keys
    const specialMethod = Symbol('specialMethod');
    const obj = {
        _id: 'kf12oi',
        [specialMethod]() { // (A)
            return this._id;
        }
    };
    console.log(obj[specialMethod]());  // 'kf12oi'

    // global symbols
    const foo = Symbol.for("app.foo");
    console.log(Symbol.keyFor(foo) === "app.foo");
    let obj1 = {};
    obj[foo] = "foo";
    console.log(obj[foo]);
    console.log(Object.keys(obj1)); 
    console.log(Object.getOwnPropertyNames(obj1)); 
    console.log(Object.getOwnPropertySymbols(obj1));
}