using TMPro;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class ButtonEvents : MonoBehaviour
{
    float originalFontSize;
    public void OnButtonDown(BaseEventData data)
    {
        // Cast the BaseEventData to PointerEventData to get more info
        PointerEventData pointerData = data as PointerEventData;
        GameObject buttonPressed = pointerData.pointerEnter;     

        if (buttonPressed != null)
        {
            if (buttonPressed.GetComponentInParent<UnityEngine.UI.Button>().interactable == false)
                return;
                       
           TMP_Text buttonText = buttonPressed.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                SFXManager.Instance.PlayBackSound();
                originalFontSize = buttonText.fontSize;
                buttonText.enableAutoSizing = false;
                buttonText.fontSize = originalFontSize / 1.5f; // or /10f as you had it
                //Debug.Log("Resized font on button: " + buttonPressed.name);
            }
            else
            {
                //Debug.LogWarning("No TMP_Text found on or under: " + buttonPressed.name);
            }
        }
    }
    public void OnButtonUp(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        GameObject buttonPressed = pointerData.pointerEnter;

        if (buttonPressed != null)
        {
            if (buttonPressed.GetComponentInParent<UnityEngine.UI.Button>().interactable == false)
                return;

            TMP_Text buttonText = buttonPressed.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {                
                buttonText.fontSize = originalFontSize;
                buttonText.enableAutoSizing = true;
            }
        }
    }

}
