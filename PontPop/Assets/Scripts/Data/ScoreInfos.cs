using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScoreInfos {
    [SerializeField]
    public string dateTime;
    public string gameId;
    public string team_number;
    public string team_pwd;
    public int score;
    public int attempts;
    public string type;
    public string msg;

}