using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour {

    public TextMeshProUGUI livesText;//naudojau projektui unity asset textmeshpro

    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString();//čia priskiriam naują tekstą, tai yra gyvybęs saugomas PlayerStats
    }
}
