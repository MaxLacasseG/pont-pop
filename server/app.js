var express = require("express");
var app = express();
var path = require("path");
const logger = require("tracer").colorConsole();
const bodyParser = require("body-parser");

app.set("view engine", "ejs");
app.use(express.static("build"));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// viewed at http://localhost:8080
app.get("/", function(req, res) {
    res.sendFile(path.join(__dirname + "/index.html"));
});
app.get("/jeu", function(req, res) {
    res.sendFile(path.join(__dirname + "/build/index.html"));
});

app.post("/test", function(req, res) {
    logger.info(req.body);
    res.send(true);
});
app.post("/admin", function(req, res) {
    res.sendFile(path.join(__dirname + "/admin.html"));
});
app.post("/auth", function(req, res) {
    //Check everything
    res.sendFile(path.join(__dirname + "/logged.html"));
});
app.post("/toggle-game", function(req, res) {
    //Check everything
    res.sendFile(path.join(__dirname + "/logged.html"));
});

//404 page catch all
app.get("/*", function(req, res) {
    res.sendFile(path.join(__dirname + "/404.html"));
});
app.listen(5000);
