using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public GameObject button;

    public Animator animator;

    public bool IsOpen = false;

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        button.SetActive(false);
    }

    public void StartDialog (Dialog dialog)
    {
        animator.SetBool("IsOpen", true);
        //Debug.Log("Starting the conversation with " + dialog.name);
        IsOpen = true;
        nameText.text = dialog.name;

        sentences.Clear();

        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) 
    {
        button.SetActive(false);
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
        button.SetActive(true);
    }

    void EndDialog()
    {
        IsOpen = false;
        animator.SetBool("IsOpen", false);
        Debug.Log("END of the conversation");
    }
}
