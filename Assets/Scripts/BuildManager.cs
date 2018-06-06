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
    private Spot selectedNode;
    public SpotUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool EnoughGold { get { return PlayerStats.Gold >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        DeselectNode();
    }

    public void SelectNode(Spot node)
    {
        if (selectedNode == node) 
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
