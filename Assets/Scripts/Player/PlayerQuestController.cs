using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestController : MonoBehaviour
{
    private Quest _activeQuest;

    public void QuestStarted(Quest quest)
    {
        _activeQuest = quest;
    }

    public void CollectItem(GameObject go)
    {
        if (_activeQuest == null)
        {
            return;
        }

        QuestManager.Instance.CollectItem();
        QuestManager.Instance.CheckQuest();
        Destroy(go);
    }
}