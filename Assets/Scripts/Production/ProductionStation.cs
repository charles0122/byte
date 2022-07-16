using System;
using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;

namespace ByteLoop.Manager
{


    public class ProductionStation : PersistentMonoSingleton<ProductionStation>
    {
        public Recipe currentRecipe;
        public RecipeState recipeState = RecipeState.UnConfirm;
        public Queue<Recipe> RecipePrepareQueue;

        public void ClearCurrentProduction()
        {
            GameObject Go = GameObject.FindGameObjectWithTag("MarterialParent");
            Transform transform;
            for (int i = 0; i < Go.transform.childCount; i++)
            {
                transform = Go.transform.GetChild(i);
                GameObject.Destroy(transform.gameObject);
            }
            if (RecipePrepareQueue.Count>0)
            {
                currentRecipe = RecipePrepareQueue.Dequeue();                
            }else{
                Debug.Log("暂时没有生意");
            }
        }

        private void Update() {
            if (recipeState==RecipeState.Confirm)
            {
                ClearCurrentProduction();
            }
        }
        protected override void Awake()
        {
            base.Awake();
            RecipePrepareQueue = new Queue<Recipe>();
        }
        private void OnEnable()
        {
            EventCenter.ComfirmCurrentProductionEvent += CheckCurrentRecipeState;

        }

        private void OnDisable()
        {
            EventCenter.ComfirmCurrentProductionEvent -= CheckCurrentRecipeState;
        }


        void CheckCurrentRecipeState()
        {
            GameObject Go = GameObject.FindGameObjectWithTag("MarterialParent");
            Material[] materials = Go.GetComponentsInChildren<Material>();
            Dictionary<MaterialType, int> dic = new Dictionary<MaterialType, int>();
            // 整理已经加工好的原料
            for (int i = 0; i < materials.Length; i++)
            {
                if (dic.ContainsKey(materials[i].Type))
                {
                    dic[materials[i].Type]++;
                }
                else
                {
                    dic.Add(materials[i].Type, 1);
                }
            }

            int count = currentRecipe.RecipeItemList.Count;
            // 对比菜单和原料
            for (int i = 0; i < count; i++)
            {
                MaterialType materialType = currentRecipe.RecipeItemList[i].materialTypes;
                if (dic.ContainsKey(materialType))
                {
                    // Debug.Log("UnConfirm");
                    // Debug.Log(dic[materialType]);
                    // Debug.Log(currentRecipe.RecipeItemList[i].nums);
                    if (dic[materialType] < currentRecipe.RecipeItemList[i].nums)
                    {
                        // Debug.Log("UnConfirm");
                        return;
                    }
                }else{
                    return;
                }
            }
            // Debug.Log(currentRecipe.RecipeItemList.Count);
            // Debug.Log(dic.Count);
            // Debug.Log("Confirm");
            recipeState = RecipeState.Confirm;

            // currentRecipe.RecipeItemList.Contains
            // return RecipeState.Confirm;

        }
    }
}