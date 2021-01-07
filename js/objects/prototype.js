function Student() {
    this.name = 'John';
    this.gender = 'M';
}
console.log(Student.prototype);  // Student {} - an empty object

Student.prototype.age = 15;
Student.prototype.addedMethod = function() {
    return this.name + " " + this.gender;
}
console.log(Student.prototype);  // Student { age:15, addedMethod: [Function (anonymous)] }

var studObj1 = new Student();
var studObj2 = new Student();
console.log(studObj1.age); // 15
console.log(studObj2.age); // 15

var studObj = new Student();
console.log(Student.prototype); // object
console.log(studObj.prototype); // undefined
console.log(studObj.__proto__); // object
console.log(typeof Student.prototype); // object
console.log(typeof studObj.__proto__); // object
console.log(Student.prototype === studObj.__proto__ ); // true
console.log(Student.prototype === Object.getPrototypeOf(studObj1)); // true
