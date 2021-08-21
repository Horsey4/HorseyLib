using UnityEngine;
using System.Collections.Generic;

/// <summary>A class to make interaction easier</summary>
[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    [HideInInspector] public bool mouseEntered;
    [HideInInspector] public bool lClicked;
    [HideInInspector] public bool rClicked;
    public LayerMask mask = -1;

    /// <summary>If you use a custom Awake() method call this within it</summary>
    public void Awake()
    {
        if (!InteractableHandler.masks.Contains(mask)) InteractableHandler.masks.Add(mask);
    }

    /// <summary>Called when the player's cursor first moves over the object</summary>
    public virtual void mouseEnter() { }

    /// <summary>Called when the player's cursor is over the object</summary>
    public virtual void mouseOver() { }

    /// <summary>Called when the player's cursor is exits object</summary>
    public virtual void mouseExit() { }

    /// <summary>Called when the player scrolls up and moused over</summary>
    public virtual void scrollUp() { }

    /// <summary>Called when the player scrolls down and moused over</summary>
    public virtual void scrollDown() { }

    /// <summary>Called when the player presses the use key and moused over</summary>
    public virtual void use() { }

    /// <summary>Called when the player presses the left mouse button and moused over</summary>
    public virtual void lClick() { }

    /// <summary>Called when the player holds the left mouse button and moused over</summary>
    public virtual void lHold() { }

    /// <summary>Called when the player releases the left mouse button or mouses off</summary>
    public virtual void lRelease() { }

    /// <summary>Called when the player presses the right mouse button and moused over</summary>
    public virtual void rClick() { }

    /// <summary>Called when the player holds the right mouse button and moused over</summary>
    public virtual void rHold() { }

    /// <summary>Called when the player releases the right mouse button or mouses off</summary>
    public virtual void rRelease() { }
}

/// <summary>An interactable that sets GUIuse when hovered over</summary>
public abstract class Useable : Interactable
{
    public override void mouseOver() => HorseyLib.GUIuse.Value = true;

    public override void mouseExit() => HorseyLib.GUIuse.Value = false;
}

class InteractableHandler : MonoBehaviour
{
    static internal List<int> masks = new List<int>();
    Interactable[] last;
    Interactable[] obj;
    RaycastHit hit;
    Ray ray;
    int i;

    void Start()
    {
        last = new Interactable[masks.Count];
        obj = new Interactable[masks.Count];
    }

    void Update()
    {
        ray = HorseyLib.FPSCamera.ScreenPointToRay(Input.mousePosition);

        for (i = 0; i < obj.Length; i++)
        {
            if (HorseyLib.PlayerInMenu.Value) obj[i] = null;
            else
            {
                Physics.Raycast(ray, out hit, 1, masks[i]);
                obj[i] = hit.collider ? hit.collider.GetComponent<Interactable>() : null;
            }

            if (obj[i])
            {
                if (!obj[i].mouseEntered)
                {
                    obj[i].mouseEntered = true;
                    obj[i].mouseEnter();
                }
                obj[i].mouseOver();
                if (cInput.GetKeyDown("Use")) obj[i].use();
                if (Input.GetMouseButtonDown(0))
                {
                    obj[i].lClick();
                    obj[i].lClicked = true;
                }
                if (Input.GetMouseButton(0)) obj[i].lHold();
                if (Input.GetMouseButtonUp(0))
                {
                    obj[i].lRelease();
                    obj[i].lClicked = false;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    obj[i].rClick();
                    obj[i].rClicked = true;
                }
                if (Input.GetMouseButton(1)) obj[i].rHold();
                if (Input.GetMouseButtonUp(1))
                {
                    obj[i].rRelease();
                    obj[i].rClicked = false;
                }
                if (Input.mouseScrollDelta.y > 0) obj[i].scrollUp();
                if (Input.mouseScrollDelta.y < 0) obj[i].scrollDown();
            }

            if (obj[i] != last[i] && last[i] != null)
            {
                last[i].mouseExit();
                last[i].mouseEntered = false;
                last[i].lClicked = false;
                last[i].rClicked = false;
                if (last[i].lClicked) last[i].lRelease();
                if (last[i].rClicked) last[i].rRelease();
            }
            last[i] = obj[i];
        }
    }

    void OnDestroy()
    {
        HorseyLib.initialized = false;
        masks.Clear();
    }
}