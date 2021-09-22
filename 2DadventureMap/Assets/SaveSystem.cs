using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public static bool LOAD;
    void Start()
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
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayer();
            Debug.Log("Saved");
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadPlayer();
            Debug.Log("Load");
        }
    }
    public void SavePlayer()
    {
        PlayerData playerData = new PlayerData();
        playerData.playerName = PlayerValues.PlayerName;
        playerData.health = PlayerValues.Health;
        playerData.damage = PlayerValues.DamageValue;
        playerData.speed = (int)PlayerValues.RunValue;
        playerData.gold = PlayerValues.GoldNumber;
        playerData.healthPotion = PlayerValues.HealthPotionNumber;
        playerData.speedPotion = PlayerValues.SpeedPotionNumber;
        Vector3 pos = GameObject.Find("Player").GetComponent<Transform>().position;
        playerData.position = new float[] { pos.x, pos.y, pos.z };
        playerData.scene = SceneManager.GetActiveScene().name;

        JsonHelper.savePlayer(playerData);
    }
    public void LoadPlayer()
    {
        LOAD = true;
        PlayerData data = JsonHelper.LoadPlayer();
        PlayerValues.PlayerName = data.playerName;
        PlayerValues.Health = data.health;
        PlayerValues.DamageValue = data.damage;
        PlayerValues.RunValue = data.speed;
        PlayerValues.GoldNumber = data.gold;
        PlayerValues.HealthPotionNumber = data.healthPotion;
        PlayerValues.SpeedPotionNumber = data.speedPotion;
        PlayerValues.position = data.position;

        Player.FirstStart = false;
        if (SceneManager.GetActiveScene().name.Equals(data.scene))
        {
            GameObject.Find("Player").GetComponent<Transform>().position = new Vector3(PlayerValues.position[0], PlayerValues.position[1], PlayerValues.position[2]);
            GameObject.Find("Player").GetComponent<Player>().renderUI();
        }
        else
        {
            SceneManager.LoadScene(data.scene);
        }

    }
}
