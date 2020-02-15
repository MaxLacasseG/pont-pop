const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const GameDataSchema = new Schema({
    gameId: { type: String, required: true },
    datetime: { type: Date, required: true },
    team_number: { type: String, required: true },
    team_pwd: { type: String, required: true },
    score: { type: String, required: true }
});

module.exports = mongoose.model("GameData", GameDataSchema);
