using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript3 : MonoBehaviour
{
    //GameObjects

    public GameObject skin_head;
    public GameObject skin_body;
    
    public GameObject hair_d;
    public GameObject glasses;
    public GameObject t_shirt;
    public GameObject trousers;
    public GameObject shoes2;

    //Textures
    public Texture[] skin_textures;
    public Texture[] hair_d_textures;
    public Texture[] t_shirt_textures;
    public Texture[] shoes2_textures;
    public Texture[] trousers_textures;
    public Texture[] glasses_textures;


    public Animator ani;


   

    // 정답 체크를 위한 int 변수들 - 옷가지의 종류를 나타냄
    //skincolor
    int skin_color = 0;

    //malefemale
    int male_female = 1;

    //hair a
    int hair = 1;
    int hairColor = 1;

    // suit 아님
    int suit_or_cloth =1;

    //shirt
    int upper_cloth = 1; //윗도리 종류 = shirt
    int t_shirt_texture = 7; // 셔츠 텍스쳐

    //trouser
    int which_trouser = 0;
    int trouser_texture = 4;

    //shoes2
    int shoes = 1;
    int shoes2_texture = 5;

    //glasses
    int glasses_texture = 3;


    void Start()
    {
        // hair c + shirt + shortpants + shoes1
        hair_d.SetActive(true); // hair a
        t_shirt.SetActive(true);
        shoes2.SetActive(true);
        trousers.SetActive(true);
        glasses.SetActive(true);


        // Texture (hair c + shirt + shortpants + shoes1)
        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[1];
        shoes2.GetComponent<Renderer>().materials[0].mainTexture = shoes2_textures[shoes2_texture]; // shoes1 texture
        trousers.GetComponent<Renderer>().materials[0].mainTexture = trousers_textures[trouser_texture]; // short pants texture
        t_shirt.GetComponent<Renderer>().materials[0].mainTexture = t_shirt_textures[t_shirt_texture]; // pullover texture       
        glasses.GetComponent<Renderer>().materials[0].mainTexture = glasses_textures[glasses_texture]; // pullover texture   
    }
}
