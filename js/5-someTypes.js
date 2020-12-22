function stringDemo() {
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

function symbolDemo() {
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