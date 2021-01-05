
// ---- break
for (const x of ['a', 'b', 'c', 'd']) {
    if (x == 'b') continue;
    if (x === 'c') break;
    console.log(x);
}

foo: { // label
    if (true) break foo; // labeled break
    console.log("won't be visble");
}

//---- switch
let x = 'hello';
switch (x) {
    case 'hello':
        console.log('bonjour');
        break;
    case 'goodbye':
        console.log('au revoir');   // will be printed without break;
        break;
    default:
        throw Error('Unknown value %s', x);
}

// ---- for loops
for (let i = 0; i < 3; i++) {
    console.log(i);
}

const iterable = ['hello', 'world'];
for (const elem of iterable) {
    console.log(elem);
}
let elem;
for (elem of iterable) {
    console.log(elem);
}

const arr = ['a', 'b', 'c'];
for (const [index, elem] of arr.entries()) {
    console.log(`${index} -> ${elem}`);
}

for (const key in { a: 'a', b: 'b' }) {
    console.log(key);
}

try {
    throw new Error('desc');
}
catch (err) {
    console.log(err.message);
    console.log(err.stack);
}
finally {
    console.log('finally')
}