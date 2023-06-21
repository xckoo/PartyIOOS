using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlocked_character : MonoBehaviour {
    private Color colorx, colorx1, colorx2, colorx3, colorx4, colorx5, colorx6, colorx7, colorx8, colorx9, colorx10, colorx11, colorx12, colorx13, colorx14, colorx15
         , colorx16, colorx17, colorx18, colorx19, colorx20, colorx21, colorx22, colorx23, colorx24, colorx25, colorx26, colorx27, colorx28, colorx29, colorx30,
         colorx31, colorx32, colorx33, colorx34, colorx35;
    public Color[] renk;
    public GameObject eyes;
    public Material adam_renk;
    public Material snowMan;
    public Material panda;
    public Material blackWhite;
    public Material jacket;
    public Material clown;
    public Material doctor;
    public Material basketball;
    public Material marshmallow;
    public Material skull;
    public Material ninja;
    public Material robot;
    public Material space;
    public Material blue;
    public Material green;
    public Material orange;
    public GameObject player_bas;

    float scrollX = 0.02f;
    float scrollY = 0f;
    float offsetX;
    float offsetY;
    bool isTextureScrolling = false;
    public GameObject coinButton;
    public GameObject continueButton;
    public Text coinText;
    public Text currentCoin;
   
    void Start () {
        ColorUtility.TryParseHtmlString("#87A323FF", out colorx);
        ColorUtility.TryParseHtmlString("#FF2294FF", out colorx1);
        ColorUtility.TryParseHtmlString("#D23D11FF", out colorx2);
        ColorUtility.TryParseHtmlString("#1F4AC9FF", out colorx3);
        ColorUtility.TryParseHtmlString("#CF6FF9", out colorx4);
       
        ColorUtility.TryParseHtmlString("#7F12F0FF", out colorx5);
        ColorUtility.TryParseHtmlString("#A3845EFF", out colorx6);
        ColorUtility.TryParseHtmlString("#000000", out colorx7);
        ColorUtility.TryParseHtmlString("#D40F0BFF", out colorx8);
        ColorUtility.TryParseHtmlString("#206EC9FF", out colorx9);
        ColorUtility.TryParseHtmlString("#4113D2FF", out colorx10);
      
        ColorUtility.TryParseHtmlString("#B572BC", out colorx11);
        ColorUtility.TryParseHtmlString("#14AD17", out colorx12);
        ColorUtility.TryParseHtmlString("#2067E7", out colorx13);
        ColorUtility.TryParseHtmlString("#2E86B5", out colorx14);
       
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx15);
     
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx16);

        ColorUtility.TryParseHtmlString("#931E89", out colorx18);


        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx19);



        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx21);
        ColorUtility.TryParseHtmlString("#BB782C", out colorx22);
        // ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx22);B572BC

        //ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx23);

        //ColorUtility.TryParseHtmlString("#6BFFF3FF", out colorx25);
        //ColorUtility.TryParseHtmlString("#FFFFFF", out colorx25);
        // ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx26);
        ColorUtility.TryParseHtmlString("#762075", out colorx25);
        ColorUtility.TryParseHtmlString("#A6651A", out colorx26);
        ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx27);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx28);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx29);
        // ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx30);
        ColorUtility.TryParseHtmlString("#018AC5", out colorx30);
        ColorUtility.TryParseHtmlString("#DEC772", out colorx31);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx32);
        ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx33);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx34);
        ColorUtility.TryParseHtmlString("#6BFFF3FF", out colorx35);



        //4CD13C

        currentCoin.text = PlayerPrefs.GetInt("Coin").ToString();

        
        renk[0] = colorx;
        renk[1] = colorx1;
        renk[2] = colorx2;
        renk[3] = colorx3;
        renk[4] = colorx4;
        renk[5] = colorx5;
        renk[6] = colorx6;
        renk[7] = colorx7;
        renk[8] = colorx8;
        renk[9] = colorx9;
        renk[10] = colorx10;
        renk[11] = colorx11;
        renk[12] = colorx12;
        renk[13] = colorx13;
        renk[14] = colorx14;
        renk[15] = colorx15;
        renk[16] = colorx16;
        renk[17] = colorx17;
        renk[18] = colorx18;
        renk[19] = colorx19;
        renk[20] = colorx20;
        renk[21] = colorx21;
        renk[22] = colorx22;
        renk[23] = colorx23;
        renk[24] = colorx24;
        renk[25] = colorx25;
        renk[26] = colorx26;
        renk[27] = colorx27;
        renk[28] = colorx28;
        renk[29] = colorx29;
        renk[30] = colorx30;
        renk[31] = colorx31;
        renk[32] = colorx32;
        renk[33] = colorx33;
        renk[34] = colorx34;
        renk[35] = colorx35;


        Debug.Log("Unlocked"+ " "+PlayerPrefs.GetInt("unlocked"));

        
        
        transform.GetChild(PlayerPrefs.GetInt("unlocked")).gameObject.SetActive(true);


       for (int i = 0; i < transform.GetChild (PlayerPrefs.GetInt ("unlocked")).GetComponent<acilacak_kiyafetler>().kiyafet.Length; i++) {
			transform.GetChild (PlayerPrefs.GetInt ("unlocked")).GetComponent<acilacak_kiyafetler> ().kiyafet [i].gameObject.SetActive (true);
		}
       
        
        
        switch (PlayerPrefs.GetInt("unlocked")+1)
        {
            case 2:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = blackWhite;
                eyes.SetActive(false);
                break;
           
           
            case 3:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = jacket;
                eyes.SetActive(true);
                break;
            case 4:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = panda;
                break;
            case 5:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = green;
                eyes.SetActive(true);
                break;
            case 6:
        
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "200";
                player_bas.GetComponent<SkinnedMeshRenderer>().material = skull;
                
                eyes.SetActive(false);
                break;
            case 7:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = marshmallow;
                eyes.SetActive(false);
                break;
            case 9:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = snowMan;
                eyes.SetActive(false);
                break;
            case 10:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = doctor;
                eyes.SetActive(true);
                break;
            case 11:
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "250";
                player_bas.GetComponent<SkinnedMeshRenderer>().material = clown;
               
                eyes.SetActive(false);
                break;
            case 12:
            case 13:
            case 15:
            case 23:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
               
                break;
            case 16:
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "300";
                player_bas.GetComponent<SkinnedMeshRenderer>().material = space;
                eyes.SetActive(false);
                isTextureScrolling = true;
                
                break;
            case 20:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = basketball;
                eyes.SetActive(true);
                break;
            case 21:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = ninja;
                eyes.SetActive(false);
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "350";
                break;
            case 22:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = orange;
                eyes.SetActive(true);
                
                break;
            case 26:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "400";
               
                break;
            case 27:
               
                player_bas.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
                break;
                
           
            case 28:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = blue;
                eyes.SetActive(true);
                break;
            
            case 31:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                eyes.SetActive(false);
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "450";
                break;
            case 36:
                continueButton.SetActive(false);
                coinButton.SetActive(true);
                coinText.text = "500";
                player_bas.GetComponent<SkinnedMeshRenderer>().material = robot;
                eyes.SetActive(false);
               
                break;
            default:
                player_bas.GetComponent<SkinnedMeshRenderer>().material = adam_renk;
                adam_renk.color = renk [PlayerPrefs.GetInt ("unlocked")];             
                eyes.SetActive(true);
                break;
        }
        // switch (PlayerPrefs.GetInt("unlocked"))
        // {
        //     case 6:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "200";
        //         break;
        //     case 11:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "250";
        //         break;
        //     case 16:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "300";
        //         break;
        //     
        //         
        //     case 21:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "350";
        //         break;
        //     case 26:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "400";
        //         break;
        //     case 31:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "450";
        //         break;
        //     case 36:
        //         continueButton.SetActive(false);
        //         coinButton.SetActive(true);
        //         coinText.text = "500";
        //         break;
        //     
        // }
    }
	
	// Update is called once per frame
	void Update () {
        if (isTextureScrolling)
        {
            offsetX = Time.time * scrollX;
            offsetY = Time.time * scrollY;
            player_bas.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);


        }
      

    }
}
