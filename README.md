# S-StateMachines

S-StateMachines is a tool to enhance your workflow when creating State Machines in C# and Unity.

Navigate to the Package folder to view the asset itself. S-StateMachines/S-StateMachines/Packages/com.slibrary.sstatemachines/
This repository is acting as a container for the sample project with the S-StateMachines asset as a package.

## Code Generation
- Create a root folder in the path of the ScriptableStateController named, <INSERTNAME>Scripts'.  
- Create folders within that called, 'Base' and 'States'.  
- Always create a Base<INSERTNAME>Controller from the BaseControllerTemplate inside of the 'Base' folder.
- If this is the first time generating, Create a <INSERTNAME>Controller from the ControllerTemplate, in root folder.
- If this is the first time generating, For every state create a <INSERTNAME>State from the StateTemplate.
  
During re-generation the only scripts that will be regenerated is the BaseController. Your Controller and States are intended to be customized with your code.

The asset supports refactoring for,
- Renaming the state machine
- Renaming the namespace
- Renaming/Deleting/Adding/Changing default states

## How to create a Scriptable State Machine
To create a Scriptable State Machine, select Create/SLibrary/Scriptable State Object. The name assigned to this file is the name of the state machine.

Select the scriptable object and populate it with the states and specify the namespace for this machine.

Select the refresh icon to get the list of new changes, and then select the checkmark to apply your changes. 

The Undo button will not undo once you've applied your changes. Use source control to ensure no loss of state scripts if deleting.

A default state does NOT need to be specified, if no default state is specified it will begin in the **None** state.

## What Scripts should I interact with?
The Controller script and all state scripts are all interactable. The BaseController script is the only script that should not be used as it is regenerated every time.

## Example State Machine
This example named StateMachine, has two states, StateA and StateB, with no default state. It is within the namespace Example.States.

![Example](http://samuelarminana.com/u/15487c064-fd6d-4f65-8ce1-0aa7666ae706.png)

### Process going from State A to State B
The CanEnter method on the current state, will either accept or deny the state change, if its accepted the OnExit method will be called for the current state, followed by OnEnter on the next state which will now become the current state.

# Dependencies
This asset is dependent on [Odin Inspector](https://odininspector.com/), a premium Unity Asset which allows you to customize the editor/insepctor as well as provide a serialization library. 

Because this is a premium asset it is excluded from this source code. Please purchase and install the asset to use it.


# Sample Project
![1b9d2d556-a29f-4d63-80cf-b9af8c3fb521](https://user-images.githubusercontent.com/79631955/111406659-11227d00-86a9-11eb-8df5-970b5fed6c38.gif)

This example is to give you an idea of how you can drive your game content using state machines. This example project comes with a few other features used commonly in my workflow,
- Bindable (Used to bind the lives and score values to a refresh delegate used by the UI to trigger an update)
- InstancedScriptableObject (Creates instances of singleton scriptable objects, used to manage the GameProperties, consisting of any configurations or references used in the game)
- Model-View-Controller UI (Based on my original implementation here https://samuelarminana.com/index.php/2019/02/11/mvc-in-unitys-ui/)

The game example is powered by only two state machines, Game State Machine and Character State Machine, shown here,

![image](https://user-images.githubusercontent.com/79631955/111406933-7b3b2200-86a9-11eb-982e-c5a17af48550.png)
![image](https://user-images.githubusercontent.com/79631955/111406992-9312a600-86a9-11eb-8e48-2184f5d4c315.png)
