using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class HamburgerMenuButton : MonoBehaviour
{
    public Image MenuOverlay;
    public RectTransform MenuObject;
    public Vector2 MenuDefaultPosition;
    public Vector2 MenuTargetPosition;
    void Start()
    {
        MenuOverlay.DOFade(0, 0);
        MenuOverlay.enabled = false;
    }
    public void OnOpenMenu()
    {
        StartCoroutine(OpenMenuAnimation(MenuOverlay, MenuObject, .4f, .5f, .5f));
    }
    public void OnCloseMenu()
    {
        StartCoroutine(CloseMenuAnimation());
    }
    public IEnumerator CloseMenuAnimation()
    {
        // transisi ke posisi target
        MenuObject.DOAnchorPos(MenuDefaultPosition, .5f).SetEase(Ease.OutQuad).SetUpdate(true);

        // Tunggu bentar, lalu transisi panel
        yield return new WaitForSecondsRealtime(.3f);
        Time.timeScale = 1f;
        MenuOverlay.DOFade(0, .5f);
        MenuOverlay.enabled = false;
    }
    public IEnumerator OpenMenuAnimation(Image overlay, RectTransform Menu, float duration, float delayDuration, float opacity)
    {
        // buat panel jadi true 
        overlay.enabled = true;
        overlay.DOFade(opacity, duration);

        // tunggu bentar, terus pindahin ke posisi tengah (posisi target) 
        yield return new WaitForSeconds(delayDuration);
        Menu.DOAnchorPos(MenuTargetPosition, duration).SetEase(Ease.OutQuad).SetUpdate(true);
        yield return new WaitForSeconds(delayDuration);
        Time.timeScale = 0f;
    }
}
