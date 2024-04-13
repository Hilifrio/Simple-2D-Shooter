using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;

    public void Start()
    {
        Dialog dialogue = new Dialog();
        dialogue.name = name;
        dialogue.sentences = sentences;
        FindObjectOfType<DialogManager>().StartDialog(dialogue);
    }
}
