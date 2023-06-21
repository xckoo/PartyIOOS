using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPick : MonoBehaviour
{
    public WheelManager wheelManager;
    SliceManager sliceManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (wheelManager.getCollide)
        {
            sliceManager = other.GetComponentInParent<SliceManager>();
            wheelManager.getCollide = false;
            sliceManager.GetCoin();
            Debug.Log(other.name);
        }
        
        
    }
}
