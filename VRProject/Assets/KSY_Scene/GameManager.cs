using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainCam;
    
    //public Player player;

    // public int stage = 1;
    public int stage;
    public float playTime;
    //public Target target1;
    //public Target target2;
    //public Target target3;
    public bool foundAll;
    public bool isPlaying;
    public int maxPlayTime;

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject successPanel;
    public GameObject failPanel;
    public GameObject inventoryPanel;
    public TMP_Text stageTxt;
    public TMP_Text playTimeTxt;
    public GameObject targetBtn1;
    // public GameObject targetBtn2;
    // public GameObject targetBtn3;
    public Image targetImg1;
    // public Image targetImg2;
    // public Image targetImg3;
    
    public GameObject answer_target1;
    public GameObject answer_target2;
    public GameObject answer_target3;
    
    
    private bool toggle1 =false;
    // private bool toggle2 =false;
    // private bool toggle3 =false;
    private bool toggle4=false;


    public TMP_Text playTimeResultTxt;

    public Sprite[] images;

    void Awake(){

    }

    public void GameStart(){


        playTime=0;
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        targetImg1.gameObject.SetActive(false);
        // targetImg2.gameObject.SetActive(false);
        // targetImg3.gameObject.SetActive(false);
        toggle1=false;
        // toggle2=false;
        // toggle3=false;
        toggle4=false;
        isPlaying=true;


        List<int> numbersList = new List<int>();

        stageTxt.text="STAGE "+stage;
        
        //1: 한명 5분, 2: 한명 3분, 3: 두명 5분, 4: 두명 3분, 5: 세명 3분
        int targetNumber = 1;
        
        if(stage<=2){
            if(stage==1){ // stage 1
                maxPlayTime=500;
                targetImg1.sprite = images[0];
                //뒤에 안보이게 타겟 생성해놓음
                answer_target1.SetActive(true);
                answer_target2.SetActive(false);
                answer_target3.SetActive(false);
                
            }
            else{ // stage 2
                maxPlayTime=400;
                targetImg1.sprite = images[1];

                answer_target1.SetActive(false);
                answer_target2.SetActive(true);
                answer_target3.SetActive(false);
            }
        }
        else if(stage<=4){
            if(stage==3){ // stage 3
                maxPlayTime=300;
                targetImg1.sprite = images[2];


                answer_target1.SetActive(false);
                answer_target2.SetActive(false);
                answer_target3.SetActive(true);
            }
            else{ // stage 4
                maxPlayTime=200;
                targetImg1.sprite = images[0];


                answer_target1.SetActive(true);
                answer_target2.SetActive(false);
                answer_target3.SetActive(false);
            }
        }
        else{ // stage 5
            maxPlayTime=180;
            targetImg1.sprite = images[1];


                answer_target1.SetActive(false);
                answer_target2.SetActive(true);
                answer_target3.SetActive(false);
        }

        

        // while(numbersList.Count < targetNumber){
        //     int number = Random.Range(0, images.Length);
        //     if(!numbersList.Contains(number)){
        //         numbersList.Add(number);
        //     }
        // }

        // if(targetNumber==1){
        //     targetImg1.sprite = images[0];

        //     targetBtn1.SetActive(true);
        //     // targetBtn2.SetActive(false);
        //     // targetBtn3.SetActive(false);
        // }
        // else if(targetNumber==2){
        //     targetImg1.sprite = images[0];
        //     targetImg2.sprite = images[1];

        //     targetBtn1.SetActive(true);
        //     targetBtn2.SetActive(true);
        //     targetBtn3.SetActive(false);
        // }
        // else{
        //     // mj
        //     targetImg1.sprite = images[0];
        //     targetImg2.sprite = images[1];
        //     targetImg3.sprite = images[2];

        //     targetBtn1.SetActive(true);
        //     targetBtn2.SetActive(true);
        //     targetBtn3.SetActive(true);
        // }
        
    }
    
    void Start() {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        targetImg1.gameObject.SetActive(false);
        // targetImg2.gameObject.SetActive(false);
        // targetImg3.gameObject.SetActive(false);
        toggle1 =false;
        // toggle2 =false;
        // toggle3 =false;
        toggle4=false;

    }

    void Update(){
        if(isPlaying){
            playTime+=Time.deltaTime;
        }
        if(isPlaying && playTime>=maxPlayTime){
            isPlaying=false;
            gamePanel.SetActive(false);
            failPanel.SetActive(true);
        }
        if(foundAll){
            isPlaying=false;
            gamePanel.SetActive(false);
            inventoryPanel.SetActive(false);
            successPanel.SetActive(true);
            int hour=(int)(playTime/3600);
            int min=(int)(playTime-hour*3600)/60;
            int second=(int)(playTime%60);
            playTimeResultTxt.text=string.Format("{0:00}", min)+":"+string.Format("{0:00}", second);
            foundAll=false;
        }
    }

    void LateUpdate(){
        int hour=(int)(playTime/3600);
        int min=(int)(playTime-hour*3600)/60;
        int second=(int)(playTime%60);

        int maxHour=(int)(maxPlayTime/3600);
        int maxMin=(int)(maxPlayTime-maxHour*3600)/60;
        int maxSecond=(int)(maxPlayTime%60);

        playTimeTxt.text=string.Format("{0:00}", min)+":"+string.Format("{0:00}", second)+"/"+string.Format("{0:00}", maxMin)+":"+string.Format("{0:00}", maxSecond);
        
    }
    public void nextButton(){
        if(stage<5){
            stage+=1;
        }
        GameStart();
        
    }
    public void ShowTarget1(){
        if(!toggle1){
            // toggle2=false;
            // toggle3=false;
            // targetImg2.gameObject.SetActive(false);
            // targetImg3.gameObject.SetActive(false);
            
            targetImg1.gameObject.SetActive(true);
            toggle1=true;
        }
        else{
            targetImg1.gameObject.SetActive(false);
            toggle1=false;
        }
    }
    // public void ShowTarget2(){
    //     if(!toggle2){
    //         toggle1=false;
    //         toggle3=false;
    //         targetImg1.gameObject.SetActive(false);
    //         targetImg3.gameObject.SetActive(false);
            
    //         targetImg2.gameObject.SetActive(true);
    //         toggle2=true;
    //     }
    //     else{
    //         targetImg2.gameObject.SetActive(false);
    //         toggle2=false;
    //     }
    // }
    // public void ShowTarget3(){
    //     if(!toggle3){
    //         toggle1=false;
    //         toggle2=false;
    //         targetImg1.gameObject.SetActive(false);
    //         targetImg2.gameObject.SetActive(false);
    //         targetImg3.gameObject.SetActive(true);
    //         toggle3=true;
    //     }
    //     else{
    //         targetImg3.gameObject.SetActive(false);
    //         toggle3=false;
    //     }
    // }
    public void showInventory(){
        if(!toggle4){
            inventoryPanel.SetActive(true);
            toggle4=true;
        }
        else{
            inventoryPanel.SetActive(false);
            toggle4=false;
        }
    }
    

}
