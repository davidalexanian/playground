//-------------------- let, const, union types
console.log('-------------------- let, const, union types');
const ponyName: string = 'Rainbow Dash';

let changing: number | boolean = 2;
changing = true; // no problem

let anyVar: any = 2;
anyVar = new Date();

// union types (we can only give one of these values to `color`)
let color: 'blue' | 'red' | 'green';
color = 'blue';


//-------------------- enums
console.log('-------------------- enums');
enum RaceStatus {
    Ready = 1,
    Started,
    Done = 'done'
}
console.log(RaceStatus.Started);    // 2
console.log(RaceStatus.Done);       // done


//-------------------- arrays
console.log('-------------------- arrays');
class Race {     
}
const ponies: Array<Race> = [new Race()];
ponies.push(new Race());


