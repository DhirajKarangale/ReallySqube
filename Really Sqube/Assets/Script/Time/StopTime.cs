using UnityEngine;

public class StopTime : MonoBehaviour
{
    private MoveEnemy[] moveEnemyList;
    private MovePlatform[] movePlatformList;
    private Dialogue[] dialoguesList;
    private Cannon[] cannonList;
    private DistSound[] distSoundList;
    private Animator[] animatorList;

    private Rigidbody2D rigidBody;
    private SpriteRenderer bg;

    private void Start()
    {
        bg = PlayerHealth.instance.reverse.bg;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("StopTime");
            Stop();
        }

        if (Input.GetKey(KeyCode.C))
        {
            Debug.Log("Continue");
            Continue();
        }
    }

    private void Stop()
    {
        moveEnemyList = FindObjectsOfType<MoveEnemy>();
        movePlatformList = FindObjectsOfType<MovePlatform>();
        dialoguesList = FindObjectsOfType<Dialogue>();
        cannonList = FindObjectsOfType<Cannon>();
        distSoundList = FindObjectsOfType<DistSound>();
        animatorList = FindObjectsOfType<Animator>();

        bg.color = Color.gray;

        foreach (MoveEnemy moveEnemy in moveEnemyList)
        {
            moveEnemy.enabled = false;
            rigidBody = moveEnemy.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
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

        foreach (Dialogue dialogue in dialoguesList)
        {
            dialogue.enabled = false;
            rigidBody = dialogue.GetComponent<Rigidbody2D>();
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
            if(animator.gameObject.CompareTag("Player")) continue;
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
        bg.color = Color.white;

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

        foreach (Dialogue dialogue in dialoguesList)
        {
            dialogue.enabled = true;
            rigidBody = dialogue.GetComponent<Rigidbody2D>();
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
            if(animator.gameObject.CompareTag("Player")) continue;
            animator.enabled = true;
            rigidBody = animator.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                rigidBody.constraints = RigidbodyConstraints2D.None;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
