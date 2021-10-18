using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueContainer
{
    //public string[] name;
    public string name;

    [TextArea(5,10)]
    public string[] speech;
    public Sprite avatar;
    public AudioClip[] sounds;
}
