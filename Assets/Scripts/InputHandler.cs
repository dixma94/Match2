using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    public Func<Vector2,CellScript> ShipUp;
    public Action<Vector3> ShipDrag;
    public Action<Vector2> ShipDown;

    [SerializeField] private Camera _camera;

    private CellScript cell;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        { 
            if(ShipUp != null)
            {
               cell = ShipUp.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
           
        }

        if (Input.GetMouseButton(0))
        {
            if( ShipDrag != null && cell!= null)
            {
               ShipDrag.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if(ShipDown != null && cell != null)
            {
                ShipDown.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
                cell = null;
            }
            
        }

    }
}
