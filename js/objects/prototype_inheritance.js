function Person(firstName, lastName) {
    this.firstName = firstName || "unknown";
    this.lastName = lastName || "unknown";
};
Person.prototype.getFullName = function() {
    return this.firstName + " " + this.lastName;
}

function Student(firstName, lastName, schoolName)
{   
    // this is the student here.  
    // As a result, methods are shared, but the object state is not. 
    Person.call(this, firstName, lastName);
    this.SchoolName = schoolName || "unknown";
}

// new sets Student.prototype to Person.prototype as well as Student.prototype.constructor to Person's
var proto = new Person();
Student.prototype = proto;
console.log(Student.prototype);             // Person { FirstName: 'unknown', LastName: 'unknown' }
console.log(Student.prototype.constructor === Person.prototype.constructor);    // true
Student.prototype.constructor = Student;

let s1 = new Student('adam', 'smith', 'school1');
let s2 = new Student('joe', 'gold', 'school2');

console.log(s1.getFullName());
console.log(s2.getFullName());
console.log(s1 instanceof Person);  // true
console.log(s2 instanceof Person);  // true

console.log(Object.getPrototypeOf(s1) === Object.getPrototypeOf(s2));
console.log(Object.getPrototypeOf(s1) === proto);
console.log(Person.prototype.isPrototypeOf(s1));    // true

