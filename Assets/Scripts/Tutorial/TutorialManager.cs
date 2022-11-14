using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Dialogue beginning;
    public Dialogue preAvoid;
    public Dialogue preAttack;
    public Dialogue preBlock;
    public Dialogue end;

    public DialogueManager dialogueManager;
    public TutoPlayerActions tutoPlayer;
    public TutoEnemyAttack tutoEnemy;

    public PlayerAttackOne playerAttack;
    public PlayerBlock playerBlock;
    public PlayerDodge playerdodge;
    public EnemyAttack enemy;

    bool isEnded;


    private void Start()
    {
        dialogueManager.StartDilaogue(beginning);
    }

    public void StartPreAvoid()
    {
        dialogueManager.StartDilaogue(preAvoid);
        StartCoroutine(tutoEnemy.PrepareAutoAttack());
    }

    public IEnumerator HasAvoid()
    {
        tutoEnemy.AutoAttack();
        yield return new WaitForSeconds(2);
        tutoPlayer.preAvoid = false;
        tutoPlayer.preAttack = true;
        StartPreAttack();
    }

    public void StartPreAttack()
    {
        dialogueManager.StartDilaogue(preAttack);
    }

    public IEnumerator HasAttack()
    {
        yield return new WaitForSeconds(2);
        tutoPlayer.preAttack = false;
        tutoPlayer.preBlock = true;
        StartPreBlock();
    }

    public void StartPreBlock()
    {
        dialogueManager.StartDilaogue(preBlock);
        StartCoroutine(tutoEnemy.PrepareLongAttack());
    }

    public IEnumerator HasBlock()
    {
        tutoEnemy.LongAttack();
        yield return new WaitForSeconds(5);
        tutoPlayer.preBlock = false;
        isEnded = true;
        dialogueManager.StartDilaogue(end);
    }

    public void StartEnd()
    {
        playerAttack.enabled = true;
        playerBlock.enabled = true;
        playerdodge.enabled = true;
        enemy.enabled = true;
        tutoPlayer.enabled = false;
        tutoEnemy.enabled = false;
        isEnded = true;
    }

    public void NextPhase()
    {
        if (!isEnded)
        {
            tutoPlayer.preAvoid = true;
            StartPreAvoid();
        }
        else
        {
            StartEnd();
        }
    }
}
