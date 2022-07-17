using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;
using ByteLoop.Manager;
public class DialogSystemTest : PersistentMonoSingleton<DialogSystemTest>
{
    [SerializeField] public DialogBox _dialogBox;
    [Multiline][SerializeField] private string content;
    public List<DialogData> datas;
    public int _index = 0;
    [SerializeField] private Widget _widget;


    public void Next(bool forceDisplayDirectly)
    {

        _dialogBox.StartCoroutine(_dialogBox.PrintDialog(
            datas[_index].Content,
            "",
            !forceDisplayDirectly && datas[_index].NeedTyping,
            datas[_index].AutoNext,
            datas[_index].CanQuickShow
        ));
        _index++;
        _index = _index % datas.Count;

    }

    public void StartDialog()
    {
        _dialogBox.Open(Next);
    }
}
