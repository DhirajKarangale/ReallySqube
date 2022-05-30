using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbPiston;
    [SerializeField] ParticleSystem psSmash;
    [SerializeField] float downForce;
    [SerializeField] float upForce;
    [SerializeField] float stopTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rbPiston.isKinematic = false;
            rbPiston.constraints = RigidbodyConstraints2D.None;
            rbPiston.constraints = RigidbodyConstraints2D.FreezeRotation;
            rbPiston.constraints = RigidbodyConstraints2D.FreezePositionX;
            rbPiston.AddForce(Vector2.down * downForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rbPiston.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
            rbPiston.isKinematic = true;
            Invoke("Freez", stopTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        psSmash.Play();
    }

    private void Freez()
    {
        rbPiston.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
