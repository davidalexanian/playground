console.log();
console.log('---------------- console demo ----------------');

// %s converts the corresponding value to a string
// %o inserts a string representation of an object
// %j converts a value to a JSON string and inserts it
// %% inserts a single %
// `${variable}` - string template literals
console.log('abc', 123, true);
console.log('Test: %s %j', 123, 'abc');
console.log('%o', { foo: 123, bar: 'abc' });
console.log('%j', { foo: 123, bar: 'abc' });
console.log('%s%%', 99);
console.error("error");

let x = 'xxx';
console.log(`${x}`)

