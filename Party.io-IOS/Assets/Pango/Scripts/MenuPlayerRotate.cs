using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerRotate : MonoBehaviour {
    Touch touch;
    Vector2 touchPosition;
    Quaternion rotationY;
    float rotateSpeed = 0.1f;
    // Use this for initialization
    private void Update()
    {
        if (Input.touchCount >0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase== TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0, -touch.deltaPosition.x * rotateSpeed, 0);
                transform.rotation = rotationY * transform.rotation;
            }
        }
      
    }

   /* void OnMouseDrag()
	{
        float rotY = Input.GetAxis("Mouse Y") * rotateSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.right, rotY);
	}*/
}
