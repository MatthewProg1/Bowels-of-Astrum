using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    [SerializeField] private UIcontrol uIcontrol;
    [SerializeField] private ShoppingControl shoppingControl;

    public List<QuestBase> quests = new List<QuestBase>();
    public Dictionary<string, int> questsGoals = new Dictionary<string, int>();

    public void DoneQuest(QuestBase quest)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if(quests[i] == quest)
            {
                shoppingControl.PlusMoney(quests[i].Reward);
                uIcontrol.GetInventoryManager().QuestName.text = "";
                uIcontrol.GetInventoryManager().QuestDiscription.text = "";
                uIcontrol.GetInventoryManager().QuestReward.text = "";
                quests.Remove(quests[i]);
            }

        }
    }

    public void AddQuest(QuestBase questBase)
    {
        quests.Add(questBase);
        questsGoals.Add(questBase.NameQuest, questBase.Count);
        uIcontrol.GetInventoryManager().QuestName.text = questBase.GetQuestName();
        uIcontrol.GetInventoryManager().QuestDiscription.text = questBase.GetQuestDescription();
        uIcontrol.GetInventoryManager().QuestReward.text = "Reward:" + questBase.GetReward().ToString();
    }
}
