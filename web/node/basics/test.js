const express = require('express')
const app = express()
app.get('/', (req, res) => {
    res.send('Hi!')
});
let server = app.listen(3000, () => console.log('Server ready'))

process.on("SIGNTERM", () => {
    console.log("Terminating process");
    server.close( () => console.log(err));
});