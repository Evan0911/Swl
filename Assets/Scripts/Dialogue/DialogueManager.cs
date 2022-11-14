using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Une queue marche de mani�re semblable � une liste mais permet de r�cup�rer uniquement le premier de la liste pour l'enlever, d'o� le nom de Queue
    private Queue<string> sentences;
    public TutorialManager tuto;
    public Animator animator;

    public Text dialogueText;

    private void Start()
    {
        //Initialisation de la Queue
        sentences = new Queue<string>();
    }

    public void StartDilaogue(Dialogue dialogue)
    {
        //On d�marre l'animation d'ouverture de dialogue, on clear la Queue avant d'y ajouter toutes les phrases du dialogue dedans et on appelle la fonctiobn pour lire la premi�re phrase
        animator.SetBool("dialogue", true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //Si la Queue est vide on fini le dialogue, sinon on appelle la couroutine qui va taper le texte avec la premi�re phrase de la Queue en param�tre
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        //On divise la phrase en charact�re et on les ajoute un par un � chaque frame
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        //On appelle l'animation pour clore le dialogue et pour le tuto c'est archa�que
        animator.SetBool("dialogue", false);
        if (tuto != null)
        {
            if (tuto.tutoPlayer.preAvoid == false && tuto.tutoPlayer.preAttack == false && tuto.tutoPlayer.preBlock == false)
            {
                tuto.NextPhase();
            }
        }
    }
}
