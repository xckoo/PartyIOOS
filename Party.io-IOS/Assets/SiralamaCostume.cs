using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiralamaCostume : MonoBehaviour
{
    public GameObject[] kiyafet;
    public Color[] renk;
    Color colorx, colorx1, colorx2, colorx3, colorx4, colorx5, colorx6, colorx7, colorx8, colorx9, colorx10, colorx11, 
        colorx12, colorx13, colorx14, colorx15, colorx16, colorx17, colorx18, colorx19, colorx20, colorx21, colorx22,
        colorx23, colorx24, colorx25, colorx26, colorx27, colorx28, colorx29, colorx30,
        colorx31, colorx32, colorx33, colorx34, colorx35;
    
    public Material adamRenk;
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
    public Material pink;
    bool isTextureScrolling = false;
    public GameObject playerEyes;
   

    public GameObject man;
    float scrollX = 0.02f;
    float scrollY = 0f;
    float offsetX;
    private int kiyafet_deg;
    float offsetY;
    // Use this for initialization

    public List<Transform> trnsfrm = new List<Transform>();
    public List<Vector3> pos = new List<Vector3>();
    public List<Quaternion> rot = new List<Quaternion>();


    public List<ConfigurableJoint> confi = new List<ConfigurableJoint>();

    void Start()
    {
        GetColor();

        foreach (var t in transform.GetChild(0).GetComponentsInChildren<Transform>())
        {
            if (t.transform == transform.GetChild(0))
            {

            }
            else if (t.GetComponent<Transform>())
            {
                trnsfrm.Add(t.GetComponent<Transform>());
                pos.Add(t.GetComponent<Transform>().localPosition);
                rot.Add(t.GetComponent<Transform>().localRotation);
            }
        }
        
        kiyafet_deg = PlayerPrefs.GetInt("kiyafet_deg");
        for (int i = 0; i < kiyafet.Length; i++)
        {
            kiyafet[i].SetActive(false);
            for (int x = 0; x < kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
            {
                kiyafet[i].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(false);
            }

        }

        kiyafet[kiyafet_deg].gameObject.SetActive(true);
        adamRenk.color = renk[kiyafet_deg];

        switch (kiyafet_deg)
        {
            case 1:
                man.GetComponent<SkinnedMeshRenderer>().material = blackWhite;
                playerEyes.SetActive(false);

                break;
            case 11:
            case 12:
            case 14:
            case 22:
            case 25:
            case 26:
            case 30:
                man.GetComponent<SkinnedMeshRenderer>().material = adamRenk;
                playerEyes.SetActive(false);

                break;
            case 2:
                man.GetComponent<SkinnedMeshRenderer>().material = jacket;
                playerEyes.SetActive(true);

                break;
            case 3:
                man.GetComponent<SkinnedMeshRenderer>().material = panda;
                break;
            case 4:
                man.GetComponent<SkinnedMeshRenderer>().material = green;
                playerEyes.SetActive(true);


                break;
            case 5:
                man.GetComponent<SkinnedMeshRenderer>().material = skull;
                playerEyes.SetActive(false);

                break;
            case 6:
                man.GetComponent<SkinnedMeshRenderer>().material = marshmallow;
                playerEyes.SetActive(false);

                break;
            case 8:
                man.GetComponent<SkinnedMeshRenderer>().material = snowMan;
                playerEyes.SetActive(false);

                break;
            case 9:
                man.GetComponent<SkinnedMeshRenderer>().material = doctor;
                playerEyes.SetActive(true);

                break;
            case 10:
                man.GetComponent<SkinnedMeshRenderer>().material = clown;
                playerEyes.SetActive(false);

                break;
            case 15:
                man.GetComponent<SkinnedMeshRenderer>().material = space;
                playerEyes.SetActive(false);

                isTextureScrolling = true;
                break;
            case 19:
                man.GetComponent<SkinnedMeshRenderer>().material = basketball;
                playerEyes.SetActive(true);

                break;
            case 20:
                man.GetComponent<SkinnedMeshRenderer>().material = ninja;
                playerEyes.SetActive(false);

                break;
            case 23:
                man.GetComponent<SkinnedMeshRenderer>().material = orange;
                playerEyes.SetActive(true);

                break;
            case 27:
                man.GetComponent<SkinnedMeshRenderer>().material = blue;
                playerEyes.SetActive(true);

                break;
            case 34:
                man.GetComponent<SkinnedMeshRenderer>().material = pink;
                playerEyes.SetActive(true);

                break;
            case 35:
                man.GetComponent<SkinnedMeshRenderer>().material = robot;
                playerEyes.SetActive(false);

                break;
            default:
                man.GetComponent<SkinnedMeshRenderer>().material = adamRenk;
                playerEyes.SetActive(true);

                break;
        }
        for (int x = 0; x < kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet.Length; x++)
        {
            kiyafet[kiyafet_deg].GetComponent<acilacak_kiyafetler>().kiyafet[x].SetActive(true);
        }
        if (kiyafet_deg == 15)
            isTextureScrolling = true;
        else
            isTextureScrolling = false;

    }

    private void GetColor()
    {
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
        ColorUtility.TryParseHtmlString("#762075", out colorx25);
        ColorUtility.TryParseHtmlString("#A6651A", out colorx26);
        ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx27);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx28);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx29);
        ColorUtility.TryParseHtmlString("#018AC5", out colorx30);
        ColorUtility.TryParseHtmlString("#DEC772", out colorx31);
        ColorUtility.TryParseHtmlString("#FB00C0FF", out colorx32);
        ColorUtility.TryParseHtmlString("#FF8F18FF", out colorx33);
        ColorUtility.TryParseHtmlString("#7DC51DFF", out colorx34);
        ColorUtility.TryParseHtmlString("#6BFFF3FF", out colorx35);
        
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
    }
}

