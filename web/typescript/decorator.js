var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var RaceService = /** @class */ (function () {
    function RaceService() {
    }
    RaceService.prototype.getRaces = function () {
        // call API
    };
    __decorate([
        Log()
    ], RaceService.prototype, "getRaces");
    return RaceService;
}());
/*
• target: the method targeted by our decorator
• name: the name of the targeted method
• descriptor: a descriptor of the targeted method (is the method enumerable, writable, etc.)
*/
var Log = function () {
    return function (target, name, descriptor) {
        console.log("Method " + name + " called");
        return descriptor;
    };
};
