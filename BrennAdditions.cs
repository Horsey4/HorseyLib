namespace HorseyLib.PlayMakerUtilities
{
    public static class PlayMakerUtils
    {
        public static void InitializeFSM(this PlayMakerFSM pm)
        {
            try { pm.Fsm.InitData(); }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }

        /// <summary>
        /// adds a GlobalTransition to the PlayMakerFSM
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="eventName"></param>
        /// <param name="stateName"></param>
        public static void AddGlobalTransition(this PlayMakerFSM pm, string eventName, string stateName)
        {
            try
            {
                pm.Fsm.InitData();
                var gT = pm.Fsm.GlobalTransitions.ToList();
                gT.Add(new FsmTransition { FsmEvent = pm.FsmEvents.First(x => x.Name == eventName), ToState = stateName });
                pm.Fsm.GlobalTransitions = gT.ToArray();
                pm.Fsm.InitData();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        /// <summary>
        /// gets a GlobalTransition of the PlayMakerFSM
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static FsmTransition GetGlobalTransition(this PlayMakerFSM pm, string eventName)
        {
            try
            {
                pm.Fsm.InitData();
                var gT = pm.Fsm.GlobalTransitions.First(x => x.EventName == eventName);
                if (gT != null) return gT;
                else return null;
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); return null; }
        }

        public static void AddVariable(this PlayMakerFSM pm, FsmFloat fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.FloatVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.FloatVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmInt fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.IntVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.IntVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmBool fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.BoolVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.BoolVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmGameObject fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.GameObjectVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.GameObjectVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmString fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.StringVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.StringVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmVector2 fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.Vector2Variables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.Vector2Variables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmVector3 fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.Vector3Variables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.Vector3Variables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmColor fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.ColorVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.ColorVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmRect fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.RectVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.RectVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmMaterial fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.MaterialVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.MaterialVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmTexture fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.TextureVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.TextureVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmQuaternion fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.QuaternionVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.QuaternionVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        public static void AddVariable(this PlayMakerFSM pm, FsmObject fsmVariable)
        {
            try
            {
                var vars = pm.Fsm.Variables.ObjectVariables.ToList();
                vars.Add(fsmVariable);
                pm.Fsm.Variables.ObjectVariables = vars.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }

        public static T GetVariable<T>(this PlayMakerFSM pm, string ID) where T : new()
        {
            try
            {
                object var = null;
                pm.Fsm.InitData();
                switch (typeof(T))
                {
                    case Type t when t == typeof(FsmFloat): var = (object)pm.Fsm.Variables.FloatVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmInt): var = (object)pm.Fsm.Variables.IntVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmBool): var = (object)pm.Fsm.Variables.BoolVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmGameObject): var = (object)pm.Fsm.Variables.GameObjectVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmString): var = (object)pm.Fsm.Variables.StringVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmVector2): var = (object)pm.Fsm.Variables.Vector2Variables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmVector3): var = (object)pm.Fsm.Variables.Vector3Variables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmColor): var = (object)pm.Fsm.Variables.ColorVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmRect): var = (object)pm.Fsm.Variables.RectVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmMaterial): var = (object)pm.Fsm.Variables.MaterialVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmTexture): var = (object)pm.Fsm.Variables.TextureVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmQuaternion): var = (object)pm.Fsm.Variables.QuaternionVariables.First(x => x.Name == ID); break;
                    case Type t when t == typeof(FsmObject): var = (object)pm.Fsm.Variables.ObjectVariables.First(x => x.Name == ID); break;
                }
                return (T)var;
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); return new T(); }
        }

        /// <summary>
        /// adds an empty FsmState to the PlayMakerFSM
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public static FsmState AddState(this PlayMakerFSM pm, string stateName)
        {
            FsmState fs = null;

            try
            {
                pm.Fsm.InitData();
                var fss = pm.FsmStates.ToList();
                if (fss.First(x => x.Name == stateName) == null)
                {
                    fs = new FsmState(pm.Fsm);
                    fs.Name = stateName;
                    fss.Add(fs);
                }
                pm.Fsm.States = fss.ToArray();
                pm.Fsm.InitData();
                return fs;
            }
            catch (Exception ex)
            {
                ModConsole.Error(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// gets a FsmState from the PlayMakerFSM
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public static FsmState GetState(this PlayMakerFSM pm, string stateName)
        {
            try
            {
                pm.Fsm.InitData();
                return pm.FsmStates.First(x => x.Name == stateName);
            }
            catch (Exception ex)
            {
                ModConsole.Error(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// gets a FsmState from the PlayMakerFSM
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public static FsmState GetState(this PlayMakerFSM pm, int index)
        {
            try
            {
                pm.Fsm.InitData();
                return pm.FsmStates[index];
            }
            catch (Exception ex)
            {
                ModConsole.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// adds a FsmTransition to the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="eventName"></param>
        /// <param name="stateName"></param>
        public static void AddTransition(this FsmState fs, string eventName, string stateName)
        {
            try
            {
                fs.Fsm.InitData();
                var t = fs.Transitions.ToList();
                if (t.First(x => x.EventName == eventName) == null) t.Add(new FsmTransition { FsmEvent = fs.Fsm.Events.First(x => x.Name == eventName), ToState = stateName });
                else ModConsole.Error($"Transition for {eventName} in State {fs.Name} exists already!");
                fs.Fsm.GlobalTransitions = t.ToArray();
                fs.Fsm.InitData();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        /// <summary>
        /// gets a FsmTransition from the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static FsmTransition GetTransition(this FsmState fs, string eventName)
        {
            try
            {
                if (fs.Transitions.First(x => x.EventName == eventName) != null) return fs.Transitions.First(x => x.EventName == eventName);
                else { ModConsole.Error($"Transition for {eventName} in State {fs.Name} doesn't exist!"); return null; }
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); return null; }
        }

        /// <summary>
        /// adds a FsmStateAction to the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="action"></param>
        public static void AddAction(this FsmState fs, FsmStateAction action)
        {
            try
            {
                var a = fs.Actions.ToList();
                a.Add(action);
                fs.Actions = a.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
        /// <summary>
        /// adds multiple FsmStateActions to the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="actions">the actions you are adding</param>
        public static void AddActions(this FsmState fs, ICollection<FsmStateAction> actions) { for (int i = 0; i < actions.Count; i++) fs.AddAction(actions.ToArray()[i]); }
        /// <summary>
        /// adds multiple FsmStateActions to the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="actions">the actions you are adding</param>
        public static void AddActions(this FsmState fs, params FsmStateAction[] actions) { for (int i = 0; i < actions.Length; i++) fs.AddAction(actions.ToArray()[i]); }

        /// <summary>
        /// gets a FsmStateAction from the FsmState
        /// </summary>
        /// <typeparam name="T">the type of the FsmStateAction you are getting</typeparam>
        /// <param name="fs"></param>
        /// <param name="index">the index of the FsmStateAction you are getting</param>
        /// <returns>the FsmStateAction</returns>
        public static T GetAction<T>(this FsmState fs, int index) where T : class, new()
        {
            try { return (T)((object)fs.Actions[index]); }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); return new T(); }
        }

        /// <summary>
        /// removes the FsmStateAction at the defined index from the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="index"></param>
        public static void RemoveAction(this FsmState fs, int index)
        {
            try
            {
                var a = fs.Actions.ToList();
                a.RemoveAt(index);
                fs.Actions = a.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }

        /// <summary>
        /// replaces the FsmStateAction at the defined index in the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="index"></param>
        /// <param name="action"></param>
        public static void ReplaceAction(this FsmState fs, int index, FsmStateAction action)
        {
            try
            {
                var a = fs.Actions.ToList();
                fs.RemoveAction(index);
                a.Insert(index, action);
                fs.Actions = a.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }

        /// <summary>
        /// inserts a FsmStateAction at the set index in the FsmState
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="index"></param>
        /// <param name="action"></param>
        public static void InsertAction(this FsmState fs, int index, FsmStateAction action)
        {
            try
            {
                var a = fs.Actions.ToList();
                a.Insert(index, action);
                fs.Actions = a.ToArray();
            }
            catch (Exception ex) { ModConsole.Error(ex.ToString()); }
        }
    }
}
namespace HorseyLib
{
    public class WaitFor
    {
        public readonly static WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();
        public readonly static WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();
        static Dictionary<float, WaitForSeconds> s = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds Seconds(float seconds)
        {
            seconds = Mathf.Clamp(seconds, 0.01f, Mathf.Infinity);
            if (s.ContainsKey(seconds)) return s[seconds];
            else
            {
                var wfs = new WaitForSeconds(seconds);
                s.Add(seconds, wfs);
                return wfs;
            }
        }
    }
}