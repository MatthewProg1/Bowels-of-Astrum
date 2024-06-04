using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject
{
    [SerializeField] private string nameQuest;
    [SerializeField] private string description;
    [SerializeField] private int reward;
    [SerializeField] private bool kill;
    [SerializeField] private bool take;
    [SerializeField] private int count;
    [SerializeField] private GameObject objectType;

    

    public string NameQuest => nameQuest;
    public string DiscriptionQuest => description;
    public int Reward => reward;
    public bool KillSomeMonsters => kill;
    public bool TakeSomethings => take;

    public GameObject ObjectType => objectType;

    public int Count => count;

    public string GetQuestName()
    {
        return nameQuest;
    }
    public string GetQuestDescription()
    {
        return description;
    }
    public int GetReward()
    {
        return reward;
    }

  

}
