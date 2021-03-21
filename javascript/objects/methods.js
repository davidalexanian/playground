console.log("-------------------- Object.assign");
const target = { a: 1, b: 2 };
const source1 = { b: 4, c: 5 };
const source2 = { d: 'dd' };
const returnedTarget = Object.assign(target, source1, source2);
console.log(target);    // { a: 1, b: 4, c: 5, d: 'dd' }
console.log(returnedTarget == target);  // true


console.log("-------------------- Object.entries & fromEntries");
const obj = {
    foo: 1,
    bar: 2
};
// assert.deepEqual(Object.entries(obj), [['foo', 1], ['bar', 2]]);
for (const entry of Object.entries(obj)) {
    console.log(`${entry[0]}-${entry[1]}`);
}
const objFromEntries = Object.fromEntries(Object.entries(obj));
console.log(JSON.stringify(obj) === JSON.stringify(objFromEntries));

console.log("-------------------- Object.freeze");
Object.freeze(obj);
obj.foo = 11;
console.log(obj.foo);   // still 1
console.log(Object.isFrozen(obj.foo));

console.log("-------------------- Object.is");
Object.is('foo', 'foo');     // true
Object.is('foo', 'bar');     // false
Object.is([], []);           // false
Object.is({ a: 1, b: 2 }, { a: 1, b: 2 });  // false

var foo = { a: 1 };
var bar = { a: 1 };
Object.is(foo, foo);         // true
Object.is(foo, bar);         // false

// Special Cases
Object.is(null, null);       // true
Object.is(0, -0);            // false
Object.is(-0, -0);           // true
Object.is(NaN, 0 / 0);         // true

console.log("-------------------- misc");
console.log(Object(10).valueOf());              // 10
console.log(Object.getPrototypeOf(person));     // object