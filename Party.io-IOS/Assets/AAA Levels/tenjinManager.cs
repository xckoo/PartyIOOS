using UnityEngine;
using System.Collections;
using System;
using UnityEngine.iOS;

public class tenjinManager : MonoBehaviour
{

  

    void Start()
    {
        TenjinConnect();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            TenjinConnect();
        }
    }

    public void TenjinConnect()
    {
        BaseTenjin instance = Tenjin.getInstance("PNXSQ122GDZH4VSUZ73NAC7MQSTRMDHY");
       

#if UNITY_IOS
        if (new Version(Device.systemVersion).CompareTo(new Version("14.0")) >= 0)
        {
            // Tenjin wrapper for requestTrackingAuthorization
            instance.RequestTrackingAuthorizationWithCompletionHandler((status) => {
                Debug.Log("===> Tenjin-App Tracking Transparency Authorization Status: " + status);

                // Sends install/open event to Tenjin
                instance.Connect();

            });
        }
        else
        {
            instance.Connect();
        }
#elif UNITY_ANDROID

      // Sends install/open event to Tenjin
      instance.Connect();

#endif
    }
}