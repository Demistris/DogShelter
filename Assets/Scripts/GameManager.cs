using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Quest[] _allQuests;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        StartNewGame();
    }

    private void StartNewGame()
    {
        ResetAllQuests();
    }

    private void ResetAllQuests()
    {
        foreach (var quest in _allQuests)
        {
            quest.ResetQuestCompletion();
        }
    }
}
