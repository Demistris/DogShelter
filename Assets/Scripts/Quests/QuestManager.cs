using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] private PlayerQuestController _player;
    [SerializeField] private GameObject _collectItemsUI;
    [SerializeField] private TextMeshProUGUI _collectItemsText;
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
            newQuest.StartQuest();
            _player.QuestStarted(newQuest);

            if(newQuest is CollectItemsQuest collectQuest)
            {
                _collectItemsUI.SetActive(true);
                _collectItemsText.text = collectQuest.GetItemCollectionProgress();
            }
        }
    }

    public void CompleteQuest()
    {
        if (_activeQuest != null)
        {
            Debug.Log($"Quest Completed: {_activeQuest.QuestName}");

            if (_activeQuest is CollectItemsQuest)
            {
                _collectItemsUI.SetActive(false);
            }

            _activeQuest = null;
        }
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

    public void CollectItem()
    {
        if(_activeQuest is CollectItemsQuest collectQuest)
        {
            collectQuest.CollectItem();
            _collectItemsText.text = collectQuest.GetItemCollectionProgress();
        }
    }
}