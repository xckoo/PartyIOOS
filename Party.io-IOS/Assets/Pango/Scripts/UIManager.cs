using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text nameHolder;
    [SerializeField] Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerValuesOnStart();
    }
    public void PlayerValuesOnStart()
    {
                int rankNo = PrefManager.GetRank();

        string guestName = "Guest" + Random.Range(0000, 5000).ToString();
        if (!string.IsNullOrEmpty(PrefManager.GetUserName()))
        {
            nameHolder.text = PrefManager.GetUserName();
        }
        else
        {
            PrefManager.SetUserName(guestName);
            nameHolder.text = PrefManager.GetUserName();
        }

    }
    public void SaveUserNameOnStartPlay()
    {
        if (string.IsNullOrEmpty(nameText.text))
        {
            PrefManager.SetUserName(nameHolder.text);
        }
        else
        {
            PrefManager.SetUserName(nameText.text);
        }
    }
}
