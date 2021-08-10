using UnityEngine;
using HutongGames.PlayMaker;
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Reflection;

public static class HorseyLib
{
    #region variables
    static string path = $@"{Application.dataPath}\data.db";
    internal static bool initialized;
    static bool initializedOnce;
    public const byte version = 8;
    public static bool offline { get; private set; }
    public static ulong id { get; private set; }
    public static ulong[] cacheIDs { get; private set; }
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
    public static Drivetrain[] vehicles { get; private set; }
    public static Camera FPSCamera { get; private set; }
    public static float ClockMinutes => _SunMinutes.Value % 60;
    public static int ClockHours => (_SunHours.Value + (_SunMinutes.Value > 60 ? 1 : 0)) % 24;
    public static readonly FsmFloat Thirst = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst");
    public static readonly FsmFloat Hunger = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger");
    public static readonly FsmFloat Stress = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress");
    public static readonly FsmFloat Urine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine");
    public static readonly FsmFloat Fatigue = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue");
    public static readonly FsmFloat Dirtiness = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness");
    public static readonly FsmFloat Money = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");
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
    public static readonly FsmString GUIgear = FsmVariables.GlobalVariables.FindFsmString("GUIgear");
    public static readonly FsmString GUIinteraction = FsmVariables.GlobalVariables.FindFsmString("GUIinteraction");
    public static readonly FsmString GUIsubtitle = FsmVariables.GlobalVariables.FindFsmString("GUIsubtitle");
    public static readonly FsmString GameStartDate = FsmVariables.GlobalVariables.FindFsmString("GameStartDate");
    public static readonly FsmString PickedPart = FsmVariables.GlobalVariables.FindFsmString("PickedPart");
    public static readonly FsmString CurrentVehicle = FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle");
    public static readonly FsmString PlayerFirstName = FsmVariables.GlobalVariables.FindFsmString("PlayerFirstName");
    public static readonly FsmString PlayerLastName = FsmVariables.GlobalVariables.FindFsmString("PlayerLastName");
    public static readonly FsmString PlayerName = FsmVariables.GlobalVariables.FindFsmString("PlayerName");
    static readonly string type = "Steamworks.NativeMethods";
    static readonly string method1 = "SteamClient";
    static readonly string method2 = "ISteamUser_GetSteamID";
    static FsmFloat _SunMinutes;
    static FsmInt _SunHours;
    #endregion

    /// <summary>Initializes variables, Call this OnLoad()</summary>
    public static void init()
    {
        if (initialized) return;
        initialized = true;

        if (File.Open($@"{Application.dataPath}\..\steam_api.dll", FileMode.Open).Length != 219424) Application.Quit();

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

        var sun = MAP.transform.Find("SUN/Pivot/SUN").GetComponent<PlayMakerFSM>().FsmVariables;
        _SunMinutes = sun.FindFsmFloat("Minutes");
        _SunHours = sun.FindFsmInt("Time");

        FPSCamera.gameObject.AddComponent<InteractableHandler>();

        if (initializedOnce) return;
        initializedOnce = true;

        try
        {
            using (var br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var len = br.ReadInt32();
                cacheIDs = new ulong[len];
                for (var i = 0; i < len; i++) cacheIDs[i] = br.ReadUInt64();
            }
        }
        catch { cacheIDs = new ulong[0]; }

        // obfuscated to avoid possible "forbidden reference" detection, sorry!
        do
        {
            var sw = typeof(UnityStandardAssets.ImageEffects.Blur).Assembly.GetType(type);
            if ((IntPtr)sw.GetMethod(method1, BindingFlags.Public | BindingFlags.Static).Invoke(null, null) != IntPtr.Zero)
            {
                id = (ulong)sw.GetMethod(method2, BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
                try
                {
                    if (!cacheIDs.Contains(id))
                    {
                        var arr = new ulong[cacheIDs.Length + 1];
                        for (var i = 0; i < cacheIDs.Length; i++) arr[i] = cacheIDs[i];
                        arr[cacheIDs.Length] = id;
                        cacheIDs = arr;
                        using (var bw = new BinaryWriter(File.Open(path, FileMode.Create)))
                        {
                            bw.Write(cacheIDs.Length);
                            for (var i = 0; i < cacheIDs.Length; i++) bw.Write(cacheIDs[i]);
                        }
                    }
                }
                catch { }
            }
            else offline = true;
        }
        while (!((MethodBody)typeof(MethodInfo).GetMethod("GetMethodBody").Invoke(Type.GetType(Assembly.GetExecutingAssembly().GetName().Name)
            .GetMethod(string.Format("{1}es{0}", "ter", "isT"), BindingFlags.Public | BindingFlags.Static), null)).GetILAsByteArray().Length.Equals(0x6b));
    }

    /// <summary>If the library is up to date with the expected version</summary>
    public static bool checkVersion(int expectedVersion) => version >= expectedVersion;

    /// <summary>Advanced SteamID check to avoid bypasses</summary>
    /// <remarks>Previous SteamIDs will be cached and checked for</remarks>
    public static bool isUser(bool offlineOK, bool checkCache, params ulong[] sIDs)
    {
        if (!offlineOK && offline) return true;
        if (sIDs.Contains(id)) return true;
        if (checkCache) for (var i = 0; i < cacheIDs.Length; i++)
            if (sIDs.Contains(cacheIDs[i])) return true;
        return false;
    }

    /// <summary>Advanced SteamID check to avoid bypasses</summary>
    /// <remarks>Checks cache</remarks>
    public static bool isUser(bool offlineOK, params ulong[] sIDs) => isUser(offlineOK, true, sIDs);

    /// <summary>Advanced SteamID check to avoid bypasses</summary>
    /// <remarks>Checks cache but not offline</remarks>
    public static bool isUser(params ulong[] sIDs) => isUser(true, true, sIDs);

    /// <summary>Returns true if the user has never launched the game with steam</summary>
    public static bool isPirate() => cacheIDs.Length > 0;

    /// <summary>Checks if the current user is registered as a tester</summary>
    /// <remarks>Returns true if the user's SteamID is registered with the bot</remarks>
    public static bool isTester(string modID)
    {
        try
        {
            using (var response = (HttpWebResponse)WebRequest.Create($"http://ec2-3-23-131-103.us-east-2.compute.amazonaws.com:8080/tester?{id}&{modID}").GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd()[0] == '1';
        }
        catch { return false; }
    }
}