var Pony = /** @class */ (function () {
    function Pony() {
        this.speed = 10; // public by default
        this.privateMember = '';
    }
    Pony.prototype.eat = function (food) {
        console.log("pony eats " + food);
    };
    Pony.prototype.run = function (meters) {
        console.log("pony runs " + meters + "m");
    };
    return Pony;
}());
var Animal = /** @class */ (function () {
    function Animal(name, speed) {
        this.name = name;
        this.speed = speed;
    }
    return Animal;
}());
var p = new Pony();
p.run(10);
p.eat('grass');
