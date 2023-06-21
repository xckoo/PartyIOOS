using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterRotater : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    private void OnMouseDrag()
    {
        float y = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, -y);
    }

}
