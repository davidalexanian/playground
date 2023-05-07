let arr = ['a', 'b', 'c'];
arr = new Array(1, 2, 3);
arr[0] = 'a';
arr[4] = Date.now();
console.log(arr[3], arr[4]);             // undefined, current time
console.log(typeof (arr) === 'object');  // true
console.log(Array.isArray(arr));         // true
console.log([] instanceof Array);        // true

// methods
var fruits = ["Banana", "Orange", "Apple", "Mango"];
console.log(fruits.toString())      // outputs all elems
console.log(fruits.join(', '));     // concats into a string with separator

// search
fruits.indexOf("Banana");
fruits.lastIndexOf("Banana");
console.log('index of first occurance or -1', fruits.findIndex((v,i) => v === "###"));    // -1
console.log('first occurance or element or undefined', fruits.find((v,i) => v === "###"));    // -1

// adding at the end or begining
let lastElem = arr.pop();
let newLength = arr.push(lastElem);
let firstElem = arr.shift();
arr.unshift(firstElem);

// slice splice concat
let removedElements = arr.splice(0, 2); // remove 2 elements from index=0
arr.splice(0, 0, removedElements);       // remove 0 elements at index =0 and add new elements
console.log(fruits.slice(1, 2).toString());
let kids = ["Cecilie", "Lone"].concat(["Emil", "Tobias", "Linus"]);

// sort
// by def sorts as a string, or pass a sorting funcition that retunr (-1,-0, 1)
let points = [40, 100, 1, 5, 25, 10];
console.log(points.sort());
console.log(points.sort(function (a, b) { return a - b }));
points.reverse();

// iteration
points.forEach((v, i) => {
    console.log(`elem at ${i} index with value ${v}`);
});
console.log('filter: using a predicate', points.filter((v, i) => v > 20));
console.log('map: creates new array', points.map(v => v * 2));
console.log('every: same as C# All', points.every((v,i) => v % 2 == 0));  // false
console.log('some: same as C# Any', points.every((v,i) => v % 2 == 0));  // true

console.log('reduce: aggregation function', [1,2,3].reduce((prevValue, curValue) => prevValue + curValue));
console.log('reduceRight: aggregation function', [1,2,3].reduceRight((prevValue, curValue) => prevValue + curValue));