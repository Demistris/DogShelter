using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string QuestName;
    public string[] CompletionConditions; //PLACEHOLDER REPLACE LATER
    public bool IsCompleted { get; private set; }

    public Quest(string questName, string[] completionConditions)
    {
        QuestName = questName;
        CompletionConditions = completionConditions;
        IsCompleted = false;
    }

    public void CompleteQuest()
    {
        IsCompleted = true;
    }

    public void ResetQuestCompletion()
    {
        IsCompleted = false;
    }

    public bool CheckCompletion()
    {
        foreach (var condition in CompletionConditions)
        {
            if(condition == "Complete") //PLACEHOLDER REPLACE LATER
            {
                return true;
            }
        }

        return false;
    }
}
