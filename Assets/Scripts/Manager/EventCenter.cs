using System;
using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;
using UnityEngine.Events;
namespace ByteLoop.Manager
{
    public class EventCenter : PersistentMonoSingleton<EventCenter>
    {

        public static event Action ComfirmCurrentProductionEvent;
        public static void CallComfirmCurrentProductionEvent()
        {
            if (ComfirmCurrentProductionEvent != null)
            {
                ComfirmCurrentProductionEvent();
            }
        }


        // Scene Controller 
        // 场景卸载前淡出事件
        public static event Action BeforeSceneUnloadFadeOutEvent;
        public static void CallBeforeSceneUnloadFadeOutEvent()
        {
            if (BeforeSceneUnloadFadeOutEvent != null)
            {
                BeforeSceneUnloadFadeOutEvent();
            }
        }

        //场景卸载之前事件
        public static event Action BeforeSceneUnloadEvent;
        public static void CallBeforeSceneUnloadEvent()
        {
            if (BeforeSceneUnloadEvent != null)
            {
                BeforeSceneUnloadEvent();
            }
        }

        // 场景加载后事件
        public static event Action AfterSceneloadEvent;
        public static void CallAfterSceneloadEvent()
        {
            if (AfterSceneloadEvent != null)
            {
                AfterSceneloadEvent();
            }
        }

        // 场景加载后淡入事件
        public static event Action AfterSceneloadFadeInEvent;
        public static void CallAfterSceneloadFadeInEvent()
        {
            if (AfterSceneloadFadeInEvent != null)
            {
                AfterSceneloadFadeInEvent();
            }
        }





        /// <summary>
        /// 删除事件监听(带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件的函数</param>
        public void RemoveEventListener<T>(string name, UnityAction action)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                eventDic[name] -= action;
        }


        /// <summary>
        /// 清空事件中心
        /// </summary>
        public void ClearEvent()
        {
            eventDic.Clear();
        }

        //key对应的是事件的名字
        //value对应的是监听这个事件对应的委托函数们
        private Dictionary<string, UnityAction> eventDic = new Dictionary<string, UnityAction>();

        /// <summary>
        ///添加事件监听 
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件的委托函数</param>
        public void AddEventListener(string name, UnityAction action)
        {
            //判断字典里有没有对应这个事件，有就执行，没有就加进去。
            if (eventDic.ContainsKey(name))
            {
                eventDic[name] += action;
            }
            else
            {
                eventDic.Add(name, action);
            }
        }
        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="name">哪一个名字的事件触发了</param>
        public void EventTrigger(string name)
        {
            if (eventDic.ContainsKey(name))
            {
                // eventDic[name]();
                eventDic[name].Invoke();
            }
        }

    }


}