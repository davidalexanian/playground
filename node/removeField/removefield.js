'use strict';

const fs = require('fs');
let jsonData = require('./categories.json');
console.log(jsonData.categories.length);

jsonData.categories.forEach(element => {
    element.images = []
});

fs.writeFileSync('categories_converted.json', JSON.stringify(jsonData));
