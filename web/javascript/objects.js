console.log("-------------------- objectsDemo");
(function objectsDemo() {
    // property shorthand
    let first = "john";
    let last = "doe";
    let obj1 = { first: first, last: last };
    obj1 = { first, last, a:15 };
    console.log(obj1);

    const obj2 = {
        firstName: 'firstName',
        lastName: 'lastName',
        if: 'if',    // keywords are ok

        // -- getter/setter
        get fullName() {
            return `${this.firstName} ${this.lastName}`;
        },
        set fullName(fullName) {
            const parts = fullName.split(' ');
            this.firstName = parts[0];
            this.lastName = parts[1];
        },
    };

    console.log(Object.keys(obj2));
    console.log(obj2.notDefined);        //undefined
    obj2.newProperty = 'newProperty'     //newProperty created
    console.log(obj2.newProperty);       //undefined

    obj2.fullName = 'aaa bbb';
    console.log(obj2.fullName);
})();

console.log("-------------------- objectAsDictionaryDemo");
(function objectAsDictionaryDemo() {
    const obj = {
        'any string': 123,
        'nice method'() { return 'method'; },
        ['f' + 'o' + 'o']: 567,
        aaa: 11,
        [Symbol.toStringTag]() {
            return 'function with symbol name';
        },
    };
    console.log(obj['any string']);
    console.log(obj['nice method']());
    console.log(obj[Symbol.toStringTag]());

    // is there a property with a given key
    console.log('any string' in obj);
    console.log(Object.keys(obj));      // list keys
    delete obj.aaa;                     // delete keys
    console.log(Object.keys(obj));      // list keys
})();