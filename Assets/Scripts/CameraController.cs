using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _DoMovement = true;
    private float _PanSpeed = 30f;
    private float _PanBorderThickness = 10f;

    private float _ScrollSpeed = 5f;
    private float _MinY = 10f;
    private float _MaxY = 80f;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _DoMovement = !_DoMovement;
        }
        if (!_DoMovement)
            return;
        
//        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - _PanBorderThickness)
//        {
//            transform.Translate(Vector3.forward * _PanSpeed * Time.deltaTime, Space.World);
//        }
//        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= _PanBorderThickness)
//        {
//            transform.Translate(Vector3.back * _PanSpeed * Time.deltaTime, Space.World);
//        }
//        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - _PanBorderThickness)
//        {
//            transform.Translate(Vector3.right * _PanSpeed * Time.deltaTime, Space.World);
//        }
//        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= _PanBorderThickness)
//        {
//            transform.Translate(Vector3.left * _PanSpeed * Time.deltaTime, Space.World);
//        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _PanSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * _ScrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, _MinY, _MaxY);
        transform.position = pos;
    }
}
