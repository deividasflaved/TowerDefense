using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint canonTurret;
    public TurretBlueprint standardDesertTurret;
    public TurretBlueprint rocketTurret;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectCanonTurret()
    {
        buildManager.SelectTurretToBuild(canonTurret);
    }
    public void SelectStandardDesertTurret()
    {
        buildManager.SelectTurretToBuild(standardDesertTurret);
    }
    public void SelectRocketTurret()
    {
        buildManager.SelectTurretToBuild(rocketTurret);
    }
}
