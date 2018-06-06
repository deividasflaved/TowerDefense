using UnityEngine;
using TMPro;

public class SpotUI : MonoBehaviour {

    private Spot target;
    public GameObject UI;

    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI sellCostText;


    public void SetTarget(Spot _target)
    {
        target = _target;
        Turret _turret = target.turret.GetComponent<Turret>();

        transform.position = target.GetBuildPosition();
        if (_turret.lvl >= target.turretBlueprint.maxLvl)
            upgradeCostText.text = "UPGRADE \n" + "MAX";
        else
            upgradeCostText.text = "UPGRADE \n" + target.turretBlueprint.upgradeCost* _turret.lvl + "C";


        sellCostText.text = "SELL \n" + (target.turretBlueprint.GetSellCost() + (_turret.lvl * target.turretBlueprint.upgradeCost / 2)) + "C";

        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
