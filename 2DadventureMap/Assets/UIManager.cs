using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Player player;
    public GameObject deadScene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        
    }
    public void DrinkPotionHealth()
    {
        player.DrinkHealthPotion();
    }
    public void DrinkPotionSpeed()
    {
        player.DrinkSpeedPotion();
    }
    public void DeadScene()
    {
        deadScene.SetActive(true);
    }
}
public enum Potion
{
    HEALTH,
    SPEED
}
