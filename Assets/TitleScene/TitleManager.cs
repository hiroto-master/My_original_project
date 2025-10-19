using UnityEngine;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponentInChildren<Image>();
        buttonImage.gameObject.SetActive(false);
    }

// カーソルがボタンに重なったときの処理
    public void OnPointerEnter()
    {
        buttonImage.gameObject.SetActive(true);
    }

// カーソルがボタンから離れたときの処理
    public void OnPointerExit()
    {
        buttonImage.gameObject.SetActive(false);
    }

}
