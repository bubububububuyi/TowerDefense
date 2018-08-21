using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color HoverColor;
    private Renderer _Rend;
    private Color _StartColor;
    private Color _NotEnoughMoneyColor;

    public GameObject Turret;
    public TurretBlueprint TurretBlueprint;
    public bool IsUpgrade;


    // Use this for initialization
    void Start()
    {
        _Rend = GetComponent<Renderer>();
        _StartColor = _Rend.material.color;
        _NotEnoughMoneyColor = Color.red;
        IsUpgrade = false;
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (!BuildManager.Instance.CanBuild)
        {
            return;
        }

        if (BuildManager.Instance.HasMoney)
        {
            _Rend.material.color = HoverColor;
        }
        else
        {
            _Rend.material.color = _NotEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        _Rend.material.color = _StartColor;
    }

    private void OnMouseDown()
    {
        //防止穿透UI点击
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Turret != null)
        {
            BuildManager.Instance.SelectNode(this);
//            Debug.Log("can't build there!");
            return;
        }
        
        if (!BuildManager.Instance.CanBuild || !BuildManager.Instance.HasMoney)
        {
            return;
        }

//        BuildManager.Instance.BuildTurretOn(this);
//        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
//        _Turret = (GameObject)Instantiate(turretToBuild, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);

        BuildTurret(BuildManager.Instance.GetTurretToBuild());

    }

    public Vector3 GetBuildPosition()
    {
        Vector3 pos = transform.position + new Vector3(0f, 0.5f, 0f);
        return pos;
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        ClientPlayer.Money -= blueprint.Const;
        TurretBlueprint = blueprint;

        GameObject turret = (GameObject)Instantiate(blueprint.Prefab, GetBuildPosition(), Quaternion.identity);
        Turret = turret;

        GameObject effect = (GameObject)Instantiate(BuildManager.Instance.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    /// <summary>
    /// 升级
    /// </summary>
    public void UpgradeTurret()
    {
        if (IsUpgrade)
        {
            return;
        }
        if (ClientPlayer.Money < TurretBlueprint.UpgradeConst)
        {
            return;
        }
        if (TurretBlueprint.UpgradeConst == 0)
            return;

        Destroy(Turret);

        ClientPlayer.Money -= TurretBlueprint.UpgradeConst;

        GameObject turret = (GameObject)Instantiate(TurretBlueprint.UpgradePrefab, GetBuildPosition(), Quaternion.identity);
        Turret = turret;

        GameObject effect = (GameObject)Instantiate(BuildManager.Instance.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        IsUpgrade = true;

        Debug.Log("upgrade is succ.");
    }

    /// <summary>
    /// 卖出
    /// </summary>
    public void SellTurret()
    {
        Destroy(Turret);
        ClientPlayer.Money += TurretBlueprint.SellCost;

        GameObject effect = (GameObject)Instantiate(BuildManager.Instance.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        TurretBlueprint = null;
        Debug.Log("sell is succ.");
    }

}
