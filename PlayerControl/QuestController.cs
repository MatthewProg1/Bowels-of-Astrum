using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField]private QuestBase startQuest;

    [SerializeField] private QuestList questList;

    void Start()
    {
        questList.AddQuest(startQuest);
    }

    public void SearchKillingQuests(GameObject gameObject)
    {
        for (int i = 0; i < questList.quests.Count; i++)
        {
            if (questList.quests[i].KillSomeMonsters)
            {
                //     questList.quests[i].ChangeCount(1);
                questList.questsGoals[questList.quests[i].NameQuest] -= 1;

                if(questList.questsGoals[questList.quests[i].NameQuest] == 0)
                {
                    questList.DoneQuest(questList.quests[i]);
                }
            }
        }
    }

    public void SearchTakingQuests(InventoryItemObject gameObject)
    {
        for (int i = 0; i < questList.quests.Count; i++)
        {
            if (questList.quests[i].TakeSomethings)
            {
                //     questList.quests[i].ChangeCount(1);
                questList.questsGoals[questList.quests[i].NameQuest] -= 1;

                if (questList.questsGoals[questList.quests[i].NameQuest] == 0)
                {
                    questList.DoneQuest(questList.quests[i]);
                }
            }
        }
    }


}
