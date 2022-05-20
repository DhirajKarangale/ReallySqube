using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    public SpriteRenderer bg;
    [SerializeField] PlayerMove player;
    [SerializeField] AudioSource reverseSound;
    [HideInInspector] public bool isRewinding = false;
    private float recordTime = 5f;
    private List<Vector2> recordedPos;

    private UIManager uiManager;

    private void Start()
    {
        recordedPos = new List<Vector2>();
        uiManager = UIManager.instance;
    }

    private void Update()
    {
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
        player.rigidBody.isKinematic = true;
        player.ChangeAnim("Move");
        UIManager.instance.objButtons.SetActive(false);
        reverseSound.Play();
    }

    public void StopRewind()
    {
        uiManager.txtStoneUseStatus.gameObject.SetActive(false);

        isRewinding = false;
        player.rigidBody.isKinematic = false;
        bg.color = Color.white;
        UIManager.instance.objButtons.SetActive(true);
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
        if (!GameManager.instance.isGameOver && !UIManager.instance.isPause && (player.rigidBody.velocity.magnitude > 0))
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
