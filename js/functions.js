(function functionsDemo()
{
    function f1(x, y = 11) {
        console.log(`x-${x}, y-${y}`);
    }
    function f2(x, ...y) {
        console.log(`x-${x}, y-${y.length == 0 ? '[]' : y}`);
    }
    function f3({start, end=3}) {
        console.log(`start-${start}, end-${end}`);
    }
    
    f1(10);  // x-10, y-11
    f1(undefined, undefined);    // undefined, 11
    
    f2(1);      // x-1, y-[]
    f2(1,2,3);  // x-1, y-2,3
    
    f3({end: 1, start:2});
    f3({}); // undefined, 3
    //f3(); - error
    
    const myArray = ['a', 'b'];
    const callback1 = (v) => console.log(v);
    const callback2 = (v, i) => {
    console.log(`value-${v}, index-${i}`);
    };    
    myArray.forEach(callback1);
    myArray.forEach(callback2);
})();

(function arrowFunctionsDemo() {
    const myArray = ['a', 'b'];
    const callback1 = (v) => console.log(v);
    const callback2 = (v, i) => {
        console.log(`value-${v}, index-${i}`);
    };    
    myArray.forEach(callback1);
    myArray.forEach(callback2);
})();

(function spreadOperatorDemo() {
    function add(a, b) { return a + b; } 

    let nums = [5, 4]; 
    console.log(add(...nums));

    nums = [4];  
    console.log(add(5, ...nums)); 

    let a = [2, 3, 4]; 
    let b = [1, ...a, 5]; 
    console.log(b);     // 1,2,3,4,5
})();

