function Person(firstname, lastname) {
    // properties
    this.firstname = firstname;
    this.lastname = lastname;
    
    //methods
    this.Display = ()=>console.log(this.firstname + ' ' + this.lastname);
}
function MyFunc() {
    this.x = 100;
               
    return { a: 123 };
}

var p = new Person('a', 'b');
console.log(p.firstname);
p.Display();
console.log(Person('fn', 'ln') == undefined);   // true Person does not return anything
console.log(p.constructor.toString());

var obj = new MyFunc();
console.log(obj.x, obj.a); // undefined 123