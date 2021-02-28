let redis = require('redis');
let redisClient = redis.createClient({
    port: 63799
});
let queueName = 'logs';
function popAndLogMssages() {
    redisClient.brpop(queueName, 0, function (err, replies) {
        if (err) console.log(err);
        console.log('Pop message:' + replies[1]);
        popAndLogMssages();
    });
};
popAndLogMssages();