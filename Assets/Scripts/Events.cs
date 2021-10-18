using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    Shield,
    Shoot
}

public class SwitchPlayerModeEvent {
    public PlayerMode mode;
    public SwitchPlayerModeEvent(PlayerMode mode)
    {
        this.mode = mode;
    }
}

public class SelectShootProjectile
{
    public GameObject projectile;
    public SelectShootProjectile(GameObject projectile)
    {
        this.projectile = projectile;
    }
}

public class TriggerPlayerCanMove
{
    public bool playerCanMove;

    public TriggerPlayerCanMove(bool playerCanMove)
    {
        this.playerCanMove = playerCanMove;
    }
}

public class UseKeyEvent {
    public KeyDragAndDrop.Key key;

    public UseKeyEvent(KeyDragAndDrop.Key _key) { key = _key; }
}

public class SpawnEndEvent {
    
}

public class ProjectileColorChange
{
    public string ChangedColor = "";
    public bool DidChange = false;

    public ProjectileColorChange(string _ChangedColor, bool _DidChange) { ChangedColor = _ChangedColor; DidChange = _DidChange; }
}

public enum PaintbrushColor {
    Orange,
    Red,
    Yellow,
    Blue
}

public class PickupPaintbrush {
    public PaintbrushColor color;

    public PickupPaintbrush(PaintbrushColor _color) { color = _color; }
}