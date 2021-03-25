// this is global here
let myMethod = function () {
    console.log(this.y, ...arguments);
}
globalThis.y = "y";
myMethod();    // y
delete globalThis.y;

// implicit binding
let myObject = {
    y: "def"
};
myObject.myMethod = myMethod;
myObject.myMethod("abc");    // abc-def
delete myObject.myMethod

let argArray = [1, 2, 3];
myMethod.call(myObject, ...argArray);     // call myMethod by setting this object
myMethod.apply(myObject, argArray);       // apply is the same as call only passes the args as an array

console.log("--------------- bind");
var user = {
    firstName: "John",
    sayHi() {
        console.log(`Hello, ${this.firstName}!`, ...arguments);
    }
};
setTimeout(user.sayHi, 1000); // Hello, undefined, losing this

// The simplest solution is to use a wrapping function that creates a clojure:
setTimeout(() => user.sayHi(), 2000);     // Hello, John!

// bind
let boundSayHi = user.sayHi.bind(user);
boundSayHi();   // could be called also directly 
setTimeout(boundSayHi, 3000);

// partially applied functions
function mul(a, b) {
    return a * b;
}

let double = mul.bind(null, 2);
console.log(double(3)); // = mul(2, 3) = 6
console.log(double(4)); // = mul(2, 4) = 8


