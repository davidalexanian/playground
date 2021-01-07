function Person() {
    this.firstName = 'firsName';
    this.sayHi = () => "hi";
}
let person = new Person;
for(const prop in person) {
    console.log(prop);
}

person.hasOwnProperty('firstName');    // true, it comes from own declaation not from prototype
console.log(Object.getOwnPropertyDescriptor(person, 'firstName'));
console.log(Object.getOwnPropertyDescriptor(person, 'sayHi'));

// add new prop in person
let desc = {
    writable : false,
    value : 'lastNameValue',
    enumerable : true,
    configurable : true
};
Object.defineProperty(person, 'lastName', desc);
console.log(person.lastName);   // lastNameValue
console.log((new Person).lastName);   // undefined

desc.configurable = false;  // do not allow to change the lastName further with defineProperty
desc.enumerable = false;    // do not show inf for-in loop any more
Object.defineProperty(person, 'lastName', desc);
for(const prop in person) {     // lastName will not be visible any more
    console.log(prop);
}
console.log(Object.getOwnPropertyDescriptor(person, 'lastName'));

