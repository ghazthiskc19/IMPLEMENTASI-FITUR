using UnityEngine;
using UnityEngine.UI;
public class VolumeButtonUI : MonoBehaviour
{
    public Button VolumeButton;
    private bool isMuted = false;

    public void ToggleMuted()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            VolumeButton.interactable = false;
        }
        else
        {
            VolumeButton.interactable = true;
        }
    }
}
