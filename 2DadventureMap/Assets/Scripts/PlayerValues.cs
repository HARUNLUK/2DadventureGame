using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues 
{
    public static string PlayerName;
    public static int Health;
    public static int DamageValue;
    public static float RunValue;
    public static float JumpForce;

    public static int GoldNumber;
    public static int HealthPotionNumber;
    public static int SpeedPotionNumber;

    public static float[] position = new float[3];

    public static List<Quest> Quests = new List<Quest>();
    public static Quest CurrentQuest;
}
