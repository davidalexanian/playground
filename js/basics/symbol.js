const s1 = Symbol('description')
const s2 = Symbol('description')
console.log(s1 == s2);    // false
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