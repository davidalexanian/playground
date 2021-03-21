function myFunction() {
    console.log('x');
}
console.log(typeof myFunction);

myFunction.someMethod = function() {
    console.log('someMethod');
}
myFunction.someMethod();

// new myFunction().someMethod();   - error
console.log(myFunction.prototype.someMethod);  // undefined
