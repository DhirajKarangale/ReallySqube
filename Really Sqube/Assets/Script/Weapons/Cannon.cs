using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float rate;
    [SerializeField] float bulletForce;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;
    private Vector2 attackPoint;

    private void Start()
    {
        attackPoint = (Vector2)transform.position - new Vector2(0, 0.21f);
        InvokeRepeating("Shoot", 0, rate);
    }

    private void Shoot()
    {
        animator.Play("Shoot");
    }

    public void ThroughBullet()
    {
        GameObject currBullet = Instantiate(bullet, attackPoint, Quaternion.identity);
        currBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        Invoke("PlayIdel", 0.5f);
    }

    private void PlayIdel()
    {
        animator.Play("Idel");
    }
}
