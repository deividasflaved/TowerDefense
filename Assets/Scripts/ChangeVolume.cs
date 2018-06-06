using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider slider;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
	public void setVolume(float newValue)
    {
        float newVol = AudioListener.volume;
        newVol = newValue;
        AudioListener.volume = newVol;
        PlayerPrefs.SetFloat("Volume", newVol);
    }
}
