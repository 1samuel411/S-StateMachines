using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SLibrary.StateMachines.ScriptableController
{
    public class GenerateScripts : ScriptableObject
    {

        [SerializeField]
        public Object baseControllerTemplate;
        [SerializeField]
        public Object controllerTemplate;
        [SerializeField]
        public Object stateTemplate;
        [TextArea]
        private string addStateCase =
@"
                case SSTATES_ENUM_ENTRY.SSTATE_NAME_ENTRY:
                    return new SSTATE_INSTANCE_ENTRY();
";

        private static GenerateScripts _instance;
        public static GenerateScripts instance
        {
            get
            {
                if (_instance == null)
                {
                    string[] paths = AssetDatabase.FindAssets("t:GenerateScripts");
                    if (paths.Length != 1)
                        throw new System.Exception("Error finding Generate Scripts Settings");
                    else
                        _instance = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(paths[0]), typeof(GenerateScripts)) as GenerateScripts;
                }

                if (_instance == null)
                    throw new System.Exception("Error getting Generate Scripts Settings");

                return _instance;
            }
        }

        private static string GetScriptTemplateContents(Object scriptFile)
        {
            string templatePath = AssetDatabase.GetAssetPath(scriptFile);
            string content = File.ReadAllText(templatePath);
            return content;
        }

        /// <summary>
        /// IF DOESNT EXIST | Create a folder in the path of the ScriptableStateController named, <INSERTNAME>Scripts',
        /// IF DOESNT EXIST | Create folders within that called, 'Base' and 'States'
        /// IF DOESNT EXIST, IN ROOT FOLDER | Create a <INSERTNAME>Controller from the ControllerTemplate
        /// IN 'Base' FOLDER | Create a Base<INSERTNAME>Controller from the BaseControllerTemplate
        /// IN 'States' FOLDER | For every state create a <INSERTNAME>State from the StateTemplate
        /// </summary>
        public static async Task RegenerateScripts(ScriptableStateController controller)
        {
            string path = Path.GetFullPath(Path.Combine(AssetDatabase.GetAssetPath(controller), @"..\"));

            // Ex: Assets/StateMachines/ExampleStateController.asset
            // Will become, Assets/StateMachines/ExampleStateControllerScripts/
            string controllerName = controller.name.Replace(" ", "");
            string cachedControllerName = controller.cachedName.Replace(" ", "");
            string folderPath = path + controllerName + "Scripts/";
            string baseFolderPath = folderPath + "Base/";
            string statesFolderPath = folderPath + "States/";

            string controllerFilePath = folderPath + controllerName + "Controller.cs";
            string cachedControllerFilePath = folderPath + cachedControllerName + "Controller.cs";

            string baseControllerFilePath = baseFolderPath + "Base" + controllerName + "Controller.cs";
            string cachedBaseControllerFilePath = baseFolderPath + "Base" + cachedControllerName + "Controller.cs";

            string baseStateControllerName = "Base" + controllerName + "State";
            string enumName = controllerName + "States";
            string defaultState = "None";
            if (controller.states.Exists(x => x.defaultState))
                defaultState = controller.states.First(x => x.defaultState).name.Replace(" ", "");

            bool namespaceChanged = false;
            bool nameChanged = false;
            string previousNamespace = controller.cachedNamespace;
            string previousName = controller.cachedName;

            #region Check For Rename
            // Check if the controller was renamed
            if (controller.cachedName != "" && controller.cachedName != controller.name)
            {
                Debug.Log("[Generation] ...Renaming State Machine from " + cachedControllerName + ", to " + controllerName + "...");
                // Rename the folder
                string cachedPath = path + cachedControllerName + "Scripts/";
                if (Directory.Exists(cachedPath))
                    Directory.Move(cachedPath, folderPath);

                // Rename the controller
                if (File.Exists(cachedControllerFilePath))
                    File.Move(cachedControllerFilePath, controllerFilePath);

                // Rename the base controller
                if (File.Exists(cachedBaseControllerFilePath))
                    File.Move(cachedBaseControllerFilePath, baseControllerFilePath);
                Debug.Log("[Generation] !Renamed State Machine!");

                nameChanged = true;
            }
            // Update the cached name
            controller.cachedName = controller.name;
            #endregion

            #region Check for Namespace Rename
            // Check for a newnamespace... ....newman....
            if (controller.cachedNamespace != controller.namespaceName)
            {
                namespaceChanged = true;
                controller.cachedNamespace = controller.namespaceName;
            }
            #endregion

            #region CREATE DIRECTORIES
            // Create Root Directory
            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("[Generation] !Created  Root Directory!");
            }

            // Create Base Directory
            if (Directory.Exists(baseFolderPath) == false)
            {
                Directory.CreateDirectory(baseFolderPath);
                Debug.Log("[Generation] !Created Base Directory!");
            }

            // Create States Directory
            if (Directory.Exists(statesFolderPath) == false)
            {
                Directory.CreateDirectory(statesFolderPath);
                Debug.Log("[Generation] !Created States Directory!");
            }
            #endregion

            // Create Controller if it doesnt exist otherwise continue
            if (File.Exists(controllerFilePath) == false)
            {
                #region CREATE CONTROLLER
                Debug.Log("[Generation] ...Creating Controller File...");
                string controllerTemplate = GetScriptTemplateContents(GenerateScripts.instance.controllerTemplate);
                // Replace namespace
                controllerTemplate = controllerTemplate.Replace("SNAMESPACE_ENTRY", controller.namespaceName);
                // Replace inherited Class name
                controllerTemplate = controllerTemplate.Replace("BASE_SSTATECONTROLLER_ENTRY", "Base" + controllerName + "Controller");
                // Replace Class name
                controllerTemplate = controllerTemplate.Replace("SSTATECONTROLLER_ENTRY", controllerName + "Controller");
                // Replace Add Component Menu
                controllerTemplate = controllerTemplate.Replace("//[AddComponentMenu(\"States/\")]", "[AddComponentMenu(\"States/" + controller.name + "\")]");
                File.WriteAllText(controllerFilePath, controllerTemplate);
                Debug.Log("[Generation] !Created Controller File!");
                #endregion
            }
            else
            {
                #region UPDATE CONTROLLER
                string controllerContent = File.ReadAllText(controllerFilePath);
                Debug.Log("[Generation] ...Updating Controller File... Namespace Changed: " + namespaceChanged + ", Name Changed: " + nameChanged);
                // Update namespaces
                if (namespaceChanged)
                {
                    controllerContent = controllerContent.Replace("namespace " + previousNamespace, "namespace " + controller.namespaceName);
                }
                // Update name
                if (nameChanged)
                {
                    string previousNameRaw = previousName.Replace(" ", "");
                    controllerContent = controllerContent.Replace("AddComponentMenu(\"States/" + previousName + "\")", "AddComponentMenu(\"States/" + controller.name + "\")");
                    controllerContent = controllerContent.Replace("class " + previousNameRaw + "Controller", "class " + controllerName + "Controller");
                    controllerContent = controllerContent.Replace("Base" + previousNameRaw + "Controller", "Base" + controllerName + "Controller");
                }
                File.WriteAllText(controllerFilePath, controllerContent);
                Debug.Log("[Generation] !Updated Controller File!");
                #endregion
            }

            // Regenerate Base Controller ALWAYS
            #region REGENERATE BASE CONTROLLER
            Debug.Log("[Generation] ...Regenerating base controller...");
            string baseControllerTemplate = GetScriptTemplateContents(GenerateScripts.instance.baseControllerTemplate);
            // Replace namespace
            baseControllerTemplate = baseControllerTemplate.Replace("SNAMESPACE_ENTRY", controller.namespaceName);
            // Replace Class name
            baseControllerTemplate = baseControllerTemplate.Replace("BASE_SSTATECONTROLLER_ENTRY", "Base" + controllerName + "Controller");
            // Replace Controller name in State
            baseControllerTemplate = baseControllerTemplate.Replace("SSTATECONTROLLER_ENTRY", controllerName + "Controller");
            // Replace Enum declaration
            baseControllerTemplate = baseControllerTemplate.Replace("SSTATES_ENUM_ENTRY", enumName);
            // Replace Enum list
            baseControllerTemplate = baseControllerTemplate.Replace("/*SSTATES_ENUM_LIST_ENTRY*/", string.Join(",", controller.states.Select(x => x.name.Replace(" ", ""))));
            // Replace Default State
            baseControllerTemplate = baseControllerTemplate.Replace(enumName + ".None/*SDEFAULTSTATE_ENTRY*/", enumName + "." + defaultState);
            // Add enum selector
            string enumText = "";
            for (int i = 0; i < controller.states.Count; i++)
            {
                // Generate from this,
                // case SSTATES_ENUM_ENTRY.SSTATE_NAME_ENTRY:
                // return new SSTATE_INSTANCE_ENTRY();
                enumText += GenerateScripts.instance.addStateCase
                    .Replace("SNAMESPACE_ENTRY", controller.namespaceName)    // Namespace
                    .Replace("SSTATES_ENUM_ENTRY", enumName)    // Enum name
                    .Replace("SSTATE_NAME_ENTRY", controller.states[i].name.Replace(" ", ""))   // Enum Value
                    .Replace("SSTATE_INSTANCE_ENTRY", controller.states[i].name.Replace(" ", "") + "State");  // Class to add
            }
            baseControllerTemplate = baseControllerTemplate.Replace("// ADD_CASE_ENTRIES_ENTRY", enumText);
            // Replace Base State class name
            baseControllerTemplate = baseControllerTemplate.Replace("BASE_SSTATE_ENTRY", baseStateControllerName);

            File.WriteAllText(baseControllerFilePath, baseControllerTemplate);
            Debug.Log("[Generation] !Regenerated base controller!");
            #endregion

            // Find removed states
            #region Delete Removed State
            List<int> removedStates = controller.generatedStates.Select(x => x.id).Except(controller.states.Select(x => x.id)).ToList();
            for (int i = 0; i < removedStates.Count; i++)
            {
                // Removed State with the id, removedStates[i], delete script
                int index = controller.generatedStates.FindIndex(x => x.id == removedStates[i]);
                if (index < 0)
                {
                    throw new System.Exception("Could not find the state we're deleting");
                }
                Debug.Log("[Generation] ...Removing State " + controller.generatedStates[index].name + "...");
                string generatedStateName = controller.generatedStates[index].name.Replace(" ", "") + "State";
                string generatedStateNamePath = statesFolderPath + generatedStateName + ".cs";
                if (File.Exists(generatedStateNamePath) == false)
                {
                    Debug.Log("[Generation] ...Could not find file at " + generatedStateNamePath + "...");
                }
                else
                {
                    File.Delete(generatedStateNamePath);
                }

                controller.generatedStates.RemoveAt(index);
                Debug.Log("[Generation] !Removed State " + generatedStateName + "!");
            }
            #endregion


            // Iterate through the states
            for (int i = 0; i < controller.states.Count; i++)
            {
                // Check if the state was updated or created
                int generatedStateIndex = controller.generatedStates.FindIndex(x => x.id == controller.states[i].id);
                // We have a state with that id already, lets update it
                if (generatedStateIndex >= 0)
                {
                    #region UPDATE STATE
                    Debug.Log("[Generation] ...Updating " + controller.states[i].name + "State File... Namespace Changed: " + namespaceChanged + ", Name Changed: " + nameChanged);
                    
                    string stateName = controller.states[i].name.Replace(" ", "") + "State";
                    string cachedStateName = controller.generatedStates[generatedStateIndex].name.Replace(" ", "") + "State";

                    // Update file name if name changed
                    if(stateName != cachedStateName)
                    {
                        string oldPath = statesFolderPath + cachedStateName + ".cs";
                        string newPath = statesFolderPath + stateName + ".cs";
                        File.Move(oldPath, newPath);
                    }

                    string stateNamePath = statesFolderPath + stateName + ".cs";
                    string stateContent = File.ReadAllText(stateNamePath);
                    // Update namespaces
                    if (namespaceChanged)
                    {
                        stateContent = stateContent.Replace("namespace " + previousNamespace, "namespace " + controller.namespaceName);
                    }
                    // Update State Machine nmae
                    if (nameChanged)
                    {
                        string previousNameRaw = previousName.Replace(" ", "");
                        stateContent = stateContent.Replace("Base" + previousNameRaw + "State", "Base" + controllerName + "State");
                        stateContent = stateContent.Replace("(" + previousNameRaw + "States", "(" + controllerName + "States");
                    }
                    // Update State name
                    if (stateName != cachedStateName)
                    {
                        stateContent = stateContent.Replace(cachedStateName, stateName);
                    }
                    File.WriteAllText(stateNamePath, stateContent);
                    controller.generatedStates[generatedStateIndex] = controller.states[i];
                    Debug.Log("[Generation] !Updated State File!");
                    #endregion
                    continue;
                }

                // A new state was added, generate the scripts
                // Create State from StateTemplate if it doesnt exist, otherwise continue
                #region CREATE STATE
                Debug.Log("[Generation] ...Generating State " + controller.states[i].name + "...");
                string generatedStateName = controller.states[i].name.Replace(" ", "") + "State";
                string generatedStateNamePath = statesFolderPath + generatedStateName + ".cs";
                if (File.Exists(generatedStateNamePath) == false)
                {
                    string stateTemplate = GetScriptTemplateContents(GenerateScripts.instance.stateTemplate);
                    stateTemplate = stateTemplate.Replace("SNAMESPACE_ENTRY", controller.namespaceName); // Namespace Name
                    stateTemplate = stateTemplate.Replace("SSTATE_INSTANCE_ENTRY", generatedStateName); // Class Name
                    stateTemplate = stateTemplate.Replace("BASE_SSTATE_ENTRY", baseStateControllerName); // Inherited Class Name
                    stateTemplate = stateTemplate.Replace("SSTATES_ENUM_ENTRY", enumName);  // Enum Name
                    File.WriteAllText(generatedStateNamePath, stateTemplate);
                    Debug.Log("[Generation] !Generated State " + controller.states[i].name + "!");
                }
                controller.generatedStates.Add(controller.states[i]);
                Debug.Log("[Generation] !State Already exists" + controller.states[i].name + "!");
                #endregion

            }


            await Task.Delay(1000);

            AssetDatabase.Refresh();
        }
    }
}