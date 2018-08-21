using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    //    public GameObject StandardTurretPrefab;
    //    public GameObject MissileLauncherPrefab;

    private TurretBlueprint _TurretToBuild;
    private Node _SelectNode;

    /// <summary>
    /// 是否可以创建
    /// </summary>
    /// <value><c>true</c> if this instance can build; otherwise, <c>false</c>.</value>
    public bool CanBuild{ get { return _TurretToBuild != null; } }

    public bool HasMoney{ get { return ClientPlayer.Money >= _TurretToBuild.Const; } }

    public GameObject BuildEffect;
    public NodeUI NodeUI;


    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CancelSelectTurretToBuild();
        }
    }

    public void CancelSelectTurretToBuild()
    {
        _TurretToBuild = null;
        _SelectNode = null;
        NodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        _TurretToBuild = turret;
        _SelectNode = null;
        NodeUI.Hide();
    }

//    public void BuildTurretOn(Node node)
//    {
//        GameObject turret = (GameObject)Instantiate(_TurretToBuild.Prefab, node.GetBuildPosition(), Quaternion.identity);
//        node.Turret = turret;
//        ClientPlayer.Money -= _TurretToBuild.Const;
//
//        GameObject effect = (GameObject)Instantiate(BuildEffect, node.GetBuildPosition(), Quaternion.identity);
//        Destroy(effect, 3f);
//    }

    public void SelectNode(Node node)
    {
        if (_SelectNode == node)
        {
            DeselectNode();
            return;
        }
        _SelectNode = node;
        _TurretToBuild = null;

        NodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        CancelSelectTurretToBuild();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return _TurretToBuild;
    }

}
