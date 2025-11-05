using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponentInChildren<Image>();
        buttonImage.gameObject.SetActive(false);
    }

// カーソルがボタンに重なったときの処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.gameObject.SetActive(true);
    }

// カーソルがボタンから離れたときの処理
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.gameObject.SetActive(false);
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("OutdoorsScene");
    }
}
