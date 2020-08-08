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

![Flow](http://samuelarminana.com/u/1bb192819-345f-4bce-b206-84116683e9d8.png)

# Dependencies
This asset is dependent on [Odin Inspector](https://odininspector.com/), a premium Unity Asset which allows you to customize the editor/insepctor as well as provide a serialization library. 

Because this is a premium asset it is excluded from this source code. Please purchase and install the asset to use it.


# Example Uses
This example is to give you an idea of how you can drive your game content using state machines.

The Scenes,
- Game (Empty scene that is initialized with all the prefabs defined in our ReferenceManager scriptable object)
- Level1 (A scene with a Main Camera, SpawnPoint, and Platforms)

Three state machines,
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

I am working on implementing this exact layout in my current game and will provide an example project when possible.
