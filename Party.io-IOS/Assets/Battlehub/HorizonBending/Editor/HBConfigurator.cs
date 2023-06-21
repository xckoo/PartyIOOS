﻿#define BB
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Battlehub.HorizonBending
{
    public class HBConfigurator : EditorWindow
    {
        public static BendingMode Internal_BendingModePrefs
        {
            get
            {
                if (!EditorPrefs.HasKey("HBBendingModePref"))
                {
                    return BendingMode._HB_OFF;
                }
                return (BendingMode)EditorPrefs.GetInt("HBBendingModePref");
            }
            set
            {
                EditorPrefs.SetInt("HBBendingModePref", (int)value);
            }
        }

        public static bool Internal_AllowChangeBendingMode
        {
            get
            {
                if (!EditorPrefs.HasKey("HBAllowChangeBendingModePref"))
                {
                    return false;
                }
                return EditorPrefs.GetBool("HBAllowChangeBendingModePref");
            }
            set
            {
                EditorPrefs.SetBool("HBAllowChangeBendingModePref", value);
            }
        }

        [MenuItem("Tools/Horizon Bend/Configure", priority = 1)]
        private static void Init()
        {
            ShowWindow(false);
        }


        public static void ShowWindow(bool intro)
        {
            HBConfigurator prevWindow = GetWindow<HBConfigurator>();
            if (prevWindow != null)
            {
                prevWindow.Close();
            }

            HBConfigurator window = ScriptableObject.CreateInstance<HBConfigurator>();
            window.position = new Rect(200, 200, 280, 480);

            window.IsIntro = intro;
            window.titleContent = new GUIContent("Horizon Bend Config");


            window.ShowUtility();
        }

        public bool IsIntro;
        private int m_worldUpAxis = 1;
        private int m_fowardAxis = 0;
        private int m_profile;
        private Texture2D m_Logo;

        private void Awake()
        {
            m_Logo = (Texture2D)Resources.Load("HBLogo", typeof(Texture2D));
        }

        private void CreateTemplates()
        {
            string path = Application.dataPath + "/Battlehub/HorizonBending/Shaders/";
            if (!Directory.Exists(path))
            {
                Debug.LogError("Unable to CreateTemplates. Required " + path + " folder is missing. Try reimport");
                return;
            }

            string cginc = path + "CGIncludes/HB_Core.cginc";
            File.Copy(cginc, cginc.Replace("HorizonBending/Shaders/", "HorizonBending/Shaders/_Templates/"), true);

            string[] shaders = Directory.GetFiles(path, "*.shader", SearchOption.AllDirectories);
            foreach (string shaderPath in shaders)
            {
                string ext = ".hbst";
                if (shaderPath.Contains("BillboardTree.shader") ||
                   shaderPath.Contains("HB_SpeedTreeBillboard.shader"))
                {
                    ext = ".hbstm";
                }

                string templatePath = shaderPath.Replace("HorizonBending/Shaders/", "HorizonBending/Shaders/_Templates/").Replace(".shader", ext);
                Directory.CreateDirectory(Path.GetDirectoryName(templatePath));
                File.Copy(shaderPath, templatePath, true);
            }

        }

        private void RestoreTemplates()
        {
            string path = Application.dataPath + "/Battlehub/HorizonBending/Shaders/_Templates";
            if (!Directory.Exists(path))
            {
                Debug.LogError("Unable to RestoreTemplates. Required " + path + " folder is missing. Try reimport");
                return;
            }

            string cginc = path + "/CGIncludes/HB_Core.cginc";
            File.Copy(cginc, cginc.Replace("_Templates", string.Empty), true);

            string[] templates = Directory.GetFiles(path, "*.hbst", SearchOption.AllDirectories);
            foreach (string templatePath in templates)
            {
                string shaderPath = templatePath.Replace("_Templates", string.Empty).Replace(".hbst", ".shader");
                Directory.CreateDirectory(Path.GetDirectoryName(shaderPath));
                File.Copy(templatePath, shaderPath, true);
            }

            templates = Directory.GetFiles(path, "*.hbstm", SearchOption.AllDirectories);
            foreach (string templatePath in templates)
            {
                string shaderPath = templatePath.Replace("_Templates", string.Empty).Replace(".hbstm", ".shader");
                Directory.CreateDirectory(Path.GetDirectoryName(shaderPath));
                File.Copy(templatePath, shaderPath, true);
            }
        }

        private void Configure()
        {
            string path = Application.dataPath + "/Battlehub/HorizonBending/Shaders/_Templates";
            if (!Directory.Exists(path))
            {
                Debug.LogError("Unable to configure. Required " + path + " folder is missing. Try reimport");
                return;
            }

            string properties;
            string feature;
            string hbst;
            string hbstm;
            BendingMode bendingMode = BendingMode._HB_XZ_YUP_ZFWD;
            if (m_profile == 0)
            {
                if (m_worldUpAxis == 0)   //X 
                {
                    if (m_fowardAxis == 0)
                    {
                        feature = "_HB_YZ_XUP_ZFWD 1";
                        bendingMode = BendingMode._HB_YZ_XUP_ZFWD;
                    }
                    else
                    {
                        feature = "_HB_YZ_XUP_YFWD 1";
                        bendingMode = BendingMode._HB_YZ_XUP_YFWD;
                    }
                }
                else if (m_worldUpAxis == 1) //Y
                {
                    if (m_fowardAxis == 0)
                    {
                        feature = "_HB_XZ_YUP_ZFWD 1";
                        bendingMode = BendingMode._HB_XZ_YUP_ZFWD;
                    }
                    else
                    {
                        feature = "_HB_XZ_YUP_XFWD 1";
                        bendingMode = BendingMode._HB_XZ_YUP_XFWD;
                    }
                }
                else //Z
                {
                    if (m_fowardAxis == 0)
                    {
                        feature = "_HB_XY_ZUP_YFWD 1";
                        bendingMode = BendingMode._HB_XY_ZUP_YFWD;
                    }
                    else 
                    {
                        feature = "_HB_XY_ZUP_XFWD 1";
                        bendingMode = BendingMode._HB_XY_ZUP_XFWD;
                    }
                }

                properties = string.Empty;
                hbst = "#define " + feature;
                hbstm = "#define " + feature;
            }
            else
            {
                feature = "#pragma multi_compile _HB_YZ_XUP_ZFWD _HB_YZ_XUP_YFWD _HB_XZ_YUP_ZFWD _HB_XZ_YUP_XFWD _HB_XY_ZUP_YFWD _HB_XY_ZUP_XFWD";
                hbst = feature;
                hbstm = feature;
                properties = "XZ_YUp_ZFwd, XZ_YUp_XFwd, YZ_XUp_ZFwd, YZ_XUp_YZFwd, XY_ZUp_YFwd, XY_ZUp_XFwd";
            }

            properties = string.Format("[KeywordEnum({0})] _HB(\"Bending Mode\", Float) = 0", properties);
            bool importShaders = bendingMode != Internal_BendingModePrefs;
            if (!importShaders)
            {
                importShaders = !ShadersExist(path, "hbst") || !ShadersExist(path, "hbstm");
            }
            if (importShaders)
            {
                CreateShaders(m_profile, path, properties, hbst, "hbst");
                CreateShaders(m_profile, path, properties, hbstm, "hbstm");
            }
            HB hb = GameObject.FindObjectOfType<HB>();
            if (hb != null)
            {
                hb.Internal_Rollback(true);
                GameObject.DestroyImmediate(hb.gameObject);
            }

            GameObject hbGO = StorageHelper.InstantiatePrefab("HB.prefab");
            hb = hbGO.GetComponent<HB>();
            hb.BendingMode = bendingMode;
            Internal_BendingModePrefs = bendingMode;
            if (m_profile == 0)
            {
                Internal_AllowChangeBendingMode = false;
            }
            else
            {
                Internal_AllowChangeBendingMode = true;
            }
            StorageHelper.CreatePrefab(hbGO.name + ".prefab", hbGO);
            DestroyImmediate(hbGO);
            if (importShaders)
            {
                AssetDatabase.ImportAsset("Assets/Battlehub/HorizonBending/Shaders/", ImportAssetOptions.ImportRecursive);
            }

            EditorPrefs.SetBool("HBConfigured", true);
        }


        private bool ShadersExist(string path, string ext)
        {
            string[] templates = Directory.GetFiles(path, "*." + ext, SearchOption.AllDirectories);
            foreach (string templatePath in templates)
            {
                string shaderPath = templatePath.Replace("_Templates", string.Empty).Replace("." + ext, ".shader");
                if (!File.Exists(shaderPath))
                {
                    return false;
                }
            }
            return true;
        }

        private static void CreateShaders(int profile, string path, string properties, string feature, string ext)
        {
            string cginc = path + "/CGIncludes/HB_Core.cginc";
            string hbCoreContent = File.ReadAllText(cginc);

            if (profile == 0)
            {
                hbCoreContent = hbCoreContent.Replace("HB_FEATURE", feature);
            }
            else
            {
                hbCoreContent = hbCoreContent.Replace("HB_FEATURE", string.Empty);
            }
            cginc = cginc.Replace("_Templates", string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(cginc));
            File.WriteAllText(cginc, hbCoreContent);

            string[] templates = Directory.GetFiles(path, "*." + ext, SearchOption.AllDirectories);
            foreach (string templatePath in templates)
            {
                string fileContents = File.ReadAllText(templatePath);
                if (profile == 0)
                {
                    fileContents = fileContents.Replace("HB_FEATURE", string.Empty);
                    fileContents = fileContents.Replace("HB_PROPERTIES", string.Empty);
                }
                else
                {
                    fileContents = fileContents.Replace("HB_FEATURE", feature);
                    fileContents = fileContents.Replace("HB_PROPERTIES", properties);
                }

                string shaderPath = templatePath.Replace("_Templates", string.Empty).Replace("." + ext, ".shader");
                Directory.CreateDirectory(Path.GetDirectoryName(shaderPath));
                File.WriteAllText(shaderPath, fileContents);
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.Separator();

            GUILayout.Label(m_Logo);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.Separator();

            m_profile = EditorGUILayout.Popup("Profile", m_profile,
                new[]
                {
                    "One Shader Feature",
                   // "All Shader Features",
                    "Multicompile"
                });

            if (m_profile == 0)
            {
                m_worldUpAxis = EditorGUILayout.Popup("World Up Axis", 1 /*m_worldUpAxis*/,
                    new[]
                    {
                        "X",
                        "Y",
                        "Z"
                    });


                if (m_worldUpAxis == 0)
                {
                    m_fowardAxis = EditorGUILayout.Popup("Forward Axis", m_fowardAxis,
                        new[]
                        {
                                "Z",
                                "Y",
                        });
                }
                else if (m_worldUpAxis == 1)
                {
                    m_fowardAxis = EditorGUILayout.Popup("Forward Axis", 0 /*m_fowardAxis*/,
                       new[]
                       {
                                "Z",
                                "X",
                       });
                }
                else
                {
                    m_fowardAxis = EditorGUILayout.Popup("Forward Axis", m_fowardAxis,
                       new[]
                       {
                                "Y",
                                "X",
                       });
                }


                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("If for some reason you need all shader features please select 'Multicompile' profile", EditorStyles.wordWrappedMiniLabel);

                EditorGUILayout.Separator();

                EditorGUI.EndChangeCheck();

                #if HB_GENESIS
                if (GUILayout.Button("Create Shader Templates"))
                {
                    Close();
                    if (EditorUtility.DisplayDialog("Are you sure?", "Templates will be overriden with new ones", "Do It", "Cancel"))
                    {
                        CreateTemplates();
                    }

                }
                if (GUILayout.Button("Edit Shader Templates"))
                {
                    Close();
                    if (EditorUtility.DisplayDialog("Are you sure?", "Shaders will be overriden with raw templates", "Do It", "Cancel"))
                    {
                        RestoreTemplates();
                    }
                }
                #endif

                if (GUILayout.Button("Configure"))
                {
                    Close();
                    if (Application.isPlaying)
                    {
                        EditorUtility.DisplayDialog("Unable to Remove", "Application.isPlaying == true", "OK");
                    }
                    else
                    {

                        Configure();
                    }
                }
            }
            else
            {
                EditorGUI.EndChangeCheck();

                if (m_profile != 0)
                {
                    EditorGUILayout.LabelField("All shader features will be included using #pragma_multicompile directive", EditorStyles.wordWrappedLabel);
                }

                EditorGUILayout.LabelField("WARNING: Although all configurations will be accessible it could take a huge amount of time to compile shaders with all features", EditorStyles.wordWrappedMiniLabel);

                EditorGUILayout.Separator();

                if (GUILayout.Button("Configure"))
                {
                    this.Close();

                    if (Application.isPlaying)
                    {
                        EditorUtility.DisplayDialog("Unable to Remove", "Application.isPlaying == true", "OK");
                    }
                    else
                    {
                        Configure();
                    }

                }
            }


            EditorGUILayout.Separator();

            if (IsIntro)
            {
                EditorGUILayout.LabelField("NOTE: This window could always be accessed from Tools->Horizon Bending->Config", EditorStyles.wordWrappedMiniLabel);
            }
        }
    }
}
