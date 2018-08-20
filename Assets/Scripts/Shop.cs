using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public TurretBlueprint StandardTurret;
    public TurretBlueprint MissileLauncher;
    public TurretBlueprint LaserBeamer;

    [Header("UI")]
    public Text MoneyText;


    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = string.Format("${0}", ClientPlayer.Money);
    }

    public void SelectStandardTurret()
    {
        BuildManager.Instance.SelectTurretToBuild(StandardTurret);
    }

    public void SelectMissileLauncher()
    {
        BuildManager.Instance.SelectTurretToBuild(MissileLauncher);
    }

    public void SelectLaserBeamer()
    {
        BuildManager.Instance.SelectTurretToBuild(LaserBeamer);
    }
}
