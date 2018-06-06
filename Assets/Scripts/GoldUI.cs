using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldUI : MonoBehaviour {

    public TextMeshProUGUI goldText;

    void Update()
    {
        goldText.text = PlayerStats.Gold.ToString();
    }
}
