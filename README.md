# S-StateMachines

S-StateMachines is a tool and workflow for State Machines in C# and Unity.

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
- Renaming/Deleting/Adding states

## How to create a Scriptable State Machine
To create a Scriptable State Machine, select Create/SLibrary/Scriptable State Object.

Select the 

## Example State Machine
![Example](http://samuelarminana.com/u/10cf2f31e-c5cb-44cd-b140-0fb14f19a308.png)

### Process going from State A to State B
![Flow](http://samuelarminana.com/u/1bb192819-345f-4bce-b206-84116683e9d8.png)

# Dependencies
This asset is dependent on [Odin Inspector](https://odininspector.com/), a premium Unity Asset which allows you to customize the editor/insepctor as well as provide a serialization library. 


# Example Project
This example project is built to give you an example of how you can drive your game content using state machines.

The Scenes in this project are,
- Game (Empty scene that is initialized with all the prefabs defined in our ReferenceManager scriptable object)
- Level1 (A scene with a Main Camera, SpawnPoint, and Platforms)

Three state machines are used in this project
- Core Game
  - Initialize
  - Splash Screen
  - Main Menu
  - In-Game
- Main Menu
  - Play
  - Settings
  - Quit
- Player Controller
  - Walk
  - Sprint
  - Jump
  - Falling
