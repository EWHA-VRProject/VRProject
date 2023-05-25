using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image image;
    private RectTransform rectTransform;

    private Vector2 smallSize = new Vector2(30f, 30f); // 이미지의 작은 크기
    private Vector2 targetPosition = new Vector2(15f, 15f); // 화면 상단으로 이동할 위치

    private bool isImageClicked = false;

    private void Awake()
    {
        rectTransform = image.GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Check if the image is clicked
        if (isImageClicked)
        {
            // Set the size and position of the image to the small size and target position
            rectTransform.sizeDelta = smallSize;
            rectTransform.anchoredPosition = targetPosition;
        }
        else
        {
            // Set the size and position of the image to its original size and position
            rectTransform.sizeDelta = image.sprite.rect.size;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void ImageClicked()
    {
        isImageClicked = !isImageClicked;
    }
}
