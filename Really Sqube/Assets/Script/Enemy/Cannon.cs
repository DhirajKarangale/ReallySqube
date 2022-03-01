using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float rate;
    [SerializeField] float bulletForce;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform attackPoint;
    [SerializeField] Animator animator;

    private void Start()
    {
        InvokeRepeating("Shoot", 0, rate);
    }

    private void Shoot()
    {
        animator.Play("Shoot");
    }

    public void ThroughBullet()
    {
        GameObject currBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        currBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        Invoke("PlayIdel", 0.5f);
    }

    private void PlayIdel()
    {
        animator.Play("Idel");
    }
}
