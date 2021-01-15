import { sum, pi, v } from './export.mjs';
import { add as newAdd, pi as newPi} from './export.mjs';
import defFunction from './export.mjs';
import * as MyModule from './export.mjs';
import { Square } from './export.mjs';

console.log(pi, sum(1,2), v);
console.log(newPi, newAdd(1,2));
defFunction();
MyModule.sum(1,2);
// v = 10;  error, cant change the value (treated as const)