using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public int requiredAmount;
    public int cureentAmount;
    public EnemyTypes EnemyType;
    public bool IsReached()
    {
        return (cureentAmount >= requiredAmount);
    }

    public bool Killed(EnemyTypes enemy)
    {
        if (enemy == EnemyType)
        {
            cureentAmount++;
            return IsReached();
        }
        return false;
    }
}
public enum EnemyTypes
{
    GIANT,
    SCALETON,
    THIEF,
    FLIGHT,
    GOBLIN
}