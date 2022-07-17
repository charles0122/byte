using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Recipe",fileName ="new Recipe",order =1)]
public class Recipe : ScriptableObject
{
    public string desc;
    public List<RecipeItem> RecipeItemList;
}


public enum RecipeState{
    None,
    UnConfirm,
    Confirm,
    Fail,
    Success
}
