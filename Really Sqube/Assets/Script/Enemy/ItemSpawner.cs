using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float amount;
    public GameObject item;

    public void SpwanItem()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 pos = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(2, 7));
            GameObject spawnedItem = Instantiate(item, pos, Quaternion.identity);
            Rigidbody2D rigidBody = spawnedItem.GetComponent<Rigidbody2D>();
            if (rigidBody) rigidBody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
