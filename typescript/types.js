//-------------------- let, const, union types
console.log('-------------------- let, const, union types');
var ponyName = 'Rainbow Dash';
var changing = 2;
changing = true; // no problem
// union types (we can only give one of these values to `color`)
var color;
color = 'blue';
//-------------------- enums
console.log('-------------------- enums');
var RaceStatus;
(function (RaceStatus) {
    RaceStatus[RaceStatus["Ready"] = 1] = "Ready";
    RaceStatus[RaceStatus["Started"] = 2] = "Started";
    RaceStatus["Done"] = "done";
})(RaceStatus || (RaceStatus = {}));
console.log(RaceStatus.Started); // 2
console.log(RaceStatus.Done); // done
//-------------------- arrays
console.log('-------------------- arrays');
var Race = /** @class */ (function () {
    function Race() {
    }
    return Race;
}());
var ponies = [new Race()];
ponies.push(new Race());
