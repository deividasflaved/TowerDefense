using UnityEngine;

public class Spot : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color startColor;

    private Renderer rend;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    BuildManager buildManager;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Gold < blueprint.cost)
        {
            Debug.Log("Not enough gold");
            return;
        }

        PlayerStats.Gold -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        Debug.Log("Turret built! Gold left: " + PlayerStats.Gold);
    }

    public void UpgradeTurret()
    {
        Turret _turret = turret.GetComponent<Turret>();

        if (PlayerStats.Gold < turretBlueprint.upgradeCost * _turret.lvl) 
        {
            Debug.Log("Not enough gold to upgrade");
            return;
        }

        if (_turret.lvl >= turretBlueprint.maxLvl)
            return;

        PlayerStats.Gold -= turretBlueprint.upgradeCost * _turret.lvl;

        _turret.range += turretBlueprint.upgradeModifier;
        _turret.fireRate += turretBlueprint.upgradeModifier;
        _turret.lvl++;
        _turret.lvlText.text = "LvL " + _turret.lvl.ToString();

        isUpgraded = true;

        Debug.Log("Turret upgraded! Gold left: " + PlayerStats.Gold);
    }

    public void SellTurret()
    {
        Turret _turret = turret.GetComponent<Turret>();
        PlayerStats.Gold += turretBlueprint.GetSellCost() + (_turret.lvl * turretBlueprint.upgradeCost / 2);
        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
            return;
        if (buildManager.EnoughGold)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMoneyColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
