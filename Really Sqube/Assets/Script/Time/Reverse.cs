using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] SpriteRenderer bg;
    [SerializeField] AudioSource reverseSound;
    private float recordTime = 5f;
    [HideInInspector] public bool isRewinding = false;
    private List<Vector2> recordedPos;

    private void Start()
    {
        recordedPos = new List<Vector2>();
    }

    private void Update()
    {
        if (isRewinding) Rewind();
        else Record();
    }

    public void StartRewind()
    {
        isRewinding = true;
        player.rigidBody.isKinematic = true;
        bg.color = Color.green;
        player.ChangeAnim("Move");
        UIManager.instance.objButtons.SetActive(false);
        reverseSound.Play();
    }

    public void StopRewind()
    {
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
