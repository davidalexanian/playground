import camelcaseKeys from 'camelcase-keys';

const arr = [1, 2, 3];
for (const [index, elem] of arr.entries()) {
    arr[index] = elem + 1;
}
console.log(arr)

function Person(firstname, lastname) {
    this.firstname = firstname;
    this.lastname = lastname;
}
let p = new Person();

console.log(toCamelCaseMy({AA:1, BB:2, Cc:'aaa'}))

function toCamelCaseMy(obj)
{
    if (obj == null) return obj;

    if (Array.isArray(obj)) {
        for (const [index, value] of obj.entries()) {
            if (Array.isArray(value) || typeof value === 'object') {
                obj[index] = toCamelCaseMy(value);
            }
        }
    }
    else if (typeof obj === 'object') {
        for (const oldKey in obj) {
            if (!obj.hasOwnProperty(oldKey)) continue;

            let desc = Object.getOwnPropertyDescriptor(obj, oldKey);
            if (!desc.writable || !desc.configurable) continue;

            if (oldKey.charAt(0) >= "A" && oldKey.charAt(0) <= "Z") {
                let newKey = oldKey.charAt(0).toLowerCase() + oldKey.slice(1);
                let value = obj[oldKey];
                if (Array.isArray(value) || typeof obj === 'object') {
                    value = toCamelCaseMy(value);
                }
                obj[newKey] = value;
                delete obj[oldKey]
            }
        }
    }
    return obj;
}
function toCamelCase(o) {
    var newO, origKey, newKey, value

    if (o instanceof Array) {
        return o.map(function (value) {
            if (typeof value === "object") {
                value = toCamel(value)
            }
            return value
        })
    } else {
        newO = {}
        for (origKey in o) {
            if (o.hasOwnProperty(origKey)) {
                newKey = (origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey).toString()
                value = o[origKey]
                if (value instanceof Array || (value !== null && value.constructor === Object)) {
                    value = toCamel(value)
                }
                newO[newKey] = value
            }
        }
    }
    return newO
}

let obj = {
    AaaBbb: 11,
    BBbbb: 12,
    CC: [{
        AaaaAaaa: 1,
        BCdefID: 'aaaa',
        Ccc: [{
            AaA: 1,
            BbB: 2,
            CcC: ['ccc', {
                FffFfff: 11,
                GggGgggG: 2
            }]
        }]
    }, "text"]
};
console.log(camelcaseKeys(obj, { deep: true, preserveConsecutiveUppercase: true, pascalCase: true }));
let cc = toCamelCaseMy(obj);
console.log(JSON.stringify(cc));
console.log(cc === obj);