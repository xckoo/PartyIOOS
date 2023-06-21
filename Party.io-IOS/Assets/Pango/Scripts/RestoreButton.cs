using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreButton : MonoBehaviour {

    
    // Use this for initialization
    void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.OSXPlayer)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void ClickRestore()
    {
        Purchaser.Instance.RestorePurchases();
    }
}
