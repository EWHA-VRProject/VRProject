using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript7 : MonoBehaviour
{
    //GameObjects

    public GameObject skin_head;
    public GameObject skin_body;
    
    public GameObject hair_d;
    public GameObject glasses;
    public GameObject pullover;
    public GameObject trousers;
    public GameObject shoes2;

    //Textures
    public Texture[] skin_textures;
    public Texture[] hair_d_textures;
    public Texture[] pullover_textures;
    public Texture[] shoes2_textures;
    public Texture[] trousers_textures;
    public Texture[] glasses_textures;


    public Animator ani;


   

    // 정답 체크를 위한 int 변수들 - 옷가지의 종류를 나타냄
    //skincolor
    int skin_color = 2;

    //female
    int male_female = 1;

    //hair d
    int hair = 1;
    int hairColor = 2;

    // suit 아님
    int suit_or_cloth =1;

    //pullover
    int upper_cloth = 0;
    int pullover_texture = 14; 

    //trouser
    int which_trouser = 0;
    int trouser_texture = 4;

    //shoes2
    int shoes = 1;
    int shoes2_texture = 3;

    //glasses
    int glasses_texture = 1;


    void Start()
    {
        // hair d + pullover + trouser + shoes2
        hair_d.SetActive(true); // hair a
        pullover.SetActive(true);
        shoes2.SetActive(true);
        trousers.SetActive(true);
        glasses.SetActive(true);


        // Texture (hair c + shirt + shortpants + shoes1)
        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[1];
        shoes2.GetComponent<Renderer>().materials[0].mainTexture = shoes2_textures[shoes2_texture]; // shoes1 texture
        trousers.GetComponent<Renderer>().materials[0].mainTexture = trousers_textures[trouser_texture]; // short pants texture
        pullover.GetComponent<Renderer>().materials[0].mainTexture = pullover_textures[pullover_texture]; // pullover texture       
        glasses.GetComponent<Renderer>().materials[0].mainTexture = glasses_textures[glasses_texture]; // pullover texture   
    }
}
