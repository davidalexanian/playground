export function sum(x, y) { 
    return x + y;
}
export const pi = 3.141593;
var v = 10;


// export by default 
export default function defaultFunct() {
    console.log('defaultFunct');
}
// or export default defaultFunct

// give an alias while exporting
export {
    sum as add,
    pi as pi_num, // trailing comma is optional
    v
};

export class Square {
}
// or export {Square};