using UnityEngine;
using HutongGames.PlayMaker;

public static class HorseyLib
{
    #region variables
    internal static bool initialized;
    public const byte majorVersion = 2;
    public const byte minorVersion = 1;
    public static GameObject SATSUMA { get; private set; }
    public static GameObject CARPARTS { get; private set; }
    public static GameObject PLAYER { get; private set; }
    public static GameObject JOBS { get; private set; }
    public static GameObject MAP { get; private set; }
    public static GameObject TRAFFIC { get; private set; }
    public static GameObject YARD { get; private set; }
    public static GameObject PERAJARVI { get; private set; }
    public static GameObject RYKIPOHJA { get; private set; }
    public static GameObject REPAIRSHOP { get; private set; }
    public static GameObject INSPECTION { get; private set; }
    public static GameObject STORE { get; private set; }
    public static GameObject LANDFILL { get; private set; }
    public static GameObject DANCEHALL { get; private set; }
    public static GameObject COTTAGE { get; private set; }
    public static GameObject CABIN { get; private set; }
    public static GameObject SOCCER { get; private set; }
    public static GameObject RALLY { get; private set; }
    public static GameObject JAIL { get; private set; }
    public static GameObject GUI { get; private set; }
    public static GameObject Database { get; private set; }
    public static Camera FPSCamera { get; private set; }
    public static Drivetrain[] vehicles { get; private set; }
    public static FsmBool RatchetSwitch { get; private set; }
    public static float clockMinutes => _sunMinutes.Value % 60;
    public static int clockHours => (_sunHours.Value + (_sunMinutes.Value > 60 ? 1 : 0)) % 24;
    public static readonly FsmFloat Thirst = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst");
    public static readonly FsmFloat Hunger = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger");
    public static readonly FsmFloat Stress = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress");
    public static readonly FsmFloat Urine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine");
    public static readonly FsmFloat Fatigue = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue");
    public static readonly FsmFloat Dirtiness = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness");
    public static readonly FsmFloat Money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
    public static readonly FsmFloat ToolWrenchSize = FsmVariables.GlobalVariables.FindFsmFloat("ToolWrenchSize");
    public static readonly FsmInt GlobalDay = FsmVariables.GlobalVariables.FindFsmInt("GlobalDay");
    public static readonly FsmInt KeyFerndale = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyFerndale");
    public static readonly FsmInt KeyGifu = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyGifu");
    public static readonly FsmInt KeyHayosiko = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyHayosiko");
    public static readonly FsmInt KeyHome = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyHome");
    public static readonly FsmInt KeyRuscko = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeyRuscko");
    public static readonly FsmInt KeySatsuma = FsmVariables.GlobalVariables.FindFsmInt("PlayerKeySatsuma");
    public static readonly FsmBool GUIassemble = FsmVariables.GlobalVariables.FindFsmBool("GUIassemble");
    public static readonly FsmBool GUIbuy = FsmVariables.GlobalVariables.FindFsmBool("GUIbuy");
    public static readonly FsmBool GUIdisassemble = FsmVariables.GlobalVariables.FindFsmBool("GUIdisassemble");
    public static readonly FsmBool GUIdrive = FsmVariables.GlobalVariables.FindFsmBool("GUIdrive");
    public static readonly FsmBool GUIuse = FsmVariables.GlobalVariables.FindFsmBool("GUIuse");
    public static readonly FsmBool PlayerHandLeft = FsmVariables.GlobalVariables.FindFsmBool("PlayerHandLeft");
    public static readonly FsmBool PlayerHandRight = FsmVariables.GlobalVariables.FindFsmBool("PlayerHandRight");
    public static readonly FsmBool PlayerHasRatchet = FsmVariables.GlobalVariables.FindFsmBool("PlayerHasRatchet");
    public static readonly FsmBool PlayerHelmet = FsmVariables.GlobalVariables.FindFsmBool("PlayerHelmet");
    public static readonly FsmBool PlayerInMenu = FsmVariables.GlobalVariables.FindFsmBool("PlayerInMenu");
    public static readonly FsmBool PlayerSeated = FsmVariables.GlobalVariables.FindFsmBool("PlayerSeated");
    public static readonly FsmBool PlayerSleeps = FsmVariables.GlobalVariables.FindFsmBool("PlayerSleeps");
    public static readonly FsmBool PlayerStop = FsmVariables.GlobalVariables.FindFsmBool("PlayerStop");
    public static readonly FsmString GUIgear = FsmVariables.GlobalVariables.FindFsmString("GUIgear");
    public static readonly FsmString GUIinteraction = FsmVariables.GlobalVariables.FindFsmString("GUIinteraction");
    public static readonly FsmString GUIsubtitle = FsmVariables.GlobalVariables.FindFsmString("GUIsubtitle");
    public static readonly FsmString GameStartDate = FsmVariables.GlobalVariables.FindFsmString("GameStartDate");
    public static readonly FsmString PickedPart = FsmVariables.GlobalVariables.FindFsmString("PickedPart");
    public static readonly FsmString CurrentVehicle = FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle");
    public static readonly FsmString PlayerFirstName = FsmVariables.GlobalVariables.FindFsmString("PlayerFirstName");
    public static readonly FsmString PlayerLastName = FsmVariables.GlobalVariables.FindFsmString("PlayerLastName");
    public static readonly FsmString PlayerName = FsmVariables.GlobalVariables.FindFsmString("PlayerName");
    static FsmFloat _sunMinutes;
    static FsmInt _sunHours;
    #endregion

