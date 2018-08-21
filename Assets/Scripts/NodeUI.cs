using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;
    private Node _Target;

    public Text UpgradeCostText;
    public Text SellCostText;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation * Quaternion.FromToRotation(new Vector3(0, 1, 0), new Vector3(0, 1, 0));
    }

    public void SetTarget(Node target)
    {
        _Target = target;
        transform.position = _Target.GetBuildPosition();

        UI.SetActive(true);

        UpgradeCostText.text = string.Format("${0}", target.TurretBlueprint.UpgradeConst);
        SellCostText.text = string.Format("${0}", target.TurretBlueprint.SellCost);

    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        _Target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        _Target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }

}
