using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ByteLoop.DialogSystem;
using System;
using ByteLoop.Manager;

public class DialogBox : MonoBehaviour
{
    // [SerializeField] private Image _background;
    [SerializeField] private Widget _widget, _nextCursor;
    [SerializeField] private TMPro.TextMeshProUGUI _npc;
    [SerializeField] private AdvancedText _content;
    // [SerializeField] private Animator _nextCursorAnim;


    private bool _interactable;
    private bool _printFinished;
    private bool _canQuickShow;
    private bool _autoNext;

    private bool CanQuickShow => !_printFinished && _canQuickShow;

    private bool CanNext => _printFinished;

    public Action<bool> OnNext;

    private void Awake()
    {
        _content.OnFinished = PrintFinished;
    }
    // 打印结束
    private void PrintFinished()
    {
        if (_autoNext)
        {
            _interactable = false;
            OnNext?.Invoke(false);
        }
        else
        {
            _interactable = true;
            _printFinished = true;
            _nextCursor.Fade(1, 0.2f, null);
        }
    }

    private void Update()
    {
        // Debug.Log(_interactable);
        if (_interactable)
        {
            UpdateInput();
        }
    }


    public void ShowTextByRecipeState(RecipeState recipeState)
    {
        switch (recipeState)
        {
            case RecipeState.Fail:
                DialogSystemTest.Instance._index++;
                if (CanNext)
                {
                    _interactable = false;
                    _nextCursor.Fade(0, 0.5f, null);
                    OnNext?.Invoke(false);
                }
                break;
            case RecipeState.Success:
                if (CanQuickShow)
                {
                    _content.QuickShowRemaining();
                }
                else if (CanNext)
                {
                    _interactable = false;
                    _nextCursor.Fade(0, 0.5f, null);
                    OnNext?.Invoke(true);
                }
                break;
        }
        // Debug.Log(GameManager.Instance.TodayHasNextNpc());
        // if (GameManager.Instance.TodayHasNextNpc())
        // {
        //     GameManager.Instance.Npc++;
        // }
        
    }

    private void UpdateInput()
    {
        // if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.N))
        // {
        //     Debug.Log("Cancel");
        //     if (CanQuickShow)
        //     {
        //         _content.QuickShowRemaining();
        //     }
        //     else if (CanNext)
        //     {
        //         _interactable = false;
        //         _nextCursor.Fade(0, 0.5f, null);
        //         OnNext?.Invoke(true);
        //     }
        // }
        // else if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.M))
        // {
        //     Debug.Log("Submit");
        //     if (CanNext)
        //     {
        //         _interactable = false;
        //         _nextCursor.Fade(0, 0.5f, null);
        //         OnNext?.Invoke(false);
        //     }
        // }
    }

    public void Open(Action<bool> nextEvent)
    {
        OnNext = nextEvent;
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            _widget.RenderOpacity = 0;
            _widget.Fade(1, 0.2f, null);
            _npc.SetText("");
            _content.Init();

        }
        _nextCursor.RenderOpacity = 0;

        OnNext?.Invoke((false));

    }
    public void Close(Action onClosed)
    {
        _widget.Fade(0, 0.4f, () =>
        {
            gameObject.SetActive(false);
            onClosed?.Invoke();
        });
    }

    public IEnumerator PrintDialog(string content, string npc, bool needTyping = true, bool autoNext = false, bool canQuickShow = true)
    {
        _interactable = false;
        _printFinished = false;
        if (_content.text != "")
        {
            _content.Disappear();
            yield return new WaitForSecondsRealtime(0.2f);
        }
        _canQuickShow = canQuickShow;
        _autoNext = autoNext;
        _npc.SetText(npc); ;
        if (needTyping)
        {
            _interactable = true;
            _content.StartCoroutine(_content.SetText(content, AdvancedText.DisplayType.Typing));
        }
        else
        {
            _content.StartCoroutine(_content.SetText(content, AdvancedText.DisplayType.Fading));
        }
    }



}
