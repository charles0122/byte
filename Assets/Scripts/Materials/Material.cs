using System.Collections;
using System.Collections.Generic;
using ByteLoop.Manager;
using UnityEngine;
using System;

public class Material : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Collider2D _collider2D;
    [SerializeField] protected MaterialType _type = MaterialType.None;
    [SerializeField] protected float _needTime = 5f;
    [SerializeField] protected Animator _anim;

    [SerializeField] protected Sprite defaultSprite, dragSprite, productionSprite;
    protected Vector2 startPos;
    [SerializeField] protected Transform targetTrans;
    [SerializeField] private bool isFinished;


    public MaterialType Type => _type;

    private void Start()
    {
        targetTrans = GameObject.FindGameObjectWithTag("MarterialParent").transform;
        if (defaultSprite != null)
        {
            _spriteRenderer.sprite = defaultSprite;
        }
        startPos = transform.position;
    }
    private void OnMouseDrag()
    {
        if (!isFinished&&dragSprite != null)
        {
            _spriteRenderer.sprite = dragSprite;
        }
        if (!isFinished && GameManager.Instance.InputAllowed)
        {
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(vector3.x, vector3.y);
        }
        else
        {
            Debug.Log("不允许移动或点击" + GameManager.Instance.InputAllowed);
        }
    }

    private void OnMouseUp()
    {

        if (!isFinished)
        {
            // 距离锅中心的多少距离  3f   可以改成原料属性
            if (Mathf.Abs(transform.position.x - targetTrans.position.x) <= 3f &&
            Mathf.Abs(transform.position.y - targetTrans.position.y) <= 3f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y);
                isFinished = true;
                StartCoroutine(InstantiateMaterial(transform.position));
            }
            else
            {
                transform.position = startPos;
            }
        }
    }
    private void OnMouseEnter()
    {
        if(!isFinished)
        transform.localScale += Vector3.one * 0.07f;
    }
    private void OnMouseExit()
    {
        if(!isFinished)
        transform.localScale -= Vector3.one * 0.07f;
    }

    // private void OnMouseDown()
    // {
    //     if (GameManager.Instance.InputAllowed)
    //     {
    //         StartCoroutine(InstantiateMaterial());
    //     }
    //     else
    //     {
    //         Debug.Log("不允许点击");
    //     }
    // }
    private IEnumerator InstantiateMaterial(Vector2 pos)
    {
        if (productionSprite != null)
        {
            _spriteRenderer.sprite = productionSprite;
        }
        GameManager.Instance.InputAllowed = false;
        // Debug.Log("添加原材料"+_type);
        // GameObject GO = Instantiate(this.gameObject, pos, Quaternion.identity);
        this.transform.SetParent(GameObject.FindGameObjectWithTag("MarterialParent").transform);
        // this.gameObject.SetActive(false);

        // Timer
        GameObject GO = Instantiate(GameManager.Instance.TimerGO);
        GO.GetComponent<Timer>().TotalTime = _needTime;
        GO.GetComponent<Timer>().Start = true;

        yield return new WaitForSeconds(_needTime);
        // 检查是否
        EventCenter.CallComfirmCurrentProductionEvent();
        Debug.Log("等待完毕");
        GameManager.Instance.InputAllowed = true;
        // Destroy(this.gameObject);
    }
}



public enum MaterialType
{
    None,
    Egg,    // 鸡蛋
    Pepper, // 胡椒粉
    Sauce,      // 甜面酱
    Sausage,    // 芝麻
    Sesame,     // 芝麻
    Coriander,  // 香菜
    Onion,      // 香葱
    RemaineLettuce,     // 生菜
    Flour,      // 面糊
    Meat,   // 里脊
    Oil,    // 油
    Crisp   // 薄脆
}
