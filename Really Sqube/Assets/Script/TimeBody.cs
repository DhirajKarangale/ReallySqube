using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    private bool isRewinding = false;
    public float recordTime = 5f;
    [SerializeField] Rigidbody2D rigidBody;
    List<PointInTime> pointsInTime;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rigidBody.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rigidBody.isKinematic = false;
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }
}
