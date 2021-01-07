function makeFunc() {
    let i = 0;

    return function displayName() {
        i++
        console.log(i);
    }
}
var myFunc = makeFunc();
myFunc();   // 1
myFunc();   // 2
myFunc();   // 3


console.log("----------------- encapsulation with closures");
var counter = (function () {
    var privateCounter = 0;
    return {
        increment: function () {
            privateCounter++;
        },
        decrement: function () {
            privateCounter--;
        },
        value: function () {
            return privateCounter;
        }
    };
})();

console.log(counter.value());   // 0
counter.increment();
counter.decrement();
console.log(counter.value());         // 0

