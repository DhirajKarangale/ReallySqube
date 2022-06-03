using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] EneWeapon eneWeapon;
    [SerializeField] Rigidbody2D rbNPC;
    [SerializeField] Transform healthBar;
    [SerializeField] GameObject spikes;
    [SerializeField] float startAttackDist;
    [SerializeField] float attackTime;
    [SerializeField] float rushForce;
    [SerializeField] float spikeForce;
    private bool isAttackStarted;
    private float spikeDir;
    private float originalDamage;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
    }

    private void Update()
    {
        LookDir();
        SetHealthBar();
        SetDamage();

        if (!isAttackStarted && playerHealth && startAttackDist >= Vector2.Distance(transform.position, playerHealth.transform.position))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttackStarted = true;
        enemyHealth.isDamageAllow = true;

        if (Random.value > 0.4f) RushAttack();
        else SpikesAttack();

        yield return new WaitForSeconds(attackTime);

        StartCoroutine(Attack());
    }

    private void SetHealthBar()
    {
        healthBar.localScale = new Vector2(enemyHealth.health / enemyHealth.mxHealth, 1);
    }

    private void LookDir()
    {
        if (playerHealth.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            spikeDir = -90;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            spikeDir = 90;
        }
    }

    private void RushAttack()
    {
        enemyHealth.isDamageAllow = false;
        Vector2 playerDir = playerHealth.transform.position - transform.position;
        playerDir = playerDir.normalized;
        rbNPC.AddForce(playerDir * rushForce, ForceMode2D.Impulse);
        Invoke("AllowDamage", 1);
    }

    private void SpikesAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject currRock = Instantiate(spikes, SpikesPos(), Quaternion.Euler(0, 0, spikeDir));
            Vector2 playerDir = playerHealth.transform.position - transform.position;
            playerDir = playerDir.normalized;
            currRock.GetComponent<Rigidbody2D>().AddForce(playerDir * spikeForce, ForceMode2D.Impulse);
            Destroy(currRock, 5);
        }
    }

     private void SetDamage()
    {
        eneWeapon.damage = enemyHealth.isDamageAllow ? originalDamage / 2 : originalDamage;
    }

    private Vector3 SpikesPos()
    {
        return new Vector3(transform.position.x - Mathf.Sign(spikeDir) * 5,
        Random.Range(transform.position.y, transform.position.y + 7), 0);
    }

    private void AllowDamage()
    {
        enemyHealth.isDamageAllow = true;
    }
}
