using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    public GameObject NodePrefab;
    public Transform parent;

    void Awake()
    {
//        Create();
    }

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    [ContextMenu("xxxx")]
    public void Create()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                var pos = new Vector3(i * 4 + 1 * i, 0, j * 4 + 1 * j);
                GameObject go = Instantiate(NodePrefab, pos, Quaternion.identity, parent);
                //                go.transform.localPosition = new Vector3(i * 4, 1, j * 4);
            }
        }
    }
}
