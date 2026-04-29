using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    private int points;

    public delegate void OnCoinChangedHandler(int amount, int changedAmount);
    public event OnCoinChangedHandler OnCoinChanged;
    public event OnCoinChangedHandler OnCoinInit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnCoinInit?.Invoke(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int amomunt)
    {
        points += amomunt;
        OnCoinChanged?.Invoke(points, amomunt);
        //Debug.Log(points);   
    }
}
