const GameData = require("../models/GameData");
const logger = require("tracer").colorConsole();
const controller = {};
controller.GetData = () => {
    GameData.find({})
        .then(res => {
            return res;
        })
        .catch(err => {
            throw err;
        });
};

controller.SaveData = gameInfos => {
    const { gameId, team_number, final_score, team_pwd } = gameInfos;
    return GameData.find({ gameId, team_number })
        .then(res => {
            logger.warn(res);

            if (res.length === 0) {
                const newGame = new GameData(gameInfos);
                newGame.datetime = new Date();
                return newGame.save();
            }
            //if (res[0].team_pwd !== team_pwd) throw "Mot de passe invalide";
            //res[0].score = score;
            return res[0].save();
        })
        .then(res => {
            console.log(res);

            return res;
        })
        .catch(err => {
            throw err;
        });
};

module.exports = controller;
