using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    public float dialogStart;
    public float timeBetweenDialog;
    public GameObject dialogBox;

    public TimelineController timelineController;

    public Dialog[] dialog;

    public DialogManager dialogManager;

    public enum State { INTRO, MARS, PROBLEM, OUTRO };
    private State state = State.INTRO;

    int animShipIndex = 0;
    int dialogIndex = 0;

    private bool isDialogBoxEnabled = false;
    
    void Start()
    {
        StartCoroutine(StartDialog(2f));
    }

    void Update()
    {
        if(isDialogBoxEnabled && !dialogManager.IsOpen)
        {
            if(state == State.INTRO)
            {
                PLayTimeline();
                Debug.Log("dialog ends");
                isDialogBoxEnabled = false;
                StartCoroutine(StartDialog(5f));
                state = State.MARS;
            }
           else if (state == State.MARS)
            {
                PLayTimeline();
                isDialogBoxEnabled = false;
                StartCoroutine(StartDialog(3f));
                state = State.PROBLEM;
                Debug.Log("state = mars dialog index" + dialogIndex);
            }
            else if (state == State.PROBLEM)
            {
                PLayTimeline();
                isDialogBoxEnabled = false;
                StartCoroutine(PlayTimeline(7.5f));
                Debug.Log("state = Problem dialog index " + dialogIndex);
                StartCoroutine(StartDialog(12f));
                state = State.OUTRO;
            }
            else if (state == State.OUTRO)
            {
                isDialogBoxEnabled = false;
                PLayTimeline();
                StartCoroutine(StartGame(4f));
                Debug.Log("state = Outro dialog index " + dialogIndex);
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(StartGame(1f));
        }
    }

    IEnumerator StartDialog(float i)
    {
        yield return new WaitForSeconds(i);
        dialogBox.SetActive(true);
        //dialogTrigger.TriggerDialog();
        dialogManager.StartDialog(dialog[dialogIndex]);
        dialogIndex++;
        PLayTimeline();
        isDialogBoxEnabled = true;
    }

    void PLayTimeline()
    {
        timelineController.PlayFromTimelines(animShipIndex);
        Debug.Log("Playing Timeline " + animShipIndex + " at the " + state + " State");
        animShipIndex++;
    }

    IEnumerator PlayTimeline(float i)
    {
        yield return new WaitForSeconds(i);
        timelineController.PlayFromTimelines(animShipIndex);
        Debug.Log("Playing Timeline " + animShipIndex + " at the " + state + " State");
        animShipIndex++;
    }

    IEnumerator StartGame(float i)
    {
        yield return new WaitForSeconds(i);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
