using System.Collections;
using System.Collections.Generic;
using ByteLoop.Manager;
using UnityEngine;
using System;

public class MaterialCopy : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Collider2D _collider2D;
    [SerializeField] protected MaterialType _type = MaterialType.None;
    [SerializeField] protected float _needTime = 5f;
    [SerializeField] protected Animator _anim;

    public MaterialType Type=>_type;



    private void OnMouseDown()
    {
        if (GameManager.Instance.InputAllowed)
        {
            StartCoroutine(InstantiateMaterial());
        }
        else
        {
            Debug.Log("不允许点击");
        }
    }
    private IEnumerator InstantiateMaterial()
    {
        GameManager.Instance.InputAllowed = false;
        // Debug.Log("添加原材料"+_type);
        GameObject GO = Instantiate(this.gameObject, ProductionStation.Instance.gameObject.transform.position, Quaternion.identity);
        GO.transform.SetParent(GameObject.FindGameObjectWithTag("MarterialParent").transform);
        // Timer
        GO = Instantiate(GameManager.Instance.TimerGO);
        GO.GetComponent<Timer>().TotalTime = _needTime;
        GO.GetComponent<Timer>().Start = true;
        yield return new WaitForSeconds(_needTime);
        // 检查是否
        EventCenter.CallComfirmCurrentProductionEvent();
        Debug.Log("等待完毕");
        GameManager.Instance.InputAllowed = true;
    }
}



// public enum MaterialType
// {
//     None,
//     Egg,
//     Pepper,
//     Sauce,
//     Sausage,
//     Sesame
// }
