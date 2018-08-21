using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    /// <summary>
    /// 炮台预制对象
    /// </summary>
    public GameObject Prefab;
    /// <summary>
    /// 所需要的金钱
    /// </summary>
    public int Const;

    public GameObject UpgradePrefab;
    /// <summary>
    /// 升级需要花费的金币
    /// </summary>
    public int UpgradeConst;
    /// <summary>
    /// 出售价格
    /// </summary>
    public int SellCost;
	
}
