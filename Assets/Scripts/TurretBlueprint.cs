using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint {

    public GameObject prefab;
    public int cost;

    public float upgradeModifier;
    public int upgradeCost;
    public int maxLvl;

    public int GetSellCost()
    {
        return cost / 2;
    }
}
