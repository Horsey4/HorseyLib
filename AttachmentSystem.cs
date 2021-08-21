using System;
using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>An interactable part</summary>
[RequireComponent(typeof(Rigidbody))]
public class Part : Interactable
{
    public delegate void attachDelegate(int index);
    public delegate void detachDelegate(int index);

    public event attachDelegate onAttach;
    public event detachDelegate onDetach;
    public Collider[] parents;
    public Bolt[] bolts;
    public bool attached;
    Rigidbody rb;
    Collider cur;
    bool pParent;
    int index;
    public int tightness
    {
        get
        {
            if (bolts == null || bolts.Length == 0) return 0;
            return bolts.Select((Bolt x) => x.tightness).Sum();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (bolts != null && bolts.Length != 0)
        {
            for (int i = 0; i < bolts.Length; i++)
            {
                bolts[i]?.gameObject.SetActive(attached);
            }
        }
    }

    void Update()
    {
        if (!attached && cur && pParent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HorseyLib.GUIassemble.Value = false;
                attached = true;
                StartCoroutine(waitAttach(cur));
            }
            else HorseyLib.GUIassemble.Value = true;
        }
        pParent = transform.parent;
    }

    public override void mouseOver()
    {
        if (attached && tightness == 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                HorseyLib.GUIdisassemble.Value = false;
                attached = false;
                gameObject.layer = 19;
                rb.isKinematic = false;
                MasterAudio.PlaySound3DAtTransformAndForget("CarBuilding", transform, variationName: "disassemble");
                transform.parent.GetComponent<Collider>().enabled = true;
                transform.SetParent(null);
                activateBolts(false);

                onDetach?.Invoke(index);
            }
            else HorseyLib.GUIdisassemble.Value = true;
        }
    }

    public override void mouseExit()
    {
        if (attached && tightness == 0) HorseyLib.GUIdisassemble.Value = false;
    }

    void OnTriggerEnter(Collider col)
    {
        index = Array.IndexOf(parents, col);
        if (index != -1) cur = col;
    }

    void OnTriggerExit(Collider col)
    {
        if (cur == col)
        {
            HorseyLib.GUIassemble.Value = false;
            cur = null;
        }
    }

    void activateBolts(bool active)
    {
        if (bolts != null && bolts.Length != 0)
            for (int i = 0; i < bolts.Length; i++)
                bolts[i].gameObject.SetActive(active);
    }

    IEnumerator waitAttach(Collider col)
    {
        yield return null;
        MasterAudio.PlaySound3DAtTransformAndForget("CarBuilding", transform, variationName: "assemble");
        attach(col);

        onAttach?.Invoke(index);
    }

    /// <summary>Attaches to a collider programmatically</summary>
    /// <remarks>Does not invoke events or play a sound</remarks>
    public void attach(Collider col)
    {
        if (!rb) rb = GetComponent<Rigidbody>();

        gameObject.layer = 16;
        rb.isKinematic = true;
        transform.SetParent(col.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        col.enabled = false;
        activateBolts(true);
    }
}

/// <summary>An interactable bolt</summary>
public class Bolt : Interactable
{
    public enum Size
    {
        Screw = 65,
        Sparkplug = 55,
        _5mm = 50,
        _6mm = 60,
        _7mm = 70,
        _8mm = 80,
        _9mm = 90,
        _10mm = 100,
        _11mm = 110,
        _12mm = 120,
        _13mm = 130,
        _14mm = 140,
        _15mm = 150,
    }
    public delegate void tightenDelagate(int tightness);
    public delegate void loosenDelegate(int tightness);

    internal static Material highlightMaterial;
    internal static Material boltMaterial;
    static WaitForSeconds waitRatchet = new WaitForSeconds(0.08f);
    static WaitForSeconds waitSpanner = new WaitForSeconds(0.28f);
    public event tightenDelagate onTighten;
    public event loosenDelegate onLoosen;
    public Size size;
    public Vector3 stepRotation = new Vector3(0, 0, 45);
    public Vector3 stepPosition = new Vector3(0, 0, -0.0025f);
    public int tightness;
    public int steps = 8;
    public bool canUseRatchet = true;
    Renderer renderer;
    bool scrollable;
    bool onCooldown;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = boltMaterial;

        transform.localPosition -= stepPosition * (steps - tightness);
        transform.localEulerAngles -= stepRotation * (steps - tightness);
    }

    public override void mouseEnter()
    {
        if (Mathf.RoundToInt(HorseyLib.ToolWrenchSize.Value * 100) == (int)size && (!HorseyLib.PlayerHasRatchet.Value || canUseRatchet))
        {
            scrollable = true;
            renderer.material = highlightMaterial;
        }
    }

    public override void mouseOver()
    {
        if (scrollable && HorseyLib.ToolWrenchSize.Value == 0) mouseExit();
    }

    public override void mouseExit()
    {
        scrollable = false;
        renderer.material = boltMaterial;
    }

    public override void scrollUp()
    {
        if (!scrollable || onCooldown) return;
        if (HorseyLib.PlayerHasRatchet.Value)
        {
            if (!canUseRatchet) return;
            if (!HorseyLib.RatchetSwitch.Value)
            {
                scrollDown();
                return;
            }
        }
        if (tightness < steps)
        {
            MasterAudio.PlaySound3DAtTransformAndForget("CarBuilding", transform, variationName: "bolt_screw");
            tightness++;
            transform.localPosition += stepPosition;
            transform.localEulerAngles += stepRotation;
            StartCoroutine(waitCooldown());

            onTighten?.Invoke(tightness);
        }
    }

    public override void scrollDown()
    {
        if (!scrollable || onCooldown) return;
        if (HorseyLib.PlayerHasRatchet.Value && HorseyLib.RatchetSwitch.Value)
        {
            if (!canUseRatchet) return;
            if (HorseyLib.RatchetSwitch.Value)
            {
                scrollUp();
                return;
            }
        }
        if (tightness != 0)
        {
            MasterAudio.PlaySound3DAtTransformAndForget("CarBuilding", transform, variationName: "bolt_screw");
            tightness--;
            transform.localPosition -= stepPosition;
            transform.localEulerAngles -= stepRotation;
            StartCoroutine(waitCooldown());

            onLoosen?.Invoke(tightness);
        }
    }

    IEnumerator waitCooldown()
    {
        onCooldown = true;
        yield return HorseyLib.PlayerHasRatchet.Value ? waitRatchet : waitSpanner;
        onCooldown = false;
    }

    /// <summary>Tightens a bolt programmatically</summary>
    /// <remarks>Does not invoke events or play a sound</remarks>
    public void setTightness(int tightness)
    {
        this.tightness = tightness;

        transform.localPosition += stepPosition * (tightness - this.tightness);
        transform.localEulerAngles += stepRotation * (tightness - this.tightness);
    }
}