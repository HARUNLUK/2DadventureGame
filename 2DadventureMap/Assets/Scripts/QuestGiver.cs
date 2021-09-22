using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Text questTitle;
    public Text questDescription;
    private void Start()
    {
        questTitle.text = quest.title;
        quest.description += "\n\n Goal : "+"Kill " + quest.goal.requiredAmount + " "+quest.goal.EnemyType.ToString();
        questDescription.text = quest.description;
    }
    public void giveQuest()
    {
        GameObject.Find("Player").GetComponent<Player>().addQuest(quest);
    }
}
