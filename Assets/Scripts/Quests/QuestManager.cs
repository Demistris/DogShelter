using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private Quest _activeQuest;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartQuest(Quest newQuest, NPCDialogController npc)
    {
        if(_activeQuest == null)
        {
            Debug.Log($"Quest Started: {newQuest.QuestName}");
            _activeQuest = newQuest;
        }
    }

    public void CompleteQuest()
    {
        _activeQuest = null;
    }

    public Quest GetActiveQuest()
    {
        return _activeQuest;
    }

    public void CheckQuest()
    {
        if(_activeQuest != null && !_activeQuest.IsCompleted && _activeQuest.CheckCompletion())
        {
            _activeQuest.CompleteQuest();
        }
    }
}