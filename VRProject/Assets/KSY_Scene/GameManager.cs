using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainCam;
    
    //public Player player;

    public int stage;
    public float playTime;
    //public Target target1;
    //public Target target2;
    //public Target target3;
    public bool isPlaying;

    public GameObject startPanel;
    public GameObject gamePanel;
    public TMP_Text stageTxt;
    public TMP_Text playTimeTxt;
    public GameObject targetBtn1;
    public GameObject targetBtn2;
    public GameObject targetBtn3;
    public GameObject targetImg1;
    public GameObject targetImg2;
    public GameObject targetImg3;
    
    private bool toggle1 =false;
    private bool toggle2 =false;
    private bool toggle3 =false;

    void Awake(){

    }

    public void GameStart(){
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        isPlaying=true;
        //player.gameObject.SetActive(true);

    }
    void Start() {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        targetImg1.SetActive(false);
        targetImg2.SetActive(false);
        targetImg3.SetActive(false);
    }

    void Update(){
        if(isPlaying){
            playTime+=Time.deltaTime;
        }
        
    }

    void LateUpdate(){
        stageTxt.text="STAGE "+stage;
        //1: 한명 5분, 2: 한명 3분, 3: 두명 5분, 4: 두명 3분, 5: 세명 3분
        int targetNumber;
        int maxPlayTime;
        if(stage<=2){
            targetNumber=1;
            if(stage==1){
                maxPlayTime=300;
            }
            else{
                maxPlayTime=180;
            }
        }
        else if(stage<=4){
            targetNumber=2;
            if(stage==3){
                maxPlayTime=300;
            }
            else{
                maxPlayTime=180;
            }
        }
        else{
            targetNumber=3;
            maxPlayTime=180;
        }

        int hour=(int)(playTime/3600);
        int min=(int)(playTime-hour*3600)/60;
        int second=(int)(playTime%60);

        int maxHour=(int)(maxPlayTime/3600);
        int maxMin=(int)(maxPlayTime-maxHour*3600)/60;
        int maxSecond=(int)(maxPlayTime%60);

        playTimeTxt.text=string.Format("{0:00}", min)+":"+string.Format("{0:00}", second)+"/"+string.Format("{0:00}", maxMin)+":"+string.Format("{0:00}", maxSecond);

        if(targetNumber==1){
            targetBtn1.SetActive(true);
            targetBtn2.SetActive(false);
            targetBtn3.SetActive(false);
        }
        else if(targetNumber==2){
            targetBtn1.SetActive(true);
            targetBtn2.SetActive(true);
            targetBtn3.SetActive(false);
        }
        else{
            targetBtn1.SetActive(true);
            targetBtn2.SetActive(true);
            targetBtn3.SetActive(true);
        }
        
    }
    public void ShowTarget1(){
        if(!toggle1){
            targetImg1.SetActive(true);
            toggle1=true;
        }
        else{
            targetImg1.SetActive(false);
            toggle1=false;
        }
    }
    public void ShowTarget2(){
        if(!toggle2){
            targetImg2.SetActive(true);
            toggle2=true;
        }
        else{
            targetImg2.SetActive(false);
            toggle2=false;
        }
    }
    public void ShowTarget3(){
        if(!toggle3){
            targetImg3.SetActive(true);
            toggle3=true;
        }
        else{
            targetImg3.SetActive(false);
            toggle3=false;
        }
    }
    

}
