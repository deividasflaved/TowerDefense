using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject canonTurretPrefab;
    public GameObject standardTurretDesertPrefab;
    public GameObject rocketTurretPrefab;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool EnoughGold { get { return PlayerStats.Gold >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
    public void BuildTurretOn(Spot node)
    {
        if (PlayerStats.Gold < turretToBuild.cost) 
        {
            Debug.Log("Not enough gold");
            return;
        }

        PlayerStats.Gold -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret built! Gold left: " + PlayerStats.Gold);
    }
}
