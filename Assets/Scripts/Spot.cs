using UnityEngine;

public class Spot : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color startColor;

    public string forestTag = "Forest";
    public string desertTag = "Desert";
    public string turretTag = "Turret";

    private Renderer rend;//skirta hover'inimui per bokštelių buttons
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;//bokštelio kainos ir prefab
    [HideInInspector]
    public bool isUpgraded = false;

    SoundsControler sounds;

    BuildManager buildManager;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        sounds = SoundsControler.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// mouse paspaudus ant laukelio tikrina ar tinkamas bokštelis ir jei tinkamas padeda
    /// dar tikrina ar nėra kito bokštelio
    /// </summary>
    void OnMouseDown()
    {

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        if (this.tag == desertTag && buildManager.GetTurretToBuild().prefab.tag == forestTag)
            return;
        else if (this.tag == forestTag && buildManager.GetTurretToBuild().prefab.tag == desertTag)
            return;

        BuildTurret(buildManager.GetTurretToBuild());//Padeda bokštelį
    }
    /// <summary>
    /// bokštelio padėjimo metodas
    /// </summary>
    /// <param name="blueprint"></param>
    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Gold < blueprint.cost)
        {
            sounds.NotEnoughResource.Play();
            Debug.Log("Not enough gold");
            return;
        }

        PlayerStats.Gold -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);//sukuria tower
        turret = _turret;
        turret.tag = turretTag;//prisikiria visiems turret tag "turret" kuris yra reikalingas kituose skriptuose
        sounds.TowerPlaced.Play();


        turretBlueprint = blueprint;

        Debug.Log("Turret built! Gold left: " + PlayerStats.Gold);
    }
    /// <summary>
    /// Patobulinti bokštelį metodas
    /// </summary>
    public void UpgradeTurret()
    {
        Turret _turret = turret.GetComponent<Turret>();

        if (PlayerStats.Gold < turretBlueprint.upgradeCost * _turret.lvl) //ar užtenka gold
        {
            sounds.NotEnoughResource.Play();
            Debug.Log("Not enough gold to upgrade");
            return;
        }

        if (_turret.lvl >= turretBlueprint.maxLvl)//tikrina ar dar ne max lvl
            return;

        PlayerStats.Gold -= turretBlueprint.upgradeCost * _turret.lvl;

        _turret.range += turretBlueprint.upgradeModifier;
        _turret.fireRate += turretBlueprint.upgradeModifier;
        _turret.lvl++;
        _turret.lvlText.text = "LvL " + _turret.lvl.ToString();

        sounds.TowerUpgrade.Play();


        Debug.Log("Turret upgraded! Gold left: " + PlayerStats.Gold);
    }
    /// <summary>
    /// parduoti bokštelį
    /// </summary>
    public void SellTurret()
    {
        Turret _turret = turret.GetComponent<Turret>();
        PlayerStats.Gold += turretBlueprint.GetSellCost() + (_turret.lvl * turretBlueprint.upgradeCost / 2);
        Destroy(turret);
        sounds.SellTower.Play();
        turretBlueprint = null;
    }
    /// <summary>
    /// jei užtenka mat bus paprasta spalva jei ne , bus kitokia
    /// </summary>
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
