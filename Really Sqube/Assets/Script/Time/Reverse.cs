using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    public SpriteRenderer bg;
    [SerializeField] PlayerMove player;
    [SerializeField] AudioSource reverseSound;
    [HideInInspector] public bool isRewinding;
    private float recordTime = 5f;
    private List<Vector2> recordedPos;

    private UIManager uiManager;
    private GameManager gameManager;

    private void Start()
    {
        isRewinding = false;
        recordedPos = new List<Vector2>();
        uiManager = UIManager.instance;
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) uiManager.ResetButton();

        if (isRewinding) Rewind();
        else Record();
    }

    public void StartRewind()
    {
        uiManager.txtStoneUseStatus.text = "Reverse";
        uiManager.txtStoneUseStatus.color = Color.black;
        uiManager.txtStoneUseStatus.gameObject.SetActive(true);
        bg.color = Color.green;

        isRewinding = true;
        player.moveInputVal = 0;
        player.rigidBody.isKinematic = true;
        player.ChangeAnim("Move");
        uiManager.objButtons.SetActive(false);
        reverseSound.Play();
    }

    public void StopRewind()
    {
        uiManager.txtStoneUseStatus.gameObject.SetActive(false);

        player.moveInputVal = 0;
        uiManager.ResetButton();
        isRewinding = false;
        player.rigidBody.isKinematic = false;
        bg.color = Color.white;
        uiManager.objButtons.SetActive(true);
        reverseSound.Stop();
    }

    private void Rewind()
    {
        if (recordedPos.Count > 0)
        {
            transform.position = recordedPos[0];
            recordedPos.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (!gameManager.isGameOver && !uiManager.isPause && (player.rigidBody.velocity.magnitude > 0))
        {
            // if (recordedPos.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) if FixedUpdate
            if (recordedPos.Count > Mathf.Round(recordTime / Time.deltaTime))
            {
                recordedPos.RemoveAt(recordedPos.Count - 1);
            }
            recordedPos.Insert(0, transform.position);
        }
    }
}
