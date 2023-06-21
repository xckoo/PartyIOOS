using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour {

    public enum ItemType
    {
        
        RemoveAds,
    }
    public ItemType itemType;
  

    private string defaultText;
    // Use this for initialization
    void Start()
    {
       
       
    }

    public void ClickBuy()
    {
        switch (itemType)
        {
            case ItemType.RemoveAds:
                Purchaser.Instance.Buyads();
                break;

        }
    }
}
