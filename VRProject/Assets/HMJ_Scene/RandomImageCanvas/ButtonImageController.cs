using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonImageController : MonoBehaviour
{
    public Image imageToShow;
    public Sprite[] images;
    private Button playButton;
    private int randomIndex;

    public void Start()
    {
        playButton = GetComponent<Button>();
        // 이미지 배열 범위 내에서 무작위 인덱스 가져오기
        randomIndex = Random.Range(0, images.Length);

        imageToShow.sprite = null;

        // Image 컴포넌트에 무작위 이미지 할당

        // Image 컴포넌트를 활성화하여 보이도록 설정
        
    }

    public void onButtonClicked(){
        imageToShow.sprite = images[randomIndex];
        imageToShow.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

}
