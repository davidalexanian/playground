function Person(firstName, lastName) {
    this.FirstName = firstName || "unknown";
    this.LastName = lastName || "unknown";
};
Person.prototype.getFullName = function() {
    return this.firstName + " " + this.lastName;
}

function Student(firstName, lastName, schoolName)
{
    this.firstName = firstName;
    this.lastName = lastName;
    
    // Person.call(this, firstName, lastName);
    this.SchoolName = schoolName || "unknown";
}
console.log(Student.prototype);         // Student: {}
Student.prototype = new Person();       // new sets __proto__ to Person.prototype
console.log(Student.prototype);
let s1 = new Student('adam', 'smith', 'school1');
let s2 = new Student('joe', 'gold', 'school2');
console.log(s1.getFullName());
console.log(s2.getFullName());





