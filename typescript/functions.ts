// void function
function voidFunction(num: number): void {
    console.log(num);
}

// return type
function startRace(): string {
    return 'str';
}

// argument types
function greet(person: string, date: Date) {
    console.log(`Hello ${person}, today is ${date.toDateString()}!`);
}

// parameter must have a property called score of the type number
function addPointsToScore1(player: { score: number }, points: number, optionalArg?: string): void {
    player.score += points;
    optionalArg = optionalArg || 'not specified'
}