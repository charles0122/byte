using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Role",fileName ="new Role",order =1)]
public class Role : ScriptableObject
{
    public int index=0;
    // protected int Likability{get;set;}=10;
    [SerializeField]  public string RoleName;
    
    [HideInInspector]public RoleDialogSO roleDialogSO;      // GameManger 动态改变 根据Npc index获取对应的RoleDialogSO

    public float WaitTime =30f;
    [SerializeField] public Recipe recipe;      // 拖拽 每天都一样

}