    /// <summary>Initializes variables, Call this OnLoad()</summary>
    public static void init()
    {
        if (initialized) return;
        initialized = true;

        SATSUMA = GameObject.Find("SATSUMA(557kg, 248)");
        CARPARTS = GameObject.Find("CARPARTS");
        PLAYER = GameObject.Find("PLAYER");
        JOBS = GameObject.Find("JOBS");
        MAP = GameObject.Find("MAP");
        TRAFFIC = GameObject.Find("TRAFFIC");
        YARD = GameObject.Find("YARD");
        PERAJARVI = GameObject.Find("PERAJARVI");
        RYKIPOHJA = GameObject.Find("RYKIPOHJA");
        REPAIRSHOP = GameObject.Find("REPAIRSHOP");
        INSPECTION = GameObject.Find("INSPECTION");
        STORE = GameObject.Find("STORE");
        LANDFILL = GameObject.Find("LANDFILL");
        DANCEHALL = GameObject.Find("DANCEHALL");
        COTTAGE = GameObject.Find("COTTAGE");
        CABIN = GameObject.Find("CABIN");
        SOCCER = GameObject.Find("SOCCER");
        RALLY = GameObject.Find("RALLY");
        JAIL = GameObject.Find("JAIL");
        GUI = GameObject.Find("GUI");
        Database = GameObject.Find("Database");
        FPSCamera = PLAYER.transform.Find("Pivot/AnimPivot/Camera/FPSCamera/FPSCamera").GetComponent<Camera>();
        vehicles = GameObject.FindObjectsOfType<Drivetrain>();
        RatchetSwitch = FPSCamera.transform.parent.Find("2Spanner/Pivot/Ratchet").GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("Switch");

        var sun = MAP.transform.Find("SUN/Pivot/SUN").GetComponent<PlayMakerFSM>().FsmVariables;
        _sunMinutes = sun.FindFsmFloat("Minutes");
        _sunHours = sun.FindFsmInt("Time");

        FPSCamera.gameObject.AddComponent<InteractableHandler>();

        var pm = FPSCamera.transform.parent.Find("2Spanner/Raycast").GetComponents<PlayMakerFSM>()[1];
        pm.Fsm.InitData();
        Bolt.highlightMaterial = ((HutongGames.PlayMaker.Actions.SetMaterial)pm.FsmStates[2].Actions[1]).material.Value;
        Bolt.boltMaterial = ((HutongGames.PlayMaker.Actions.SetMaterial)pm.FsmStates[4].Actions[0]).material.Value;
    }

    /// <summary>If the library is up to date with the expected version</summary>
    public static bool checkVersion(byte majorVersion, byte minorVersion) => HorseyLib.majorVersion == majorVersion && HorseyLib.minorVersion >= minorVersion;
}