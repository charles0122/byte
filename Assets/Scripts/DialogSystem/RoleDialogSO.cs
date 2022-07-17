using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogData{
    [Multiline] public string Content;
    public bool AutoNext;
    public bool NeedTyping;
    public bool CanQuickShow;
}

[CreateAssetMenu(menuName ="RoleDialog",fileName ="new RoleDialog",order =1)]
public class RoleDialogSO : ScriptableObject
{
    public string Npc;
    public List<DialogData> defaultDialog;
    public DialogData successDialog;
    public DialogData failDialog;
}
