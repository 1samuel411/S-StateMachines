using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SLibrary.StateMachines.ScriptableController
{
    [CreateAssetMenu(fileName = "New Scriptable State Controller", menuName = "SLibrary/Scriptable State Controller", order = 0)]
    public class ScriptableStateController : ScriptableObject
    {

        [System.Serializable]
        public struct StateEntry
        {
            [HideInInspector]
            public ScriptableStateController controller;

            [ReadOnly, HideLabel, HorizontalGroup(GroupID = "Horizontal", Width = 12)]
            public int id;

            [HideInInspector]
            public bool defaultState;

            [HideLabel, HorizontalGroup(GroupID = "Horizontal")]
            public string name;

            // Make Set Default Button
            [Button(Name = "Set Default"), HideIf("defaultState"), HorizontalGroup(GroupID = "Horizontal", Width = 80.5f)]
            void SetDefault()
            {
                for (int i = 0; i < controller.states.Count; i++)
                {
                    StateEntry state = controller.states[i];
                    state.defaultState = false;
                    controller.states[i] = state;
                }
                defaultState = true;
            }

            // Make Default Button
            [Button(Name = "Default"), ShowIf("defaultState"), HorizontalGroup(GroupID = "Horizontal", Width = 80.5f)]
            void DisableDefault()
            {
                defaultState = false;
            }
        }

        [HideInInspector]
        public string cachedName = "";
        [HideInInspector]
        public string cachedNamespace = "";
        [HideInInspector]
        public string cachedDefaultState = "";
        [InfoBox("$changes", InfoMessageType.Info)]
        public string namespaceName = "Example.States";

        [ListDrawerSettings(CustomAddFunction = "AddNewState")]
        public List<StateEntry> states = new List<StateEntry>();
        [HideInInspector]
        public List<StateEntry> generatedStates = new List<StateEntry>();

        private string changes;
        private bool saving;

        private void OnValidate()
        {
            ViewChanges();
        }
        //: "↺",  ✓
        [Button(Name = "↺"), HorizontalGroup(GroupID = "Main", Width = 22.5f)]
        public void ViewChanges()
        {
            // Reset changes
            changes = "";

            if (cachedName == "")
            {
                changes += "Created " + name + "\n";
            }
            else
            {
                if (cachedName != name)
                {
                    changes += "Renamed Controller, " + cachedName + ", to: " + name + "\n";
                }
            }

            // Namespace changed
            if (cachedNamespace != namespaceName)
            {
                if (cachedNamespace == "")
                    changes += "Set namespace to: " + namespaceName + "\n";
                else
                    changes += "Changed namespace from:" + cachedNamespace + ", to: " + namespaceName + "\n";
            }

            // Default state changed
            string defaultState = "None";
            if (states.Exists(x => x.defaultState))
                defaultState = states.First(x => x.defaultState).name.Replace(" ", "");
            if (defaultState != cachedDefaultState)
            {
                if (cachedDefaultState == "")
                    changes += "Set Default State to: " + defaultState + "\n";
                else
                    changes += "Changed Default State from: " + cachedDefaultState + ", to: " + defaultState + "\n";
            }

            // Get removed states
            List<int> removedStates = generatedStates.Select(x => x.id).Except(states.Select(x => x.id)).ToList();
            for (int i = 0; i < removedStates.Count; i++)
            {
                changes += ("Removed state: " + generatedStates.First(x => x.id == removedStates[i]).name) + "\n";
            }
            for (int i = 0; i < states.Count; i++)
            {
                int generatedStateIndex = generatedStates.FindIndex(x => x.id == states[i].id);
                if (generatedStateIndex >= 0)
                {
                    if (generatedStates[generatedStateIndex].name != states[i].name)
                    {
                        changes += ("Updated Name: " + generatedStates[generatedStateIndex].name + " to, " + states[i].name) + "\n";
                    }
                    if (generatedStates[generatedStateIndex].defaultState != states[i].defaultState)
                    {
                        changes += ("Updated Default State: " + states[i].name + " to, " + states[i].defaultState) + "\n";
                    }
                    continue;
                }
                changes += ("Added new state: " + states[i].name) + "\n";
            }
        }

        [Button(Name = "$SaveStatus"), ShowIf("ShowSave"), HorizontalGroup(GroupID = "Main", Width = 22.5f)]
        public async void SaveChanges()
        {
            saving = true;

            await GenerateScripts.RegenerateScripts(this);
            saving = false;
            AssetDatabase.Refresh();
            // Reset changes
            changes = "";
        }


        [Button(Name = "Undo"), ShowIf("$ShowSave"), HorizontalGroup(GroupID = "Main", Width = 60)]
        public void Undo()
        {
            states = generatedStates.ToList();
            this.name = cachedName;
            namespaceName = cachedNamespace;
            string defaultState = "None";
            if (states.Exists(x => x.defaultState))
                defaultState = states.First(x => x.defaultState).name.Replace(" ", "");
            this.cachedDefaultState = defaultState;
            ViewChanges();
        }

        bool ShowSave()
        {
            return changes != "";
        }

        string SaveStatus()
        {
            return saving ? "…" : "✓";
        }
        StateEntry AddNewState()
        {
            StateEntry entry = new StateEntry();
            if (states.Count <= 0)
            {
                entry.defaultState = true;
            }

            entry.controller = this;
            entry.name = "State" + (states.Count + 1);
            entry.id = states.Count + 1;
            while (states.Exists(x => x.id == entry.id) || generatedStates.Exists(x => x.id == entry.id))
                entry.id++;
            return entry;
        }
    }
}