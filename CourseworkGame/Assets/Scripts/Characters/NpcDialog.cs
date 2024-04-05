using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcDialog
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
