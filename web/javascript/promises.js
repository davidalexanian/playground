export function promisesDemo() {
    // resolve immediately
    var p1 = Promise.resolve("foo");    
    p1.then((res) => console.log(res));
    
    // resolve after 2 seconds
    var p2 = new Promise(function (resolve, reject) {
        setTimeout(() => resolve(4), 2000);
    });
    p2.then((res) => console.log(res)); // 4

    // reject the promise
    var p = new Promise(function(resolve, reject) { 
        setTimeout(() => reject("Timed out!"), 2000); 
    }); 
    p.then(
        (res) => console.log('wont execute'), 
        (err) => console.log(err));

    // error
    var p = new Promise(function(resolve, reject) { 
        setTimeout(() => {
            throw new Error("Error encountered!");
        }, 2000); }); 
    
    p.then((res) => console.log("Wont execute:", res))
     .catch((err) => console.log("Error:", err));
}

export function promiseAll() {
    var fileUrls = [ 
        'http://example.com/file1.txt', 
        'http://example.com/file2.txt' 
    ]; 
    var httpGet = function(item) { 
        return new Promise(function(resolve, reject) { 
            setTimeout(() => resolve(item), 2000); 
        }); 
    }; 
    var promisedTexts = fileUrls.map(httpGet); 
    Promise.all(promisedTexts) 
    .then(function (texts) { 
        texts.forEach(function (text) { 
            console.log(text);
         });
    })
    .catch(function (reason) { 
        console.log(reason); // Receives first rejection among the promises 
    });
}

export function promiseRace() {
    function delay(ms) { 
        return new Promise((resolve, reject) => { 
            setTimeout(resolve, ms); 
        }); 
    } 
    Promise
        .race([
            delay(3000).then(() => "I finished second."),
            delay(2000).then(() => "I finished first.")
        ])
        .then(
            function (txt) {
                console.log(txt);
            })
        .catch(function (err) {
            console.log("error:", err);
        });
}
