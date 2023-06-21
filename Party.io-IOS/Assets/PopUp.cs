using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
   public void ResetPopUp()
   {
      PlayerPrefs.SetInt("PopUp",1);
   }
}
