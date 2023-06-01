using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Creates a new list specifically for the leaderbaords use
[Serializable]
public class LeaderboardSaveData
{
    public List<playerData> highscores = new List<playerData>();
}
