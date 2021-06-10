'use strict';

const Fs = require('fs')  
const Path = require('path')  
const Axios = require('axios')

async function downloadImage (url, path) {  
  const writer = Fs.createWriteStream(path)

  const response = await Axios({
    url,
    method: 'GET',
    responseType: 'stream'
  })
  response.data.pipe(writer)
  return new Promise((resolve, reject) => {
    writer.on('finish', resolve)
    writer.on('error', reject)
  })
}

let jsonData = require('./categories.json');
console.log('total categories:'+jsonData.categories.length);

let counter = 0;
let path = 'C:\\Workspace\\Prof\\code\\playground\\node\\removeField\\images';

jsonData.categories.forEach(element => {
    element.images.forEach(async i => {
        counter++;
        await downloadImage(i, path+'\\'+counter+'.jpg');
        console.log(counter);
    });    
});

console.log("You waited: " + difference + " seconds");