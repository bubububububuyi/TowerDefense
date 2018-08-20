using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;
    private Node _Target;

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
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

}
