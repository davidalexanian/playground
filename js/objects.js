(function objectsDemo() {
    // property shorthand
    let first = "john";
    let last = "doe";
    let obj1 = { first: first, last: last };
    obj1 = { first, last };
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

(function objectAssignDemo() {
    const target = { a: 1, b: 2 };
    const source1 = { b: 4, c: 5 };
    const source2 = { d: 'dd' };
    const returnedTarget = Object.assign(target, source1, source2);
    console.log(target);    // { a: 1, b: 4, c: 5, d: 'dd' }
    console.log(returnedTarget == target);  // true
})();

(function objectAsDictionaryDemo() {
    const obj = {
        'any string': 123,
        'nice method'() { return 'method'; },
        ['f'+'o'+'o']: 567,
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
    console.log(Object.values(obj));    // list values
    delete obj.aaa;                     // delete keys
})();