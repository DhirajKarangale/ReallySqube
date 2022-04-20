using UnityEngine;

public class StopTimeObj : MonoBehaviour
{
    public float TimeBeforeAffected; //The time after the object spawns until it will be affected by the timestop(for projectiles etc)
    private Rigidbody2D rigidBody;
    private Vector3 recordedVelocity;
    private float recordedMagnitude;

    private float timeBeforeAffectedTimer;
    private bool isAffected;
    private bool isStopped;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        timeBeforeAffectedTimer = TimeBeforeAffected;
    }

    private void Update()
    {
        timeBeforeAffectedTimer -= Time.deltaTime; // minus 1 per second
        if (timeBeforeAffectedTimer <= 0f)
        {
            isAffected = true; // Will be affected by timestop
        }

        if (isAffected && TimeStone.instance.isTimeStoped && !isStopped)
        {
            if (rigidBody.velocity.magnitude >= 0f) //If Object is moving
            {
                recordedVelocity = rigidBody.velocity.normalized; //records direction of movement
                recordedMagnitude = rigidBody.velocity.magnitude; // records magitude of movement

                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                rigidBody.velocity = Vector3.zero; //makes the rigidbody stop moving
                rigidBody.isKinematic = true; //not affected by forces
                isStopped = true; // prevents this from looping
            }
        }

    }
    public void ContinueTime()
    {
        rigidBody.constraints = RigidbodyConstraints2D.None;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidBody.isKinematic = false;
        isStopped = false;
        rigidBody.velocity = recordedVelocity * recordedMagnitude; //Adds back the recorded velocity when time continues
    }
}
