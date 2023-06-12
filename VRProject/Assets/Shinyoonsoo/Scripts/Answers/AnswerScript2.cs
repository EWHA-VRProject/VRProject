using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript2 : MonoBehaviour
{
    //GameObjects

    public GameObject skin_head;
    public GameObject skin_body;
    
    public GameObject hair_c;
    public GameObject shirt;
    public GameObject shortpants;
    public GameObject shoes1;

    //Textures
    public Texture[] skin_textures;
    public Texture[] hair_c_textures;
    public Texture[] shirt_textures;
    public Texture[] shoes1_textures;
    public Texture[] shortpants_textures;


    public Animator ani;


   

    // 정답 체크를 위한 int 변수들 - 옷가지의 종류를 나타냄
    //skincolor
    int skin_color = 3;

    //malefemale
    int male_female = 1;

    //hair a
    int hair = 0;
    int hairColor = 1;

    // suit 아님
    int suit_or_cloth =1;

    //shirt
    int upper_cloth = 1; //윗도리 종류 = shirt
    int shirt_texture = 7; // 셔츠 텍스쳐

    //shortpants
    int which_trouser = 1;
    int shortpants_texture = 4;

    //shoes1
    int shoes = 0;
    int shoes1_texture = 7;


    void Start()
    {
        // hair c + shirt + shortpants + shoes1
        hair_c.SetActive(true); // hair a
        shirt.SetActive(true);
        shoes1.SetActive(true);
        shortpants.SetActive(true);


        // Texture (hair c + shirt + shortpants + shoes1)
        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[1];
        shoes1.GetComponent<Renderer>().materials[0].mainTexture = shoes1_textures[shoes1_texture]; // shoes1 texture
        shortpants.GetComponent<Renderer>().materials[0].mainTexture = shortpants_textures[shortpants_texture]; // short pants texture
        shirt.GetComponent<Renderer>().materials[0].mainTexture = shirt_textures[shirt_texture]; // pullover texture       

    }
}
