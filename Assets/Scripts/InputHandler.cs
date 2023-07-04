using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    public Action<Vector2> ShipUp;
    public Action<Vector3> ShipDrag;
    public Action<Vector2> ShipDown;

    public static InputHandler Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
    }

    [SerializeField] private Camera _camera;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        { 

           ShipUp?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
           
        }

        if (Input.GetMouseButton(0))
        {

               ShipDrag?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));

        }
        if (Input.GetMouseButtonUp(0)) 
        {
                ShipDown?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            
        }

    }
}
