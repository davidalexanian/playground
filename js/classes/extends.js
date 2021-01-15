class Animal {
    constructor(name) {
        this.name = name;
    }
    speak() {
        console.log(`${this.name} makes a noise.`);
    }
}

class Dog extends Animal {
    constructor(name) {
        // If there is a constructor present in the subclass, it needs to first call super() before using "this"
        super(name);
    }
    speak() {
        super.speak();
        console.log(`${this.name} barks.`);
    }
}

let d = new Dog('Mitzie');
d.speak(); // Mitzie barks.


// extending a function
function Animal1(name) {
    this.name = name;
}
Animal.prototype.speak = function () {
    console.log(`${this.name} makes a noise.`);
}

class Dog1 extends Animal1 {
    speak() {
        console.log(`${this.name} barks.`);
    }
}
let d1 = new Dog1('Mitzie');
d1.speak(); // Mitzie barks.