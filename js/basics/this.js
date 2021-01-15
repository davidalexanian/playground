console.log("------------ global context");
this.a = 10;
console.log(this);                  // {a:10} module level global in node
console.log(this === globalThis);   // false, just this

console.log("------------  function context");
function f1(n) {
    this.b = n;                    // added to global object
    return this;
}
let result = f1(1);
console.log(result === globalThis, `globalThis, b=${result.b}`);   // true, 20
console.log(new f1(2));     // {b : 2}

(function f2() {
    'use strict';
    console.log(this);  // undefined
    return this;
})();

console.log("------------ object context");
const test = {
    prop: 42,
    func: function () {
        'use strict'    // does not matter
        console.log(this.prop);
    },
    arrFunc: () => console.log(this.prop)
};
test.func();        // 42
test.arrFunc();     // undefined, it is global object (window in browser or module in node)
