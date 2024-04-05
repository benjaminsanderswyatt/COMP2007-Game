using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog")]
    static public bool inDialog = false;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogTxt;

    public float typingSpeed;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(NpcDialog dialog)
    {
        DialogManager.inDialog = true;

        animator.SetBool("IsOpen", true);

        nameTxt.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogTxt.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogTxt.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }


    void EndDialog()
    {
        animator.SetBool("IsOpen", false);
        DialogManager.inDialog = false;
    }


}
