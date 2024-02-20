import defaultFunct, { sum, pi, v } from './module-export.mjs';
import { add as newAdd, pi as newPi} from './module-export.mjs';
import * as MyModule from './module-export.mjs';
import { Square } from './module-export.mjs';

console.log(pi, sum(1,2), v);
console.log(newPi, newAdd(1,2));
defaultFunct()
MyModule.sum(1,2);
// v = 10;  error, cant change the value (treated as const)