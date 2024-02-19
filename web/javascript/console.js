// %s converts the corresponding value to a string
// %o inserts a string representation of an object
// %j converts a value to a JSON string and inserts it
// %f format as floating point number
// %% inserts a single %
// `${variable}` - string template literals
console.log('outputing multiple variables > ', 123, true, new Date().toUTCString());
console.log('%%s - converts the corresponding value to a string > %s %s %s', 12, 'abc');
console.log('%%o - inserts a STRING representation of an object > %o %o', { foo: 123, bar: 'abc' }, new Object());
console.log('%%j - inserts a JSON representation of an object > %j %j', { foo: 123, bar: 'abc' }, new Date());
console.log(`\${variable} - insert variable value > ${new Date()}`)

// logging methods
console.log("any object");
console.error("error");
console.debug({a:1,b:2})
console.info("info")
console.trace('any object/text');

// how long executed
const doSomething = () => {
    for (i = 1; i <= Number.MAX_SAFE_INTEGER; i += 100000000);
};
console.time('doSomething()')       // unique by argument
doSomething()
console.timeEnd('doSomething()')

// input
const readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
})
readline.question(`What's your name?`, (name) => {
    console.log(`Hi ${name}!`)
    readline.close()
})