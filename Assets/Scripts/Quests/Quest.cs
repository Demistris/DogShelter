using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public string QuestName;
    public bool IsCompleted { get; private set; }
    
    public abstract bool CheckCompletion();
    public abstract void StartQuest();

    public void CompleteQuest()
    {
        IsCompleted = true;
    }

    public void ResetQuestCompletion()
    {
        IsCompleted = false;
    }
}
