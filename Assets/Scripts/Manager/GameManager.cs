using System;
using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ByteLoop.Manager
{


    public class GameManager : PersistentMonoSingleton<GameManager>
    {
        [SerializeField] private int day = 0;
        [SerializeField] private int npc = 0;
        public bool IsPaused = false;
        public bool InputAllowed = true;
        public GameObject TimerGO;
        [Space]
        [Header("NPC 列表")]
        [SerializeField] public List<Role> npcList;

        [Space]
        [Header("对话相关")]

        [SerializeField] public List<DayDialog> dayDialogList;
        private List<RoleDialogSO> roleDialogSOList;
        [SerializeField] public DialogBox _dialogBox;
        private int dialogIndex = 0;
        public int Day
        {
            get => day;
            set
            {
                DialogSystemTest.Instance._index=0;
                if (value < dayDialogList.Count)
                {
                    day = value;
                    RefalshCurrentProductionDialog();
                    // 每天更新 要做的菜谱列表
                    // RefalshProductionStationRecipe();
                    RefalshCurrentProductionStationRecipe();
                }
                else
                {
                    Day = 0;
                }

            }
        }
        public int Npc
        {
            get => npc;
            set
            {
                DialogSystemTest.Instance._index=0;
                if (value <= dayDialogList[Day].dialogSOs.Count)
                {
                    npc = value;
                    RefalshCurrentProductionDialog();
                    RefalshCurrentProductionStationRecipe();
                }
                else
                {
                    Day++;
                    Npc = 0;
                    Debug.Log("next day");
                }

            }
        }

        private List<DialogData> currentProductionDialog;

        public List<DialogData> RefalshCurrentProductionDialog()
        {
            List<DialogData> list = new List<DialogData>();
            list.AddRange(dayDialogList[day].dialogSOs[npc].defaultDialog);
            list.Add(dayDialogList[day].dialogSOs[npc].successDialog);
            list.Add(dayDialogList[day].dialogSOs[npc].failDialog);
            currentProductionDialog = list;
            return list;
        }

        private void Start()
        {
            // 如果做存储的话 保存这两个值
            Day = 0;
            Npc = 0;
            // DialogSystemTest.Instance._dialogBox.Open(null);
            // DialogSystemTest.Instance.Next(false);
            DialogSystemTest.Instance.StartDialog();

        }

        // void RefalshProductionStationRecipe()
        // {
        //     for (int i = 0; i < npcList.Count; i++)
        //     {
        //         Recipe recipe = npcList[i].recipe;
        //         ProductionStation.Instance.RecipeList.Add(recipe);
        //     }

        // }
        void RefalshCurrentProductionStationRecipe()
        {
            ProductionStation.Instance.CurrentRecipe = npcList[Npc].recipe;
        }
        private void Update()
        {


        }

        public bool TodayHasNextNpc()
        {
            return Day < dayDialogList.Count && Npc < dayDialogList[Day].dialogSOs.Count;
        }

        void Next(bool forceDisplayDirectly)
        {
            // _dialogBox.StartCoroutine(_dialogBox.PrintDialog(
            //     dayDialogList[day].dialogSOs[dialogIndex].defaultDialog[0].Content,
            //     dayDialogList[day].dialogSOs[dialogIndex].Npc,
            //     !forceDisplayDirectly && dayDialogList[day].dialogSOs[dialogIndex].defaultDialog[0].NeedTyping,
            //     dayDialogList[day].dialogSOs[dialogIndex].defaultDialog[0].AutoNext,
            //     dayDialogList[day].dialogSOs[dialogIndex].defaultDialog[0].CanQuickShow
            //  ));
            // dialogIndex++;
            // dialogIndex = dialogIndex / dayDialogList[day].dialogSOs[dialogIndex].defaultDialog.Count;

        }





    }
    [Serializable]
    public class DayDialog
    {
        [SerializeField] public List<RoleDialogSO> dialogSOs;
    }

}