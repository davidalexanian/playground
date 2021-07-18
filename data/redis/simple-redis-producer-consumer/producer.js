let redis = require('redis');
let redisClient = redis.createClient({
    port : 63799
});

let queueName = 'logs'
let counter = 0;
let handle = setInterval(() => {
    redisClient.LPUSH(queueName, ++counter);
    console.log(`Pushed ${counter} message`);
}, 1000);
