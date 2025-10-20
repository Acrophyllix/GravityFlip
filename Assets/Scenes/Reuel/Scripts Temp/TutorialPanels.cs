using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TutorialPanels : MonoBehaviour
{
    public GameObject panel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        StartCoroutine(DelayOnStart());    
    }

    
    public void pauseOnStart(){

        if(this.gameObject.CompareTag("Intro")){
            Debug.Log("this is intro");
            Time.timeScale = 0f;
        }
    }
    public void exitButton(){
        Time.timeScale = 1f;
    }


    void OnCollisionEnter2D( Collision2D other){

        if(other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("gLightBox") ){
            panel.SetActive(true);
            Time.timeScale = 0f;

        
        }else if(other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("rLightBox")){
            panel.SetActive(true);
            Time.timeScale = 0f;
        }else if(other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("PressureCollision")){
            Debug.Log("time start");
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter2D( Collider2D other){
        Debug.Log("Trigger");
        //Time Panel
        if(other.gameObject.CompareTag("rLightBox") && this.gameObject.CompareTag("RedPlate")){
            panel.SetActive(true);
            Time.timeScale = 0f;
        }else if(other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("HazardTrigger")){
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    IEnumerator DelayOnStart(){
        int i = 0;
        while(i==0){
            yield return new WaitForSeconds(1f);
            pauseOnStart();
            i++;
        }
    }

    
}
