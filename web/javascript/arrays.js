// >>> array basics
console.log()
console.log('>>> array basics')
let arr = new Array(1, 2, 3);
arr[0] = 'a';
arr[4] = Date.now();
console.log(arr[3], arr[4]);             // undefined, current time
console.log(typeof (arr) === 'object');  // true
console.log(Array.isArray(arr));         // true
console.log([] instanceof Array);        // true
arr = ['a', 'b', 'c'];
console.log(arr);               // ['a', 'b', 'c']
console.log(arr.toString());    // a,b,c
console.log(arr.join(', '));    // a, b, c

// >>> forEach, indexOf, lastIndexOf, findIndex, find
console.log()
console.log('>>> forEach, indexOf, lastIndexOf')
let fruits = ['Banana', 'Apple', 'Orange', 'Banana'];
fruits.forEach((v, i) => {
    console.log(`elem at ${i} index with value ${v}`);
});
console.log(fruits.indexOf("Banana"));          // 0
console.log(fruits.lastIndexOf("Banana"));      // 3
console.log('index of first occurance or -1:', fruits.findIndex((v,i) => v === "###"));          // -1
console.log('first occurance of element or undefined:', fruits.find((v,i) => v === "Apple"));    // Apple

// >>> sort, reverse, filter, map, every, some
console.log()
console.log('>>> sort, reverse, filter, map, every, some')
// sort: by def sorts as a string, or pass a sorting funcition that return (-1,0,1)
let points = [5, 4, 3, 2, 1];
console.log(points.sort());
console.log(points.sort(function (a, b) { return a - b }));
console.log(points);
points.reverse();
console.log('filter: using a predicate', points.filter((v, i) => v > 3));   // 5, 4
console.log('map: creates new array', points.map(v => v * 2));
console.log('every: same as C# All', points.every((v,i) => v % 2 == 0));    // false
console.log('some: same as C# Any', points.some((v,i) => v % 2 == 0));      // true

// >>> pop, push, shift, unshift
console.log()
console.log('>>> pop, push, shift, unshift')
arr = ['a', 'b', 'c'];
let lastElem = arr.pop();               // remove and return last element: [a,b]
let newLength = arr.push(lastElem);     // add at the end
let firstElem = arr.shift();            // remove and return first element [b,c]
newLength = arr.unshift(firstElem);     // add at start

// >>> slice splice concat
console.log()
console.log('>>> slice splice concat')
let letters = ["a", "b"].concat(["c", "d", "e"]);
console.log(letters.slice(0, 2));       // [ab], returns a copy (original array not affected)
let newElements = ["x", "y", "z"]
// remove 2 elements from index=0 and insert new elements instead of
let removedElements = letters.splice(0, 2, ...newElements);
console.log(removedElements);
console.log(letters);

// >>> reduce reduceRight (call a fucntion for each element, return result)
console.log()
console.log('>>> reduce reduceRight')
console.log('reduce: aggregation function',
    [1,2,3].reduce((prevValue, curValue) => prevValue + curValue));        // 6, from left to right
console.log('reduceRight: aggregation function',
    [1,2,3].reduceRight((prevValue, curValue) => prevValue + curValue));   // 6, right to left