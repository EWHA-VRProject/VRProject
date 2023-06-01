using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript1 : MonoBehaviour
{
    //GameObjects

    public GameObject skin_head;
    public GameObject skin_body;
    
    public GameObject hair_a;
    public GameObject chain1;
    public GameObject pullover;
    public GameObject shortpants;
    public GameObject shoes3;

    //Textures
    public Texture[] skin_textures;
    public Texture[] hair_a_textures;
    public Texture[] pullover_textures;
    public Texture[] shoes3_textures;
    public Texture[] shortpants_textures;
    public Texture[] chain1_textures;


    public Animator ani;


    public bool show_run;
   
    bool hat;

    // 정답 체크를 위한 int 변수들 - 옷가지의 종류를 나타냄
    //skincolor
    int skin_color = 1;

    //malefemale
    int male_female = 0;
    //hair a
    int hair = 0;
    int hairColor = 1;
    int hair_cut = 1;

    // suit 아님
    int suit_or_cloth =1;
    //chain1
    int chain = 0;

    //pullover
    int upper_cloth = 0; //윗도리 종류 = pullover

    //shortpants
    int which_trouser = 1;

    //shoes3
    int shoes = 2;
    int shoes3_texture = 2;


    IEnumerator Start()
    {
        // hair a + chain1 + pullover + shortpants + shoes3
        yield return new WaitForSeconds(0);

        hat = true;

        hair_a.SetActive(true); // hair a
        chain1.SetActive(true);
        pullover.SetActive(true);
        shoes3.SetActive(true);
        shortpants.SetActive(true);


        // Texture (hair a + cap + chain1 + pullover + shortpants + shoes3)
        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[3]; // hair a texture
        shoes3.GetComponent<Renderer>().materials[0].mainTexture = shoes3_textures[2]; // shoes3 texture
        chain1.GetComponent<Renderer>().materials[0].mainTexture = chain1_textures[3]; // chain1 texture
        shortpants.GetComponent<Renderer>().materials[0].mainTexture = shortpants_textures[8]; // short pants texture
        pullover.GetComponent<Renderer>().materials[0].mainTexture = pullover_textures[15]; // pullover texture       



    }
}
