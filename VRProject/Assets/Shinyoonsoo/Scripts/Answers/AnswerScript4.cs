using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript4 : MonoBehaviour
{
    //GameObjects

    public GameObject skin_head;
    public GameObject skin_body;
    
    public GameObject farmer_suit;
    public GameObject farmer_suit_hat;

    //Textures
    public Texture[] skin_textures;
    public Texture farmer_suit_texture;


    public Animator ani;

    bool hat;
   

    // 정답 체크를 위한 int 변수들 - 옷가지의 종류를 나타냄
    int skin_color = 5;
    int male_female = 0;
    // suit 장착

    int suit_or_cloth =0;
    int which_suit = 2;


    void Start()
    {
        // farmersuit
        farmer_suit.SetActive(true);
        farmer_suit_hat.SetActive(true);

        // Texture (hair c + shirt + shortpants + shoes1)
        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];

        farmer_suit_hat.GetComponent<Renderer>().materials[0].mainTexture = farmer_suit_texture;
    }
}
