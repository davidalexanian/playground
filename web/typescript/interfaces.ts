// interface with props and methods
interface HasScore {
    score: number;
    optionalArg?: string;
    showScore(n : number) : void;
}
function addPointsToScore2(player: HasScore, points: number): void {
    player.score += points;
    player.showScore(player.score);
}
const scoreArg1: HasScore = { 
    score: 1, 
    showScore : (n:number) => console.log(`argument=${n}`) 
};
const scoreArg2 = { 
    score: 2,
    showScore : scoreArg1.showScore
};
addPointsToScore1(scoreArg1, 1, 'optional');
addPointsToScore2(scoreArg1, 1);
addPointsToScore2(scoreArg2, 1);