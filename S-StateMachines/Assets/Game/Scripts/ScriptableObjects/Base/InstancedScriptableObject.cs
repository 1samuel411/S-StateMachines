using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class InstancedScriptableObject<ScriptableObjT> : ScriptableObject where ScriptableObjT : ScriptableObject
    {
        public const string resourcesPath = "Data/";

        public static ScriptableObjT instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        string path = resourcesPath + typeof(ScriptableObjT).ToString();
                        _instance = Resources.Load(path, typeof(ScriptableObjT)) as ScriptableObjT;
                    }
                    catch
                    {
                    }
                }
                return _instance;
            }
        }
        private static ScriptableObjT _instance;
        private readonly string insertPath;

        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 15)]

        public static void CreateMyAsset()
        {
#if UNITY_EDITOR
            if (instance != null)
            {
                UnityEditor.Selection.activeObject = instance;
                return;
            }
            ScriptableObjT asset = ScriptableObject.CreateInstance<ScriptableObjT>();
            UnityEditor.AssetDatabase.CreateAsset(asset, "Assets/Game/Resources/" + resourcesPath + typeof(ScriptableObjT).ToString() + ".asset");
            UnityEditor.AssetDatabase.SaveAssets();

            UnityEditor.EditorUtility.FocusProjectWindow();

            UnityEditor.Selection.activeObject = asset;
#endif
        }
    }
}
