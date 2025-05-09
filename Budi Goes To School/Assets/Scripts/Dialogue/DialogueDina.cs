using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueDina : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    [SerializeField] public string[] speaker;
    [SerializeField][TextArea] private string[] dialogueWords;
    [SerializeField] private Sprite[] portrait;
    [SerializeField] private GameObject npcVCam;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject npcWall;

    private bool dialogueActivated;
    private int currentDialogueIndex = 0;

    void Update()
    {
        if (dialogueActivated && Input.GetKeyDown(KeyCode.E))
        {
            if (npcVCam != null) npcVCam.SetActive(true);
            if (interactText != null) interactText.SetActive(false);

            if (currentDialogueIndex >= speaker.Length)
            {
                EndDialogue();
                if (dialogueCanvas != null) dialogueCanvas.SetActive(false);
            }
            else
            {
                if (dialogueCanvas != null) dialogueCanvas.SetActive(true);
                if (speakerText != null) speakerText.text = speaker[currentDialogueIndex];
                if (dialogueText != null) dialogueText.text = dialogueWords[currentDialogueIndex];
                if (portraitImage != null) portraitImage.sprite = portrait[currentDialogueIndex];

                currentDialogueIndex += 1;
            }
        }
    }

    private void EndDialogue()
    {
        currentDialogueIndex = 0;
        if (npcVCam != null) npcVCam.SetActive(false);
        if (interactText != null) interactText.SetActive(true);
        if (npcWall != null) npcWall.SetActive(false);
        SceneManager.LoadScene("ClassroomScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueActivated = true;
            if (interactText != null) interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueActivated = false;
            if (npcVCam != null) npcVCam.SetActive(false);
            if (interactText != null) interactText.SetActive(false);
            if (dialogueCanvas != null) dialogueCanvas.SetActive(false);
        }
    }
}

