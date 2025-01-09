using System.Collections.Generic;
using PlasticGui.WorkspaceWindow.Update;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Pnj : MonoBehaviour, IIteraction
{
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TextMeshProUGUI _textZone;

    [SerializeField] private List<string> sentences;
    
    [SerializeField] private InputActionReference _inputAction;
    
    //Private field
    private bool _inputTrigger;
    private bool _canTalk;
    private int nextSentencesIndex;
    
    [SerializeField] private InputStates _inputState;
    public InputStates InputState { get; set; }
    
    private void Start()
    {
        InputState = _inputState;
        nextSentencesIndex = 0;
    }

    public void StartInteraction(InteractableDetector id)
    {
        if (nextSentencesIndex == 0)
        {
            ShowDialog();
        }
        else
        {
            UpdateDialog();
        }
    }

    public void EndInteraction(InteractableDetector id)
    {
        HideDialog();
    }

    void ShowDialog()
    {
        _dialogBox.SetActive(true);
        _textZone.text = sentences[nextSentencesIndex];
        nextSentencesIndex++;
    }

    void UpdateDialog()
    {
        if (nextSentencesIndex < sentences.Count)
        {
            _textZone.text = sentences[nextSentencesIndex];
            nextSentencesIndex++;
        }
        else
        {
            HideDialog();
        }
    }
    
    void HideDialog()
    {
        _dialogBox.SetActive(false);
        nextSentencesIndex = 0;
    }
}
