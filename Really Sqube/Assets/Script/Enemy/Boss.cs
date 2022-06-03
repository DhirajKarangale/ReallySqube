using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] EneWeapon eneWeapon;
    [SerializeField] Transform healthBar;
    [SerializeField] Rigidbody2D rbNPC;

    [Header("Enemy")]
    [SerializeField] GameObject spikes;
    [SerializeField] GameObject cutter;
    [SerializeField] GameObject spikeCube;

    [Header("Items")]
    [SerializeField] GameObject coin;
    [SerializeField] GameObject timeStone;
    [SerializeField] GameObject healthPack;

    [Header("Attributes")]
    [SerializeField] float startAttackDist;
    [SerializeField] float attackTime;
    [SerializeField] float rushForce;
    [SerializeField] float throwForce;

    private bool isAttackStarted;
    private float spwanDir;
    private float originalDamage;
    private int objSpwanAmount;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
        objSpwanAmount = 1;
        originalDamage = eneWeapon.damage;
        // int x;
    }

    private void Update()
    {
        LookDir();
        SetDamage();
        SetHealthBar();
        SetObjSpwanAmount();

        if (!isAttackStarted && playerHealth && startAttackDist >= Vector2.Distance(transform.position, playerHealth.transform.position))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttackStarted = true;
        enemyHealth.isDamageAllow = true;

        if (Random.Range(1, 3) == 1) RushAttack();
        else SpwanAttack();

        yield return new WaitForSeconds(attackTime);

        StartCoroutine(Attack());
    }

    private void SpwanAttack()
    {
        int val = Random.Range(1, 4);

        if (val == 1) CutterAttack();
        else if (val == 2) SpikeCubeAttack();
        else SpikeCubeAttack();
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
        SpwanObjects(spikes);
    }

    private void CutterAttack()
    {
        SpwanObjects(cutter);
    }

    private void SpikeCubeAttack()
    {
        float currSpwanDir = spwanDir;
        spwanDir = 0;
        SpwanObjects(spikeCube);
        spwanDir = currSpwanDir;
    }

    private void SpwanObjects(GameObject obj)
    {
        for (int i = 0; i < objSpwanAmount; i++)
        {
            GameObject currObj = Instantiate(obj, SpwanPos(), Quaternion.Euler(0, 0, spwanDir));
            Vector2 playerDir = playerHealth.transform.position - transform.position;
            playerDir = playerDir.normalized;
            currObj.GetComponent<Rigidbody2D>().AddForce(playerDir * throwForce, ForceMode2D.Impulse);
            SetSpwanItem(currObj);
            Destroy(currObj, 10);
        }
    }

    private void SetDamage()
    {
        eneWeapon.damage = enemyHealth.isDamageAllow ? originalDamage / 2 : originalDamage;
    }

    private void SetSpwanItem(GameObject currObj)
    {
        if (Random.value > 0.45f)
        {
            ItemSpawner itemSpawner = currObj.GetComponent<ItemSpawner>();

            float val = Random.value;
            if (val <= 0.5f)
            {
                itemSpawner.amount = 1;
                itemSpawner.item = healthPack;
            }
            else if (val < 0.75f)
            {
                itemSpawner.amount = 1;
                itemSpawner.item = timeStone;
            }
            else
            {
                itemSpawner.amount = 5;
                itemSpawner.item = coin;
            }
        }
    }

    private void SetObjSpwanAmount()
    {
        if (enemyHealth.health < 300) objSpwanAmount = 4;
        else if (enemyHealth.health < 700) objSpwanAmount = 3;
        else if (enemyHealth.health < 1400) objSpwanAmount = 2;
        else objSpwanAmount = 1;
    }

    private void SetHealthBar()
    {
        healthBar.localScale = new Vector2(enemyHealth.health / enemyHealth.mxHealth, 1);
    }

    private void LookDir()
    {
        if (playerHealth.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-4f, 4f, 4f);
            spwanDir = -90;
        }
        else
        {
            transform.localScale = new Vector3(4f, 4f, 4f);
            spwanDir = 90;
        }
    }

    private Vector3 SpwanPos()
    {
        return new Vector3(transform.position.x - Mathf.Sign(spwanDir) * 5,
        Random.Range(transform.position.y, transform.position.y + 7), 0);
    }

    private void AllowDamage()
    {
        enemyHealth.isDamageAllow = true;
    }
}
