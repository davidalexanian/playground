interface CanRun {
    run(meters: number): void;
}
interface CanEat {
    eat(food:string) : void;
}
class Pony implements CanRun, CanEat {
    speed = 10;     // public by default
    private privateMember = '';
    
    eat(food: string): void {
        console.log(`pony eats ${food}`);
    }
    run(meters): void {
        console.log(`pony runs ${meters}m`);
    }
}

class Animal {
    // shorthand to define public/private members
    constructor(public name: string, private speed: number) {
    }
}

let p = new Pony();
p.run(10);
p.eat('grass');

let a = new Animal('horse', 100);
// a.speed - private member not accessible in ts