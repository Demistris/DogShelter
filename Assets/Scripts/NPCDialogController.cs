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
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private string[] _dialogueLines; // Array of dialogue lines
    private int _currentLineIndex = 0;
    private bool _isInDialogue = false;

    private bool _isPlayerInRange = false;

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
        ShowDialogueLine();

        // Disable player movement
        _player.enabled = false;
        _camera.enabled = false;
    }

    private void ContinueDialog()
    {
        _currentLineIndex++;

        if(_currentLineIndex < _dialogueLines.Length)
        {
            ShowDialogueLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowDialogueLine()
    {
        _dialogueText.text = _dialogueLines[_currentLineIndex];
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
