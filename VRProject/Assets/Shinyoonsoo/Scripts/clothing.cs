using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothing : MonoBehaviour
{


    public GameObject skin_head;
    public GameObject skin_body;
  

    public GameObject cigarette;
    public GameObject crowbar;
    public GameObject fireaxe;
    public GameObject glock;
    public GameObject phone;
    

    public GameObject beard_a;
    public GameObject beard_b;
    public GameObject beard_c;
    public GameObject beard_d;

    public GameObject hair_a;
    public GameObject hair_b;
    public GameObject hair_c;
    public GameObject hair_d;
    public GameObject hair_e;

    public GameObject cap;
    public GameObject cap2;
    public GameObject cap3;
    public GameObject chain1;
    public GameObject chain2;
    public GameObject chain3;
    
    public GameObject banker_suit;

    public GameObject cock_suit;
    public GameObject cock_suit_hat;

    public GameObject farmer_suit;
    public GameObject farmer_suit_hat;

    public GameObject fireman_suit;
    public GameObject fireman_suit_hat;

    public GameObject mechanic_suit;
    public GameObject mechanic_suit_hat;

    public GameObject nurse_suit;

    public GameObject police_suit;
    public GameObject police_suit_hat;

    public GameObject roober_suit;
    public GameObject roober_suit_hat;

    public GameObject security_guard_suit;
    public GameObject security_guard_suit_hat;

    public GameObject seller_suit;

    public GameObject worker_suit;
    public GameObject worker_suit_hat;

    public GameObject glasses;
    public GameObject jacket;
    public GameObject pullover;
    public GameObject scarf;
    public GameObject shirt;

    public GameObject shoes1;
    public GameObject shoes2;
    public GameObject shoes3;

    public GameObject shortpants;
    public GameObject t_shirt;
    public GameObject tank_top;
    public GameObject trousers;


  
    
    public Texture[] skin_textures;

    public Texture[] beard_textures;

    public Texture[] hair_a_textures;
    public Texture[] hair_b_textures;
    public Texture[] hair_c_textures;
    public Texture[] hair_d_textures;
    public Texture[] hair_e_textures;

    public Texture[] cap_textures;
    public Texture[] cap2_textures;
    public Texture[] cap3_textures;
    public Texture[] chain1_textures;
    public Texture[] chain2_textures;
    public Texture[] chain3_textures;

    public Texture[] banker_suit_texture;

    public Texture cock_suit_texture;
    

    public Texture farmer_suit_texture;
    

    public Texture fireman_suit_texture;
    

    public Texture mechanic_suit_texture;
   

    public Texture nurse_suit_texture;

    public Texture police_suit_texture;


    public Texture roober_suit_texture;


    public Texture security_guard_suit_texture;
   

    public Texture seller_suit_texture;

    public Texture worker_suit_texture;


    public Texture[] glasses_textures;
    public Texture[] jacket_textures;
    public Texture[] pullover_textures;
    public Texture[] scarf_textures;
    public Texture[] shirt_textures;

    public Texture[] shoes1_textures;
    public Texture[] shoes2_textures;
    public Texture[] shoes3_textures;

    public Texture[] shortpants_textures;
    public Texture[] t_shirt_textures;
    public Texture[] tank_top_textures;
    public Texture[] trousers_textures;

    public Animator ani;


    public bool show_run;
   

    // clothing texture 변수 -> 정답 확인 위함
    int male_female;

    int glasses_texture;
    int jacket_texture;
    int pullover_texture;
    int scarf_texture;
    int shirt_texture;

    int shoes;
    int shoes1_texture;
    int shoes2_texture;
    int shoes3_texture;

    int which_trouser; 
    int shortpants_texture;
    int t_shirt_texture;
    int tank_top_texture;
    int trouser_texture;

    int skin_texture;

    int beard_texture;

    int hairColor;
    int hair_cut;
    int hair;
    int hair_a_texture;
    int hair_b_texture;
    int hair_c_texture;
    int hair_d_texture;
    int hair_e_texture;

    int cap_texture;
    int cap2_texture;
    int cap3_texture;

    int chain;
    int chain1_texture;
    int chain2_texture;
    int chain3_texture;
   
    int suit_or_cloth;
    int which_suit;

    bool hat;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0);

        // disapear all cloth, for a new run

        hat = true;

        hair_a.SetActive(false);
        hair_b.SetActive(false);
        hair_c.SetActive(false);
        hair_d.SetActive(false);
        hair_e.SetActive(false);

        beard_a.SetActive(false);
        beard_b.SetActive(false);
        beard_c.SetActive(false);
        beard_d.SetActive(false);

        cap.SetActive(false);
        cap2.SetActive(false);
        cap3.SetActive(false);

        chain1.SetActive(false);
        chain2.SetActive(false);
        chain3.SetActive(false);

        banker_suit.SetActive(false);

        cock_suit.SetActive(false);
        cock_suit_hat.SetActive(false);

        farmer_suit.SetActive(false);
        farmer_suit_hat.SetActive(false);

        fireman_suit.SetActive(false);
        fireman_suit_hat.SetActive(false);

        mechanic_suit.SetActive(false);
        mechanic_suit_hat.SetActive(false);

        nurse_suit.SetActive(false);

        police_suit.SetActive(false);
        police_suit_hat.SetActive(false);

        roober_suit.SetActive(false);
        roober_suit_hat.SetActive(false);

        security_guard_suit.SetActive(false);
        security_guard_suit_hat.SetActive(false);

        seller_suit.SetActive(false);

        worker_suit.SetActive(false);
        worker_suit_hat.SetActive(false);

        glasses.SetActive(false);

        jacket.SetActive(false);

        pullover.SetActive(false);

        scarf.SetActive(false);

        shirt.SetActive(false);

        shoes1.SetActive(false);

        shoes2.SetActive(false);

        shoes3.SetActive(false);

        shortpants.SetActive(false);

        t_shirt.SetActive(false);

        tank_top.SetActive(false);

        trousers.SetActive(false);








      

        // determining skin color

        int skin_color = UnityEngine.Random.Range(0, 6);

        skin_head.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];
        skin_body.GetComponent<Renderer>().materials[0].mainTexture = skin_textures[skin_color];



        // determining male or female
        male_female = UnityEngine.Random.Range(0, 2);

   
        
       
        // does a hat fit for the hair
        

        // determining haircolor
        hairColor = UnityEngine.Random.Range(0, 4);    // 0 = dark  1 = brown  2 = blonde
        
        // male
        if(male_female == 0)
        {
         

            hat = true;

            // choose hair type   hair_a , hair_b  , hair_e
            hair = UnityEngine.Random.Range(0, 3);

            if (hair == 0)
            {
                hair_a.SetActive(true);

                // 0 = full hair    1 = under cut
                hair_cut = UnityEngine.Random.Range(0, 2);
                hat = true;
            

                if (hairColor == 0)
                {
                    if (hair_cut == 0)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[0];
                    }
                    if (hair_cut == 1)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[1];
                    }
                }
                if (hairColor == 1)
                {
                    if (hair_cut == 0)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[2];
                    }
                    if (hair_cut == 1)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[3];
                    }

                }
                if (hairColor == 2)
                {
                    if (hair_cut == 0)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[4];
                    }
                    if (hair_cut == 1)
                    {
                        hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[5];
                    }

                }


            }

            if (hair == 1)
            {
                hair_b.SetActive(true);
                hat = false;

                // 0 = full hair    1 = under cut
                hair_cut = UnityEngine.Random.Range(0, 2);



                if (hairColor == 0)
                {
                    if (hair_cut == 0)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[0];
                    }
                    if (hair_cut == 1)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[5];
                    }
                }
                if (hairColor == 1)
                {
                    if (hair_cut == 0)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[1];
                    }
                    if (hair_cut == 1)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[3];
                    }

                }
                if (hairColor == 2)
                {
                    if (hair_cut == 0)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[2];
                    }
                    if (hair_cut == 1)
                    {
                        hair_b.GetComponent<Renderer>().materials[0].mainTexture = hair_b_textures[4];
                    }

                }



            }

            if (hair == 2)
            {
                hair_e.SetActive(true);
                hat = false;
               



                if (hairColor == 0)
                {
                    
                        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[0];
                    
                   
                }
                if (hairColor == 1)
                {
                   
                        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[1];
                    
                   

                }
                if (hairColor == 2)
                {
                   
                        hair_e.GetComponent<Renderer>().materials[0].mainTexture = hair_e_textures[2];
                    
                   

                }


            }

        }
        // female
        if(male_female == 1)
        {
            
            hat = false;

            // choose hair type   hair_c , hair_d
            hair = UnityEngine.Random.Range(0, 2);


            if(hair == 0)
            {
                hat = false;
                hair_c.SetActive(true);

                if(hairColor == 0)
                {
                    hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[0];
                }
                if (hairColor == 1)
                {
                    hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[1];
                }
                if (hairColor == 2)
                {
                    hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[2];
                }
            }
            if (hair == 1)
            {
                hat = false;
                hair_d.SetActive(true);

                if (hairColor == 0)
                {
                    hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[0];
                }
                if (hairColor == 1)
                {
                    hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[1];
                }
                if (hairColor == 2)
                {
                    hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[2];
                }
            }
        }

        shoes = UnityEngine.Random.Range(0, 3);

        if(shoes == 0)
        {
            shoes1.SetActive(true);

            shoes1_texture = UnityEngine.Random.Range(0, 8);

            shoes1.GetComponent<Renderer>().materials[0].mainTexture = shoes1_textures[shoes1_texture];

        }

        if (shoes == 1)
        {
            shoes2.SetActive(true);

            shoes2_texture = UnityEngine.Random.Range(0, 7);

            shoes2.GetComponent<Renderer>().materials[0].mainTexture = shoes2_textures[shoes2_texture];

        }

        if (shoes == 2)
        {
            shoes3.SetActive(true);

            shoes3_texture = UnityEngine.Random.Range(0, 6);

            shoes3.GetComponent<Renderer>().materials[0].mainTexture = shoes3_textures[shoes3_texture];

        }
        
        which_trouser = UnityEngine.Random.Range(0, 2);

        // trousers
        if(which_trouser == 0)
        {
            trousers.SetActive(true);

            trouser_texture = UnityEngine.Random.Range(0, 15);

            trousers.GetComponent<Renderer>().materials[0].mainTexture = trousers_textures[trouser_texture];
            
        }
        // short pants
        if (which_trouser == 1)
        {
            shortpants.SetActive(true);


            shortpants_texture = UnityEngine.Random.Range(0, 11);

            shortpants.GetComponent<Renderer>().materials[0].mainTexture = shortpants_textures[shortpants_texture];


        }


        // upper bosy cloth :   0 = pullover  1 = shirt    2 = t_shirt    3 = tanktop
        int upper_cloth = UnityEngine.Random.Range(0, 4);

        
        if(upper_cloth == 0)
        {
            pullover.SetActive(true);

            pullover_texture = UnityEngine.Random.Range(0, 17);

            pullover.GetComponent<Renderer>().materials[0].mainTexture = pullover_textures[pullover_texture];
        }

        if (upper_cloth == 1)
        {
            shirt.SetActive(true);

            shirt_texture = UnityEngine.Random.Range(0, 14);

            shirt.GetComponent<Renderer>().materials[0].mainTexture = shirt_textures[shirt_texture];
        }
        if (upper_cloth == 2)
        {
            t_shirt.SetActive(true);

            t_shirt_texture = UnityEngine.Random.Range(0, 21);

            t_shirt.GetComponent<Renderer>().materials[0].mainTexture = t_shirt_textures[t_shirt_texture];
        }
        if (upper_cloth == 3)
        {
            tank_top.SetActive(true);

            tank_top_texture = UnityEngine.Random.Range(0, 11);

            tank_top.GetComponent<Renderer>().materials[0].mainTexture = tank_top_textures[tank_top_texture];
        }
        
    }
    
    public void DressItem (Item item)
    
    {
        print(item.itemName);
        switch(item.itemName) // 종류 판단
        {
            case "Chain1_d":
            {   
                chain1.SetActive(true);
                chain2.SetActive(false);
                chain3.SetActive(false);

                chain1_texture = 3;
                chain1.GetComponent<Renderer>().materials[0].mainTexture = chain1_textures[chain1_texture];
                break;
            }
            case "Hair_a_c":
            {   
                hair_a.SetActive(true);
                hair_b.SetActive(false);
                hair_c.SetActive(false);
                hair_d.SetActive(false);
                hair_e.SetActive(false);
                hair_a.GetComponent<Renderer>().materials[0].mainTexture = hair_a_textures[3];
                break;
            }
            case "Pullover_p":
            {   
                jacket.SetActive(false);
                pullover.SetActive(true);
                shirt.SetActive(false);
                t_shirt.SetActive(false);
                tank_top.SetActive(false);
                pullover.GetComponent<Renderer>().materials[0].mainTexture = pullover_textures[15]; // pullover texture       
                break;
            }
            case "Shoes3_c":
            {   
                shoes1.SetActive(false);
                shoes2.SetActive(false);
                shoes3.SetActive(true);
                shoes3_texture = 2;
                shoes3.GetComponent<Renderer>().materials[0].mainTexture = shoes3_textures[shoes3_texture];
                break;
            }
            case "Shortpants_i":
            {   
                shortpants.SetActive(true);
                trousers.SetActive(false);
                shortpants_texture = 4;
                shortpants.GetComponent<Renderer>().materials[0].mainTexture = shortpants_textures[8]; // short pants texture
                break;
            }
            //Answer 2
            case "Hair_c_a":
            {   
                hair_a.SetActive(false);
                hair_b.SetActive(false);
                hair_c.SetActive(true);
                hair_d.SetActive(false);
                hair_e.SetActive(false);
                hair_c.GetComponent<Renderer>().materials[0].mainTexture = hair_c_textures[1];
                break;
            }
            case "Shirt_h":
            {   
                jacket.SetActive(false);
                pullover.SetActive(false);
                shirt.SetActive(true);
                t_shirt.SetActive(false);
                tank_top.SetActive(false);
                shirt_texture = 7;
                shirt.GetComponent<Renderer>().materials[0].mainTexture = shirt_textures[shirt_texture]; // pullover texture       
                break;
            }
            case "Shoes 1_h":
            {   
                shoes1.SetActive(true);
                shoes2.SetActive(false);
                shoes3.SetActive(false);
                shoes1_texture = 8;
                shoes1.GetComponent<Renderer>().materials[0].mainTexture = shoes1_textures[shoes1_texture]; // shoes1 texture
                break;
            }
            case "Shortpants_e":
            {   
                shortpants.SetActive(true);
                trousers.SetActive(false);
                shortpants_texture = 4;
                shoes3.GetComponent<Renderer>().materials[0].mainTexture = shoes3_textures[shoes3_texture];
                break;
            }
            //Answer3
            case "Glasses1_a":
            {   
                glasses.SetActive(true);
                glasses_texture = 3;
                glasses.GetComponent<Renderer>().materials[0].mainTexture = glasses_textures[glasses_texture]; // pullover texture   
                break;
            }
            case "Hair_d_b":
            {   
                hair_a.SetActive(false);
                hair_b.SetActive(false);
                hair_c.SetActive(false);
                hair_d.SetActive(true);
                hair_e.SetActive(false);
                hair_d.GetComponent<Renderer>().materials[0].mainTexture = hair_d_textures[1];
                break;
            }
            case "Shoes2_i":
            {   
                shoes1.SetActive(false);
                shoes2.SetActive(true);
                shoes3.SetActive(false);
                shoes2_texture = 5;
                shoes2.GetComponent<Renderer>().materials[0].mainTexture = shoes2_textures[shoes2_texture];
                break;
            }
            case "T_shirt_h":
            {   
                jacket.SetActive(false);
                pullover.SetActive(false);
                shirt.SetActive(false);
                t_shirt.SetActive(true);
                tank_top.SetActive(false);
                t_shirt_texture = 7;
                t_shirt.GetComponent<Renderer>().materials[0].mainTexture = t_shirt_textures[t_shirt_texture];
                break;
            }
            case "Trouser_e":
            {   
                shortpants.SetActive(false);
                trousers.SetActive(true);
                trouser_texture = 4;
                trousers.GetComponent<Renderer>().materials[0].mainTexture = trousers_textures[trouser_texture]; // short pants texture
                break;
            }
        }
    }

}
