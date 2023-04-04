using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    public string dialogueText = "";  
    public float wordDelay = 0.1f;  
    public float letterDelay = 0.025f;  
    public TMP_Text dialogueBox; 

    public GameObject goButton;

    private string[] words;  
    private Coroutine dialogueCoroutine; 

    void Start()
    {
        words = dialogueText.Split(' ');
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        dialogueCoroutine = StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        dialogueBox.text = "";

        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < words[i].Length; j++)
            {
                dialogueBox.text += words[i][j];
                yield return new WaitForSeconds(letterDelay);
            }

            if (i < words.Length - 1)
            {
                dialogueBox.text += " ";
            }

            if(i==words.Length-2)
            {
                goButtonActivate();
            }

            yield return new WaitForSeconds(wordDelay);
        }
    }

    private void goButtonActivate()
    {
        goButton.SetActive(true);
    }

    public void goToMain()
    {
        SceneManager.LoadScene(1);
    }

}
