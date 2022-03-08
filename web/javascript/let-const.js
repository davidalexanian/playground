console.log('---------------- let and const ----------------');

let i = 10;     //let i = 2 - duplicate declaration error
i = 100;
console.log(i); //100

// ref is const, but obj props can change
const obj = { prop: 0 };
obj.prop = 10;
console.log(obj.prop);
// obj = {} - error

// ok on for-of but must use let in plain for
const arr = ['hello', 'world'];
for (const elem of arr) {
    console.log(elem);
}
for (let i = 0; i < arr.length; i++) {
    console.log(arr[i]);
}