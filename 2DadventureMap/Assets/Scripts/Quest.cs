using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public string title;
    public string description;
    public int gold;
    public QuestGoal goal;
    public void Complete()
    {
        Debug.Log(title+" is finished !!! "+goal.requiredAmount+" "+goal.EnemyType.ToString() + " Killed");
    }
}
