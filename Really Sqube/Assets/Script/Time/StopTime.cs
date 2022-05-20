using UnityEngine;
using System.Collections;

public class StopTime : MonoBehaviour
{
    private Cannon[] cannonList;
    private Dialogue[] dialogueList;
    private MoveEnemy[] moveEnemyList;
    private MovePlatform[] movePlatformList;
    private DistSound[] distSoundList;
    private Animator[] animatorList;

    private Rigidbody2D rigidBody;

    private void Stop()
    {
        UIManager.instance.txtStoneUseStatus.text = "Time Pause";
        UIManager.instance.txtStoneUseStatus.color = Color.white;
        UIManager.instance.txtStoneUseStatus.gameObject.SetActive(true);
        PlayerHealth.instance.reverse.bg.color = Color.blue;

        moveEnemyList = FindObjectsOfType<MoveEnemy>();
        dialogueList = FindObjectsOfType<Dialogue>();
        movePlatformList = FindObjectsOfType<MovePlatform>();
        cannonList = FindObjectsOfType<Cannon>();
        distSoundList = FindObjectsOfType<DistSound>();
        animatorList = FindObjectsOfType<Animator>();

        foreach (MoveEnemy moveEnemy in moveEnemyList)
        {
            moveEnemy.enabled = false;
            rigidBody = moveEnemy.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            dialogue.enabled = false;
            dialogue.GetComponent<BoxCollider2D>().enabled = false;
        }

        foreach (MovePlatform movePlatform in movePlatformList)
        {
            movePlatform.enabled = false;
            rigidBody = movePlatform.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (Cannon cannon in cannonList)
        {
            cannon.enabled = false;
            rigidBody = cannon.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (DistSound distSound in distSoundList)
        {
            distSound.enabled = false;
            rigidBody = distSound.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.CompareTag("Player")) continue;
            animator.enabled = false;
            rigidBody = animator.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void Continue()
    {
        UIManager.instance.txtStoneUseStatus.gameObject.SetActive(false);
        PlayerHealth.instance.reverse.bg.color = Color.white;


        foreach (MoveEnemy moveEnemy in moveEnemyList)
        {
            moveEnemy.enabled = true;
            rigidBody = moveEnemy.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            dialogue.enabled = true;
            dialogue.GetComponent<BoxCollider2D>().enabled = true;
        }

        foreach (MovePlatform movePlatform in movePlatformList)
        {
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
            distSound.enabled = true;
            rigidBody = distSound.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        foreach (Animator animator in animatorList)
        {
            if (animator.gameObject.CompareTag("Player")) continue;
            animator.enabled = true;
            rigidBody = animator.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
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