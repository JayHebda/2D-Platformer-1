using TMPro;
using UnityEngine;

public class UI_CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CoinComponent coinComp;

    private void Awake()
    {
        coinComp.OnCoinChanged += CoinComp_OnCoinChanged;
        coinComp.OnCoinInit += CoinComp_OnCoinInit;
    }

    private void CoinComp_OnCoinInit(int amount, int changedAmount)
    {
        text.text = amount.ToString();
    }

    private void CoinComp_OnCoinChanged(int amount, int changedAmount)
    {
        text.text = amount.ToString();
    }
}
