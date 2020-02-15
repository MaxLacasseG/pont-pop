var express = require("express");
var app = express();
var path = require("path");
const logger = require("tracer").colorConsole();
const bodyParser = require("body-parser");
const GameController = require("./controller/GameController");
const mongoose = require("mongoose");

const db = process.env.MONGO_URI;
mongoose.connect("mongodb://localhost:27017/pontpop", { useNewUrlParser: true }, err => {
    if (err) return logger.log(err);
    logger.trace("Connecté à la base de donnée");
});

app.set("view engine", "ejs");
app.use(express.static("build"));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// viewed at http://localhost:8080
app.get("/", function(req, res) {
    res.sendFile(path.join(__dirname + "/index.html"));
});

app.get("/get-final-info", function(req, res) {
    GameController.GetData()
        .then(res => {
            return res.status(200).json(res);
        })
        .catch(err => {
            return res.status(400).json(err);
        });
});

app.post("/save-final-info", function(req, res) {
    logger.info(req.body);
    GameController.SaveData(req.body)
        .then(saveData => {
            console.log(saveData);

            return res.send("Enregistrement réussi");
        })
        .catch(err => {
            console.log(err);

            return res.send(err);
        });
});

//404 page catch all
app.get("/*", function(req, res) {
    res.sendFile(path.join(__dirname + "/index.html"));
});
app.listen(5000);
