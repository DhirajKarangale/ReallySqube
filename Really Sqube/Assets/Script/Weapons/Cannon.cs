using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float rate;
    [SerializeField] float bulletForce;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;
    private Vector2 attackPoint;
    private bool isShootingAllow;

    private void Update()
    {
        if (PlayerHealth.instance && Mathf.Abs(transform.position.x - PlayerHealth.instance.transform.position.x) < 30)
        {
            if (!isShootingAllow)
            {
                InvokeRepeating("Shoot", 0, rate);
                isShootingAllow = true;
            }
        }
        else
        {
            CancelInvoke("Shoot");
            isShootingAllow = false;
        }
    }

    private void Shoot()
    {
        if (GameManager.instance.isGameOver) return;
        
        animator.Play("Shoot");
    }

    public void ThroughBullet()
    {
        if (GameManager.instance.isGameOver) return;

        attackPoint = (Vector2)transform.position - new Vector2(0, 1.5f);
        GameObject currBullet = Instantiate(bullet, attackPoint, Quaternion.identity);
        currBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        Invoke("PlayIdel", 0.5f);
    }

    private void PlayIdel()
    {
        animator.Play("Idel");
    }
}
