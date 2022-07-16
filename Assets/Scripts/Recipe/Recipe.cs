using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Recipe",fileName ="new Recipe",order =1)]
public class Recipe : ScriptableObject
{
    public List<RecipeItem> RecipeItemList;
}


public enum RecipeState{
    UnConfirm,
    Confirm
}
