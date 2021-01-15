// unnamed class expression
let Rectangle1 = class {
    constructor(height, width) {
        this.height = height;
        this.width = width;
    }
};
console.log(Rectangle1.name);                // Rectangle1
console.log( new Rectangle1(10, 20).height); // 10

// named class expression
let rn = class RectangleNew {
    constructor(height, width) {
        this.height = height;
        this.width = width;
    }
};
console.log(rn.name);    // RectangleNew

// class declaration
class Rectangle {
    // .ctor
    constructor(w, h) {
        this._width = w;
        this._height = h;
    }

    // methods
    area() {
        // x is undefined, use this.x
        return this._height * this.width;
    }

    // getter & setters
    get width() {
        return this._width;
    }
    set width(v) {
        this._width = v;
    }

    // static
    static staticField = "staticField";
    static staticMethod() {
        console.log(Rectangle.staticField);

        if (this != undefined) {
            console.log(this.staticField);
            console.log(this);  //[Function: Rectangle] { staticField: 'staticField' }
        }        
        else {
            console.log("this == undefined");
        }
    }
}
let r = new Rectangle(10, 20);
r._height = 100;    // change fieds
console.log(r._height, r.width, r.area());
console.log(Rectangle.staticField, r.staticField); // staticField, undefined

Rectangle.staticMethod();   // this refers to the class
let sm = Rectangle.staticMethod;
sm();   // this is undefined hence staticField is not availble (throws error as in strict mode)
