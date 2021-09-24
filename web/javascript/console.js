// %s converts the corresponding value to a string
// %o inserts a string representation of an object
// %j converts a value to a JSON string and inserts it
// %f format as floating point number
// %% inserts a single %
// `${variable}` - string template literals

console.log();
console.log('------------------ console ------------------');

console.log('outputing multiple variables > ', 123, true, new Date().toUTCString());
console.log('%%s - converts the corresponding value to a string > %s %s %s', 12, 'abc');
console.log('%%o - inserts a STRING representation of an object > %o %o', { foo: 123, bar: 'abc' }, new Object());
console.log('%%j - inserts a JSON representation of an object > %j %j', { foo: 123, bar: 'abc' }, new Date());

let dd = new Date();
console.log(`\${variable} - insert variable value > ${dd}`)


console.log('------------------ console methods');
console.error("error");
console.debug("debug")
console.info("info")

let af = (txt) => stackTraceExample(txt);
af('print stack trace');
function stackTraceExample(arg) {
    console.trace(arg);
}
console.count('text')           // 1
console.count('text')           // 2
console.count('text changed')   // 1


console.log('------------------ calculate time spent');
const doSomething = () => {
    for (i = 1; i <= Number.MAX_SAFE_INTEGER; i += 100000000);
};
const measureDoingSomething = () => {
    console.time('doSomething()')
    doSomething()
    console.timeEnd('doSomething()')
}
measureDoingSomething()


console.log('------------------ stdin');
const readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
})
readline.question(`What's your name?`, (name) => {
    console.log(`Hi ${name}!`)
    readline.close()
})


console.log('')
console.log('------------------ progress bar');
const { setTimeout } = require("timers");
const ProgressBar = require('progress')
const bar = new ProgressBar(':bar', { total: 100 })

let currentProgress = 0;
const increase = 5;
const timer = setInterval(() => {
    currentProgress += increase;
    bar.tick(increase)
    console.log(currentProgress);
    if (bar.complete) {
        clearInterval(timer)
    }
}, 50)
console.log('cleaning console in 2 seconds');
setTimeout(() => console.clear(), 2000);


