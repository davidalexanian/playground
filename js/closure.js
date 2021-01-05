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
