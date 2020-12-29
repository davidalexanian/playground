// type coercion by operators
const obj = {};
obj['true'] = 123;
console.log(obj[true]);     // 123
console.log(obj[false]);    // undefined

console.log([1, 2] + [3, 4]);       //'1,2,3,4
console.log({ a: 14 } + 'abc');     //[object Object]abc

// assignment
let str = 'str';
str += 'more';

// == equality with coercion
console.log('123' == 123);  // true
console.log(false == 0);    // true
console.log('' == 0);       // true    
console.log(['1', '2', '3'] == '1,2,3');
console.log(`undefined == null: ${undefined == null}`);     // true

Object.is(123, 124);        // false
Object.is('abc', 'abc');    // true
Object.is(NaN, NaN);        // true