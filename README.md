<i>Please don't include these files with your mods! instead, link the [releases page](https://github.com/Horsey4/HorseyLib/releases) in required mods avoid clashes in versions</i>

# HorseyLib
### Contains all helper methods & properties

<br>

Name | Type | Path
-|-|-
offline | bool | `Steamworks.NativeMethods.SteamClient() == null`
id | ulong | `Steamworks.NativeMethods.ISteamUser_GetSteamID()`
cacheIDs | ulong[] | Past SteamIDs
SATSUMA | GameObject | `SATSUMA(557kg, 248)`
CARPARTS | GameObject | `CARPARTS`
PLAYER | GameObject | `PLAYER`
JOBS | GameObject | `JOBS`
MAP | GameObject | `MAP`
TRAFFIC | GameObject | `TRAFFIC`
YARD | GameObject | `YARD`
PERAJARVI | GameObject | `PERAJARVI`
RYKIPOHJA | GameObject | `RYKIPOHJA`
REPAIRSHOP | GameObject | `REPAIRSHOP`
INSPECTION | GameObject | `INSPECTION`
STORE | GameObject | `STORE`
LANDFILL | GameObject | `LANDFILL`
DANCEHALL | GameObject | `DANCEHALL`
COTTAGE | GameObject | `COTTAGE`
CABIN | GameObject | `CABIN`
SOCCER | GameObject | `SOCCER`
RALLY | GameObject | `RALLY`
JAIL | GameObject | `JAIL`
Database | GameObject | `Database`
FPSCamera | Camera | `PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera`
vehicles | Drivetrain[] | `Object.FindObjectsOfType<Drivetrain>()`
Thirst | FsmFloat | `Globals/PlayerThirst`
Hunger | FsmFloat | `Globals/PlayerHunger`
Stress | FsmFloat | `Globals/PlayerStress`
Urine | FsmFloat | `Globals/PlayerUrine`
Fatigue | FsmFloat | `Globals/PlayerFatigue`
Dirtiness | FsmFloat | `Globals/PlayerDirtiness`
Money | FsmFloat | `Globals/PlayerMoney`
KeyFerndale | FsmInt | `Globals/PlayerKeyFerndale`
KeyGifu | FsmInt | `Globals/PlayerKeyGifu`
KeyHayosiko | FsmInt | `Globals/PlayerKeyHayosiko`
KeyHome | FsmInt | `Globals/PlayerKeyHome`
KeyRuscko | FsmInt | `Globals/PlayerKeyRuscko`
KeySatsuma | FsmInt | `Globals/PlayerKeySatsuma`
KeyFerndale | FsmInt | `Globals/PlayerKeyFerndale`
GUIassemble | FsmBool | `Globals/GUIassemble`
GUIbuy | FsmBool | `Globals/GUIbuy`
GUIdisassemble | FsmBool | `Globals/GUIdisassemble`
GUIdrive | FsmBool | `Globals/GUIdrive`
GUIuse | FsmBool | `Globals/GUIuse`
GUIgear | FsmString | `Globals/GUIgear`
GUIinteraction | FsmString | `Globals/GUIinteraction`
GUIsubtitle | FsmString | `Globals/GUIsubtitle`
GameStartDate | FsmString | `Globals/GameStartDate`
PickedPart | FsmString | `Globals/PickedPart`
CurrentVehicle | FsmString | `Globals/PlayerCurrentVehicle`
PlayerFirstName | FsmString | `Globals/PlayerFirstName`
PlayerLastName | FsmString | `Globals/PlayerLastName`
PlayerName | FsmString | `Globals/PlayerName`
ClockMinutes | float | `MAP/SUN/Pivot/SUN: Minutes`
ClockHours | int | `MAP/SUN/Pivot/SUN: Time`

<br>

Name | Returns | Params | Summary
-|-|-|-
init | void | None | Initializes variables, Call this OnLoad()
checkVersion | bool | `int expectedVersion` | If the library is up to date with the expected version
isUser | bool | `bool offlineOK, checkCache, params ulong[] steamIDs` | Advanced steamID check to avoid bypasses
isUser | bool | `bool offlineOK, params ulong[] steamIDs` | Checks cache
isUser | bool | `params ulong[] steamIDs` | Checks cache but not offline
isPirate | bool | None | Returns true if the user has never launched the game with steam	
isTester | bool | `Mod mod` | Returns true if the user's SteamID is registered with the bot

<br>

# Interactable
### A class to make interaction easier

<br>
Add component to gameobject for code to be ran
<i>All methods are only called at a max distance of 1 meter away just like the game does</i>

Name | Called
-|-
mouseEnter() | Called when the player's cursor first moves over the object
mouseOver | Called when the player's cursor is over the object
mouseExit | Called when the player's cursor is exits object
scrollUp | Called when the player scrolls up and moused over
scrollDown | Called when the player scrolls down and moused over
use | Called when the player presses the use key and moused over
lClick | Called when the player presses the left mouse button moused over
lHold | Called when the player holds the left mouse button moused over
lRelease | Called when the player releases the left mouse button or mouses off
rClick | Called when the player presses the right mouse button moused over
rHold | Called when the player holds the right mouse button moused over
rRelease | Called when the player releases the right mouse button or mouses off

<br>

## Example

```cs
class InteractableExample : Interactable
{
	public override void mouseEnter() => HorseyLib.GUIuse.value = true;

	public override void lClick() => ModConsole.Print("Interactable clicked");

	public override void mouseExit() => HorseyLib.GUIuse.value = false;
}
```

## Subclasses
Name | Functionality
-|-
Useable | Sets GUIuse to true when the mouse is over

<br>

# SaveBytes
### Saves classes with BinaryFormatters

<br>

Can save any class marked as `System.Serializable`, or any of the exceptions below
- UnityEngine.Color
- UnityEngine.Quaternion
- UnityEngine.Vector2
- UnityEngine.Vector3
- UnityEngine.Vector4

<br>

Name | Returns | Params | Summary
-|-|-|-
save | void | `string saveFile, params object[] data` | Save a list of data to the save file
save | void | `string saveFile, object data` | Save data to the save file
load | object[] | `string saveFile, object[] ifFail` | Load and return a list of data from the save file
load\<T> | T | `string saveFile, T ifFail` | Load and return data from the save file

<br>

## Example

```cs
SaveBytes.save(saveFile,
	myColor,
	myQuaternion,
	myVector2,
	myVector3,
	myVector4
);

var data = SaveBytes.load(saveFile);
if (data == null) return; // failed to load save

myColor = (Color)data[0];
myQuaternion = (Quaternion)data[1];
myVector2 = (Vector2)data[2];
myVector3 = (Vector3)data[3];
myVector4 = (Vector4)data[4];
```
