console.log('------------------- typeof');
const x = {};
let i = 100;
console.log(typeof x, typeof i);  // object, number
 
console.log('------------------- instanceof');
console.log((function () { }) instanceof Function);   // true
console.log(({}) instanceof Object);  // true
console.log([] instanceof Array);     // true
console.log(125 instanceof Number);   // false

console.log('------------------- primitive type constructors');
console.log(new Number('123') == 123);              // true
console.log(new Number('123') === 123);             // false
console.log(new Number(123).valueOf() === 123);     // true
console.log(Number.isInteger(125));                 // true
console.log((125).toString == Number.prototype.toString);   // false
console.log((125).toString === Number.prototype.toString);   // true

console.log('------------------- type conversion');
console.log(typeof Object(10));                 // object
console.log(Object(10).valueOf());              // 10
console.log(typeof (Object(10).valueOf()));     // number

console.log('7' * '3');             // 21
console.log(typeof ('7' * '3'));    // number