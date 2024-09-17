using UnityEngine;

[CreateAssetMenu(fileName = "New CollectItemsQuest", menuName = "Quest/Collect Items")]
public class CollectItemsQuest : Quest
{
    public int RequiredItemCount;
    public string ItemTag;

    private int _currentItemCount = 0;

    public override bool CheckCompletion()
    {
        return _currentItemCount >= RequiredItemCount;
    }

    public override void StartQuest()
    {
        _currentItemCount = 0;
    }

    public void CollectItem()
    {
        if(!IsCompleted)
        {
            _currentItemCount++;
            Debug.Log($"Collected {_currentItemCount}/{RequiredItemCount} items.");
        }
    }

    public string GetItemCollectionProgress()
    {
        return $"{_currentItemCount}/{RequiredItemCount}";
    }
}
