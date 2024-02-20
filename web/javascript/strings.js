// object.toString
console.log(String({ a: 1 }));      // [object Object]
console.log({ a: 1 }.toString());   // [object Object]
const obj = {
    toString() {
        return 'hello';
    }
};
console.log(obj.toString());            // hello
console.log(JSON.stringify({ a: 1 }));  // {a : 1}


// string methods
console.log('abc'.slice(1, 3));             // bc    
console.log('aa | bb | cc'.split('|'));     // [ 'aa ', ' bb ', ' cc' ]
console.log('a : b : c'.split(/ *: */));    // ['a', 'b', 'c']
console.log('abc'.replace('bc', 'de'));     // ade
console.log('abc'.charAt(1));               // b
console.log('abc'.toUpperCase());           // ABC
console.log('abc'.includes('bc'));          // true
console.log('abc'.repeat(2));               // true
console.log('abc'.padEnd(5,'def'));         // abcde
console.log('abcdef123'.search('f1'));      // 5
console.log('  abc '.trim());               // abc


// string template literals
console.log(`Max-${Number.MAX_VALUE}, backslash-\\`);


// multiline & raw (identical to @ in C# o avoid escapeing the special simbols)
const filePath = String.raw`C:\Development\profile\aboutme.html`;
console.log(`The file was uploaded from: ${filePath}`);
let str = `multiline 
                text`;


// show function body
console.log(String(function f() { return 4 }));


// print each character
for (const ch of 'abc')
    console.log(ch);