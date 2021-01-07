console.log({}.constructor === Object);     // true
console.log({}.constructor === Object)      // true
console.log([].constructor === Array)       // true

let n = 3;
console.log(n.constructor === Number)       // true
console.log(n.constructor(11));             // 11
console.log(typeof n.constructor(11));      // number


function Tree(name) {
    this.name = name
}
function ChangedConstructor() {
    console.log('changed');
}
let theTree = new Tree('Redwood')
console.log(typeof theTree.constructor)         // function
console.log(theTree.constructor.toString());    // display function body
console.log(theTree.constructor === Tree.prototype.constructor); // true
theTree.constructor = ChangedConstructor;
console.log(theTree.constructor === ChangedConstructor.prototype.constructor); // true
