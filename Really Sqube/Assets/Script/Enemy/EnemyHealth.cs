using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject objDestroy;
    [SerializeField] ParticleSystem psDye;
    [SerializeField] Color effectColor = Color.gray;
    [SerializeField] float mxHealth;
    private float health;
    private ParticleSystem.MainModule psMain;

    private void Start()
    {
        health = mxHealth;
        psMain = psDye.main;
    }

    private void Update()
    {
        if (this.transform.position.y <= -100) TakeDamage(mxHealth + 10);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            psMain.startColor = effectColor;
            Instantiate(psDye.gameObject, transform.position, transform.rotation);
            Destroy(objDestroy);
        }
    }
}
