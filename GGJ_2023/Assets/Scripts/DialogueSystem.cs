using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue dialogue;
    public TMP_Text dialogueText;
    public Image dialoguePanel;
    public Image characterImage;

    private int currentEntry = 0;
    private string currentText = "";
    private bool isTyping = false;
    private bool cancelTyping = false;

    public Color normalColor;
    public Color narratorColor;

    public static GameObject dialoguePanelPrefab;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
                cancelTyping = true;
            else
                DisplayNextEntry();
        }
        if(Input.GetKeyDown(KeyCode.Backspace)) 
        {
            DisplayPreviousEntry();
        }
    }
    public static void CreateDialogue(Dialogue newDialogue)
    {
        GameObject previousDialogue = GameObject.Find("DialogueCanvas(Clone)");
        if(previousDialogue)
        {
            Destroy(previousDialogue);
        }
        GameObject go = Instantiate(Blueprints.instance.dialoguePrefab);
        go.GetComponentInChildren<DialogueSystem>().QueueDialogue(newDialogue);
    }

    public void QueueDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
        currentEntry = 0;
        DisplayNextEntry();
    }
    private void DisplayNextEntry()
    {
        if (currentEntry >= dialogue.dialogueEntries.Length)
        {
            Destroy(transform.parent.gameObject);

            if (dialogue.navigateToScene != "")
                SceneManager.LoadScene(dialogue.navigateToScene);

            return;
        }

        Dialogue.Entry entry = dialogue.dialogueEntries[currentEntry];
        characterImage.sprite = entry.characterImage;

        if(entry.narrator)
        {
            dialoguePanel.color = narratorColor;
        }
        else
        {
            dialoguePanel.color = normalColor;
        }

        StartCoroutine(TypeText(entry.text));
    }
    private void DisplayPreviousEntry()
    {
        if (currentEntry <= 1 || isTyping)
            return;

        StopAllCoroutines();

        currentEntry -= 2;
        

        Dialogue.Entry entry = dialogue.dialogueEntries[currentEntry];
        characterImage.sprite = entry.characterImage;

        StartCoroutine(TypeText(entry.text));
    }

    private IEnumerator TypeText(string text)
    {
        int letter = 0;
        currentText = "";
        isTyping = true;
        cancelTyping = false;

        while (isTyping && (letter < text.Length - 1))
        {
            if(cancelTyping)
            {
                dialogueText.text = text;
                break;
            }    

            currentText += text[letter];
            dialogueText.text = currentText;
            letter += 1;
            yield return new WaitForSeconds(0.02f);
        }

        dialogueText.text = text;
        isTyping = false;
        cancelTyping = false;
        currentEntry++;
    }
}