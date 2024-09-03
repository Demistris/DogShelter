using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialogController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _dialogueUI; // The UI panel for the dialogue
    [SerializeField] private TextMeshProUGUI _dialogueText; // Text component for showing dialogue lines

    [Header("Dialogue")]
    [SerializeField] private string[] _initialDialogueLines;// Dialogue before the quest starts
    [SerializeField] private string[] _questInProgressLines; // Dialogue while the quest is in progress
    [SerializeField] private string[] _questCompletedLines; // Dialogue after the quest is completed
    [SerializeField] private string[] _questBlockedLines; // Dialogue if the player is doing another quest
    private string[] _currentdialogueLines;
    private int _currentLineIndex = 0;
    private bool _isInDialogue = false;

    [Header("Player")]
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private CameraFollow _camera;
    private bool _isPlayerInRange = false;

    [SerializeField] private Quest _quest;

    private void Start()
    {
        _dialogueUI.SetActive(false);
    }

    private void Update()
    {
        if(_isPlayerInRange && !_isInDialogue && Input.GetKeyDown(KeyCode.Q))
        {
            StartDialogue();
        }

        if(_isInDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialog();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }

    private void StartDialogue()
    {
        _isInDialogue = true;
        _currentLineIndex = 0;
        _dialogueUI.SetActive(true);

        // Get the correct dialogue lines based on the current quest state
        _currentdialogueLines = GetCurrentDialogueLines();
        ShowDialogueLine(_currentdialogueLines);

        // Disable player movement
        _player.enabled = false;
        _camera.enabled = false;
    }

    private void ContinueDialog()
    {
        _currentLineIndex++;

        if (_currentLineIndex < _currentdialogueLines.Length)
        {
            ShowDialogueLine(_currentdialogueLines);
        }
        else
        {
            EndDialogue();
        }
    }

    private string[] GetCurrentDialogueLines()
    {
        Quest activeQuest = QuestManager.Instance.GetActiveQuest();

        if (activeQuest == null)
        {
            if (!_quest.IsCompleted)
            {
                // Start the quest
                QuestManager.Instance.StartQuest(_quest, this);
                return _initialDialogueLines;
            }
            else
            {
                // This NPC's quest is already completed
                return _questBlockedLines;
            }
        }
        else
        {
            if (activeQuest == _quest)
            {
                if (!_quest.IsCompleted)
                {
                    // This quest is active but not completed
                    return _questInProgressLines;
                }
                else
                {
                    QuestManager.Instance.CompleteQuest();
                    return _questCompletedLines;
                }
            }
            else
            {
                // Quest of another NPC is active
                return _questBlockedLines;
            }
        }
    }

    private void ShowDialogueLine(string[] dialogueLines)
    {
        _dialogueText.text = dialogueLines[_currentLineIndex];
    }

    private void EndDialogue()
    {
        _isInDialogue = false;
        _dialogueUI.SetActive(false);

        // Enable player movement
        _player.enabled = true;
        _camera.enabled = true;
    }
}
