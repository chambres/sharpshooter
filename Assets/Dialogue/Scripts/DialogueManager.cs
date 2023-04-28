using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textBox;
    public AudioClip typingClip;
    public AudioSourceGroup audioSourceGroup;

    public Button playDialogue1Button;
    public Button playDialogue2Button;
    public Button playDialogue3Button;

    [TextArea]
    public string mashingDialogue;
    [TextArea]
    public string reviveDialogue;
    [TextArea]
    public string dialogue2;
    [TextArea]
    public string dialogue3;

    private DialogueVertexAnimator dialogueVertexAnimator;
    void Awake() {
        dialogueVertexAnimator = new DialogueVertexAnimator(textBox, audioSourceGroup);
        playDialogue1Button.onClick.AddListener(delegate { PlayDialogue2(); });
        playDialogue2Button.onClick.AddListener(delegate { PlayDialogue2(); });
        playDialogue3Button.onClick.AddListener(delegate { PlayDialogue3(); });
    }



    public void PlayDialogue2() {
        PlayDialogue(dialogue2);
    }

    public void PlayDialogue3() {
        PlayDialogue(dialogue3);
    }

    public void PlayBeginMashingDialogue()
    {
        PlayDialogue(mashingDialogue);
    }

    public void PlayReviveDialogue()
    {
        PlayDialogue(reviveDialogue);
    }


    private Coroutine typeRoutine = null;
    public void PlayDialogue(string message) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        dialogueVertexAnimator.textAnimating = false;
        List<DialogueCommand> commands = DialogueUtility.ProcessInputString(message, out string totalTextMessage);
        typeRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, typingClip, null));
    }
}
