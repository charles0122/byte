// using System;
using System.Collections;
using System.Collections.Generic;
using ByteLoop.Tool;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour 
{

    [SerializeField] private Slider slider;
    [SerializeField] public bool Start{get;set;}=false;

    [SerializeField] public float TotalTime{get;set;}=10f;
    [SerializeField] private float startTime=0;

   

    private void Update() {
        if(Start && startTime<=TotalTime){
            slider.value = startTime/TotalTime;
            startTime  += Time.deltaTime;
        }

        if( startTime>TotalTime){
            Destroy(this.gameObject);
        }
    }

   
}
