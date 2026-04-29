using UnityEngine;

public class Coin : MonoBehaviour
{
    public int point = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<CoinComponent>().AddPoints(point);
        Destroy(gameObject);
    }
}
