using UnityEngine;

public class Collectable : MonoBehaviour
{
    private enum Item { RealityStone, TimeSone, Coin, HealthPack };
    [SerializeField] Item itemCollect;
    [SerializeField] ParticleSystem ps;
    private ParticleSystem.MainModule psMain;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            psMain = ps.main;
            switch (itemCollect)
            {
                case Item.RealityStone:
                    psMain.startColor = Color.red;
                    UIManager.instance.UpdateRealityStone(1);
                    break;
                case Item.TimeSone:
                    psMain.startColor = Color.green;
                    UIManager.instance.UpdateTimeStone(1);
                    break;
                case Item.Coin:
                    psMain.startColor = Color.yellow;
                    UIManager.instance.UpdateCoin(1);
                    break;
                case Item.HealthPack:
                    psMain.startColor = Color.cyan;
                    PlayerHealth.instance.ChangeHealth(-50);
                    break;
            }
            Instantiate(ps, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
