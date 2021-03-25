console.log('------------------- typeof');
const x = {};
let i = 100;
console.log(typeof x, typeof i);  // object, number
 
console.log('------------------- instanceof');
console.log((function () { }) instanceof Function);   // true
console.log(({}) instanceof Object);  // true
console.log([] instanceof Array);     // true
console.log(125 instanceof Number);   // false

function C() {}
let o = new C()
console.log(o instanceof C);        // true, because: Object.getPrototypeOf(o) === C.prototype
console.log(o instanceof Object);   // true