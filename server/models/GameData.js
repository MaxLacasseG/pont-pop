const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const GameDataSchema = new Schema({
    gameId: { type: String, required: true },
    datetime: { type: Date, required: true },
    teamName: { type: String, required: true },
    score: { type: String, required: true }
});

module.exports = mongoose.model("Region", GameDataSchema);
