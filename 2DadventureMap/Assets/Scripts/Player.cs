using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public string playerName;
    public int health;
    public int damageValue;
    [Range(0, 100f)] public float runSpeed;
    [Range(0, 100f)] public float jumpForce;

    public int gold;
    public int healthPotion;
    public int speedPotion;

    public static List<Quest> Quests = new List<Quest>();
    public Quest currentQuest;
    float renderTime=0;
    public static bool FirstStart = true;
    void Start()
    {
        instance = this;
        if (SaveSystem.LOAD)
        {
            transform.position = new Vector3(PlayerValues.position[0], PlayerValues.position[1], PlayerValues.position[2]);
            SaveSystem.LOAD = false;
        }
        PlayerValues.JumpForce = jumpForce;

        if (FirstStart)
        {
            Debug.Log("First Start");
            FirstStart = false;
            LoadValues();
        }
        
        renderUI();
        nextQuest();
    }
    void LoadValues()
    {
        PlayerValues.PlayerName = playerName;
        PlayerValues.Health = health;
        PlayerValues.DamageValue = damageValue;
        PlayerValues.RunValue = runSpeed;
        PlayerValues.JumpForce = jumpForce;

        PlayerValues.GoldNumber = gold;
        PlayerValues.HealthPotionNumber = healthPotion;
        PlayerValues.SpeedPotionNumber = speedPotion;

        PlayerValues.CurrentQuest = null;
    }

    void Update(){
        if (renderTime < Time.time)
        {
            GameObject.Find("Hero").GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }
    
    public void setCurrentQuest(Quest quest)
    {
        PlayerValues.CurrentQuest = quest;
        setQuestUI(quest.title, quest.description.Substring(quest.description.IndexOf("\n")));
    }
    public void addQuest(Quest quest)
    {
        PlayerValues.Quests.Add(quest);
        foreach(Quest quest1 in PlayerValues.Quests)
            {
                Debug.Log(quest1.title);
            }
        if (PlayerValues.Quests.Count == 1)
        {
            setCurrentQuest(quest);
        }
    }
    public void nextQuest()
    {
        setQuestUI("","");
        if (PlayerValues.Quests.Count>0)
        {
            setCurrentQuest(PlayerValues.Quests[0]);
        }
        else
        {
            PlayerValues.CurrentQuest = null;

        }
    }
    void setQuestUI(string title, string description)
    {
        GameObject.Find("QuestTitle").GetComponent<Text>().text = title;
        GameObject.Find("QuestDescription").GetComponent<Text>().text = description;
    }
    public void addGold(int value)
    {
        PlayerValues.GoldNumber += value;
        renderUI();
    }
    public void TakeDamage(int value)
    {
        if (renderTime < Time.time)
        {
            PlayerValues.Health -= value;
            renderUI();
            if (PlayerValues.Health <= 0)
            {
                Die();
            }
            GameObject.Find("Hero").GetComponent<SpriteRenderer>().color = Color.red;
            renderTime = Time.time + .2f;
        }
        
    }
    void addHealth(int newHealth)
    {
        if (PlayerValues.Health < 100)
        {
            PlayerValues.Health += newHealth;
        }
        if (PlayerValues.Health > 100)
        {
            PlayerValues.Health = 100;
        }
        renderUI();
    }
    public void renderUI()
    {
        GameObject.Find("HealthBar").GetComponent<HealthBarScript>().SetSlider(PlayerValues.Health);
        GameObject.Find("CoinText").GetComponent<Text>().text = PlayerValues.GoldNumber.ToString()+"  ";
        GameObject.Find("HealthCount").GetComponent<Text>().text = PlayerValues.HealthPotionNumber.ToString();
        GameObject.Find("SpeedCount").GetComponent<Text>().text = PlayerValues.SpeedPotionNumber.ToString();
    }
    public void addHealthPotion()
    {
        if (PlayerValues.GoldNumber >= 100)
        {
            PlayerValues.GoldNumber -= 100;
            PlayerValues.HealthPotionNumber++;
            renderUI();
        }
    }
    public void addSpeedPotion()
    {
        if (PlayerValues.GoldNumber >= 100)
        {
            PlayerValues.GoldNumber -= 100;
            PlayerValues.SpeedPotionNumber++;
            renderUI();
        }
    }
    public void DrinkHealthPotion()
    {
        if (PlayerValues.HealthPotionNumber <= 0 || PlayerValues.Health >= 100)
        {
            return;
        }
        addHealth(25);
        PlayerValues.HealthPotionNumber--;
        renderUI();
    }
    public void DrinkSpeedPotion()
    {
        if (PlayerValues.SpeedPotionNumber > 0 && PlayerValues.RunValue < 50)
        {
            speedPotionEffect = true;
            PlayerValues.RunValue = PlayerValues.RunValue * 1.5f;
            PlayerValues.SpeedPotionNumber--;
            renderUI();
            instance.PotionEffecT();
        }
    }
    void PotionEffecT() {
        StartCoroutine(WaitandClearSpeedPotionEffects(30.0f));
    }
    bool speedPotionEffect = false;
    public IEnumerator WaitandClearSpeedPotionEffects(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("speed potion end");
        speedPotionEffect = false;
        PlayerValues.RunValue = 25f;
    }
    public void killed(EnemyTypes enemyType)
    {
        List<Quest> finishedQuests = new List<Quest>();
        if (PlayerValues.Quests.Count > 0)
        {
            foreach (Quest quest in PlayerValues.Quests)
            {
                if (quest.goal.Killed(enemyType))
                {
                    quest.Complete();
                    SaveSystem.instance.SavePlayer();
                    FindObjectOfType<AudioManager>().Play("QuestComplete");
                    addGold(quest.gold);
                    finishedQuests.Add(quest);

                }
            }
            foreach (Quest quest in finishedQuests)
            {
                PlayerValues.Quests.Remove(quest);
            }
            nextQuest();
        }
    }
    void Die()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        UIManager.instance.DeadScene();
    }
    
}
