<i>Please don't include these files with your mods! instead, link the [releases page](https://github.com/Horsey4/HorseyLib/releases) in required mods avoid clashes in versions</i>

# HorseyLib
<h2>Contains all helper methods & properties</h2>

<br>

Name | Type | Path
-|-|-
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
RatchetSwitch | FsmBool | `PLAYER/Pivot/AnimPivot/Camera/FPSCamera/FPSCamera/2Spanner/Pivot/Ratchet: Switch`
ClockMinutes | float | `MAP/SUN/Pivot/SUN: Minutes`
ClockHours | int | `MAP/SUN/Pivot/SUN: Time`
Thirst | FsmFloat | `Globals/PlayerThirst`
Hunger | FsmFloat | `Globals/PlayerHunger`
Stress | FsmFloat | `Globals/PlayerStress`
Urine | FsmFloat | `Globals/PlayerUrine`
Fatigue | FsmFloat | `Globals/PlayerFatigue`
Dirtiness | FsmFloat | `Globals/PlayerDirtiness`
Money | FsmFloat | `Globals/PlayerMoney`
ToolWrenchSize | FsmFloat | `Globals/ToolWrenchSize`
GlobalDay | FsmInt | `Globals/GlobalDay`
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
PlayerHandLeft | FsmBool | `Globals/PlayerHandLeft`
PlayerHandRight | FsmBool | `Globals/PlayerHandRight`
PlayerHasRatchet | FsmBool | `Globals/PlayerHasRatchet`
PlayerHelmet | FsmBool | `Globals/PlayerHelmet`
PlayerInMenu | FsmBool | `Globals/PlayerInMenu`
PlayerSeated | FsmBool | `Globals/PlayerSeated`
PlayerSleeps | FsmBool | `Globals/PlayerSleeps`
PlayerStop | FsmBool | `Globals/PlayerStop`
GUIgear | FsmString | `Globals/GUIgear`
GUIinteraction | FsmString | `Globals/GUIinteraction`
GUIsubtitle | FsmString | `Globals/GUIsubtitle`
GameStartDate | FsmString | `Globals/GameStartDate`
PickedPart | FsmString | `Globals/PickedPart`
CurrentVehicle | FsmString | `Globals/PlayerCurrentVehicle`
PlayerFirstName | FsmString | `Globals/PlayerFirstName`
PlayerLastName | FsmString | `Globals/PlayerLastName`
PlayerName | FsmString | `Globals/PlayerName`

<br>

Name | Returns | Params | Summary
-|-|-|-
init | void | None | Initializes variables, Call this OnLoad()
checkVersion | bool | `byte majorVersion, byte minorVersion` | If the library is up to date with the expected version

<br>

# Interactable
<h2>A class to make interaction easier</h2>

<i>All methods are only called at a max distance of 1 meter away just like the game does</i>

<br>

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

<h3>Example</h3>

<br>

```cs
class InteractableExample : Interactable
{
	public override void mouseEnter() => HorseyLib.GUIuse.value = true;

	public override void lClick() => ModConsole.Print("Interactable clicked");

	public override void mouseExit() => HorseyLib.GUIuse.value = false;
}
```

<br>

# Attachment System
<h2>Part</h2>

<br>

Name | Type | Summary
-|-|-
onAttach | attachDelegate | Called with the index the part is attached to
onDetach | detachDelegate | Called with the index the part is detached from
parents | Collider[] | A list of colliders the part can attach to
bolts | Bolt[] | A list of bolts the part requires
attached | bool | If the part is attached or not
tightness | int | The total tightness of all bolts
attach | void | Attaches to a collider programmatically

Remarks:
- Requires a rigidbody component
- Collider parent is disabled when parented
- Bolts are enabled and disabled when attached and detached
- Colliders in the Unity Editor can only be referenced if under the same parent

<h2>Bolt</h2>

<br>

Name | Type | Summary
-|-|-
onTighten | tightenDelagate | Called with the tightness when the bolt is tightened
onLoosen | loosenDelegate | Called with the tightness when the bolt is loosened
size | enum | The size of the bolt
stepRotation | Vector3 | How much the bolt is turned each step
stepPosition | Vector3 | How much the bolt is moved each step
tightness | int | The current tightness of the bolt
steps | int | The total steps the bolt has
canUseRatchet | bool | If the ratched works for this bolt or not

Remarks:
- The bolt is assumed to be fully in when set up and backed out to the set tightness
- The bolt material is applied automatically and isn't needed in the assets

<br>

# SaveBytes
<h2>Saves classes with BinaryFormatters</h2>

<br>

Can save any class marked as `System.Serializable`, or any surrogates added to the list. The default list contains:
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
addSurrogate | void | `Type type, ISerializationSurrogate surrogate` | Adds a serialization surrogate to the list

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