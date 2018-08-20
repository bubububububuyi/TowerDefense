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


    // Use this for initialization
    void Start()
    {
        _Rend = GetComponent<Renderer>();
        _StartColor = _Rend.material.color;
        _NotEnoughMoneyColor = Color.red;
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

        BuildManager.Instance.BuildTurretOn(this);
//        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
//        _Turret = (GameObject)Instantiate(turretToBuild, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
    }

    public Vector3 GetBuildPosition()
    {
        Vector3 pos = transform.position + new Vector3(0f, 0.5f, 0f);
        return pos;
    }
}
