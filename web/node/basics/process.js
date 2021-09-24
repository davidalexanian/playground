import ProgressBar from 'progress';

console.log();
console.log('----------------------- process ----------------------- ');

// set environment
process.env.NODE_ENV = 'development'

// arguments passed
process.argv.forEach((val, index) => {
    console.log(`${index}: ${val}`)
})