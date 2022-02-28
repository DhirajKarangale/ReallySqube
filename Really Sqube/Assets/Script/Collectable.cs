using UnityEngine;

public class Collectable : MonoBehaviour
{
    private enum Item {RealityStone,HealthPack};
    [SerializeField] Item itemCollect;
    [SerializeField] GameObject ps;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch(itemCollect)
            {
                case Item.RealityStone:
                RealityStone.instance.ChangeStone(1);
                break;
                case Item.HealthPack:
                PlayerHealth.instance.ChangeHealth(-50);
                break;
            }
            Instantiate(ps, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
