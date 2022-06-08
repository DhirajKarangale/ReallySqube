using UnityEngine;
using System.Collections;

public class StopTime : MonoBehaviour
{
    private Cannon[] cannonList;
    private MoveEnemy[] moveEnemyList;
    private MovePlatform[] movePlatformList;
    private DistSound[] distSoundList;
    private Piston[] pistonList;
    private Animator[] animatorList;
    private Dialogue[] dialogueList;
    private NPC npc;
    private Boss boss;

    public bool isTimeStopped;
    private Rigidbody2D rigidBody;

    private void Stop()
    {
        isTimeStopped = true;
        UIManager.instance.txtStoneUseStatus.text = "Time Pause";
        UIManager.instance.txtStoneUseStatus.color = Color.white;
        UIManager.instance.txtStoneUseStatus.gameObject.SetActive(true);
        PlayerHealth.instance.reverse.bg.color = Color.blue;

        moveEnemyList = FindObjectsOfType<MoveEnemy>();
        movePlatformList = FindObjectsOfType<MovePlatform>();
        cannonList = FindObjectsOfType<Cannon>();
        distSoundList = FindObjectsOfType<DistSound>();
        pistonList = FindObjectsOfType<Piston>();
        animatorList = FindObjectsOfType<Animator>();
        dialogueList = FindObjectsOfType<Dialogue>();
        boss = FindObjectOfType<Boss>();
        npc = FindObjectOfType<NPC>();

        if (boss) boss.isTimeStopped = true;
        if (npc) npc.isTimeStopped = true;

        foreach (MoveEnemy moveEnemy in moveEnemyList)
        {
            if (!moveEnemy) return;
            moveEnemy.enabled = false;
            rigidBody = moveEnemy.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (MovePlatform movePlatform in movePlatformList)
        {
            if (!movePlatform) return;
            movePlatform.enabled = false;
            rigidBody = movePlatform.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (Cannon cannon in cannonList)
        {
            if (!cannon) return;
            cannon.enabled = false;
            rigidBody = cannon.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (DistSound distSound in distSoundList)
        {
            if (!distSound) return;
            distSound.enabled = false;
            rigidBody = distSound.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (Piston piston in pistonList)
        {
            if (!piston) return;
            piston.isStopPiston = true;
            piston.Up();
            piston.enabled = false;
        }

        foreach (Animator animator in animatorList)
        {
            if (!animator) return;
            if (animator.gameObject.CompareTag("Player") || animator.gameObject.layer == 5) continue;
            animator.enabled = false;
            rigidBody = animator.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            if (!dialogue) return;
            if (dialogue.playerDir == 0) continue;
            dialogue.enabled = false;
            BoxCollider2D boxCollider = dialogue.GetComponent<BoxCollider2D>();
            if (boxCollider) boxCollider.enabled = false;
        }
    }

    public void Continue()
    {
        StopAllCoroutines();
        isTimeStopped = false;
        UIManager.instance.txtStoneUseStatus.gameObject.SetActive(false);
        PlayerHealth.instance.reverse.bg.color = Color.white;

        if (boss) boss.isTimeStopped = false;
        if (npc) npc.isTimeStopped = false;

        foreach (MoveEnemy moveEnemy in moveEnemyList)
        {
            if (!moveEnemy) return;
            moveEnemy.enabled = true;
            rigidBody = moveEnemy.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (MovePlatform movePlatform in movePlatformList)
        {
            if (!movePlatform) return;
            movePlatform.enabled = true;
            rigidBody = movePlatform.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (Cannon cannon in cannonList)
        {
            if (!cannon) return;
            cannon.enabled = true;
            rigidBody = cannon.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (DistSound distSound in distSoundList)
        {
            if (!distSound) return;
            distSound.enabled = true;
            rigidBody = distSound.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (Piston piston in pistonList)
        {
            if (!piston) return;
            piston.isStopPiston = false;
            piston.enabled = true;
        }

        foreach (Animator animator in animatorList)
        {
            if (!animator) return;
            animator.enabled = true;
            rigidBody = animator.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            if (!dialogue) return;
            dialogue.enabled = true;
            BoxCollider2D boxCollider = dialogue.GetComponent<BoxCollider2D>();
            if (boxCollider) boxCollider.enabled = true;
        }
    }

    private IEnumerator IEStopTime()
    {
        Stop();
        yield return new WaitForSeconds(30);
        Continue();
    }

    public void StopTimeButton()
    {
        StartCoroutine(IEStopTime());
    }
}