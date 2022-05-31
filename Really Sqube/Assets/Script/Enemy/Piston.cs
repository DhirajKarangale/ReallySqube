using UnityEngine;
using System.Collections;

public class Piston : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbPiston;
    [SerializeField] ParticleSystem psSmash;
    [SerializeField] AudioSource audioSource;
    private PlayerHealth playerHealth;
    private Rigidbody2D rbPlayer;
    [SerializeField] float playerPushForce;
    [SerializeField] float downForce;
    [SerializeField] float upForce;
    [SerializeField] float upStopTime = 5f;
    [SerializeField] float downStopTime = 1.5f;
    [SerializeField] float upGoingTime = 1.5f;
    [SerializeField] float playerDist = 25f;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
        rbPlayer = playerHealth.playerMove.rigidBody;
        StartCoroutine(StartPiston());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth && playerDist >= Vector2.Distance(transform.position, playerHealth.transform.position))
            {
                psSmash.Play();
                audioSource.Play();
                CamShake.instance.Shake(9, 0.3f);
            }
        }
        else
        {
            rbPlayer.AddForce(Mathf.Sign(playerHealth.transform.position.x - transform.position.x) * Vector2.right * playerPushForce);
        }
    }

    private IEnumerator StartPiston()
    {
        // Down
        rbPiston.isKinematic = false;
        rbPiston.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        rbPiston.AddForce(Vector2.down * downForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(downStopTime);

        // Up
        rbPiston.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
        rbPiston.isKinematic = true;
        yield return new WaitForSeconds(upGoingTime);
        rbPiston.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(upStopTime);

        StartCoroutine(StartPiston());
    }
}
