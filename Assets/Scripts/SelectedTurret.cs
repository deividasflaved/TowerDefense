using UnityEngine.UI;
using UnityEngine;

public class SelectedTurret : MonoBehaviour {

    BuildManager buildManager;
    public Image image;
    public Sprite imgSprite;

    void Start()
    {
        buildManager = BuildManager.instance;
        imgSprite = image.sprite;
    }
    private void Update()
    {
        if (buildManager.GetTurretToBuild() != null)
            image.sprite = buildManager.GetTurretToBuild().prefab.GetComponent<SpriteRenderer>().sprite;
        else
            image.sprite = imgSprite;
    }
}
