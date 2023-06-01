using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript5 : MonoBehaviour
{
    //GameObjects

    public GameObject skin_head;
    public GameObject skin_body;
    
    public GameObject hair_e;
    public GameObject scarf;
    public GameObject tank_top;
    public GameObject shortpants;
    public GameObject shoes2;

    //Textures
    public Texture[] skin_textures;
    public Texture[] hair_e_textures;
    public Texture[] tank_top_textures;
    public Texture[] shoes2_textures;
    public Texture[] shortpants_textures;
    public Texture[] scarf_textures;


    public Animator ani;


   

    // 정답 체크를 위한 int 변수들 - 옷가지의 종류를 나타냄
    //skincolor
    int skin_color = 4;

    //malefemale
    int male_female = 0;
    //hair e
    int hair = 2;
    int hairColor = 1;

    // suit 아님
    int suit_or_cloth =1;

    //tanktop
    int upper_cloth = 3; //윗도리 종류 = tanktop
    int tank_top_texture = 7; 

    //trouser
    int which_trouser = 1;
    int shortpants_texture = 4;

    //shoes2
    int shoes = 1;
    int shoes2_texture = 3;

    //scarf
    int scarf_texture = 7;


    void Start()
    {
        // hair e + tanktop + shortpants + scarf + shoes2
        hair_e.SetActive(true); // hair a
        tank_top.SetActive(true);
        shoes2.SetActive(true);
        shortpants.SetActive(true);
        scarf.SetActive(true);


        // Texture (hair c + shirt + shortpants + shoes1)
        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[1];
        shoes2.GetComponent<Renderer>().materials[0].mainTexture = shoes2_textures[shoes2_texture]; // shoes1 texture
        shortpants.GetComponent<Renderer>().materials[0].mainTexture = shortpants_textures[shortpants_texture]; // short pants texture
        tank_top.GetComponent<Renderer>().materials[0].mainTexture = tank_top_textures[tank_top_texture]; // pullover texture       
        scarf.GetComponent<Renderer>().materials[0].mainTexture = scarf_textures[scarf_texture]; // pullover texture   
    }
}
