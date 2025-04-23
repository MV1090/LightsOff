using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class TouchTargetUI : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public GameObject targetImage;
    private LightObject lightObject;    
    
    public void TouchTarget()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            // Raycast into the UI



            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = touchPos
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            bool hitTarget = false;                                  

            foreach (RaycastResult result in results)
            {
                if (LightManager.Instance.playableLightObjects.Contains(result.gameObject.GetComponent<LightObject>()))
                {
                    lightObject = result.gameObject.GetComponent<LightObject>();

                    if (lightObject != null)
                    {
                        Debug.Log("Object touched");
                        if (!lightObject.GetIsLightActive())
                        {
                            if (GameManager.Instance.GetStartGame() == false)
                                return;

                            GameManager.Instance.InvokeGameOver();
                            return;
                        }
                        lightObject.OnTouched();
                        return;
                    }                  
                }
            }

            if (!hitTarget)
            {               
                if (GameManager.Instance.GetStartGame() != true)
                {
                    Debug.Log("Game Not Started");
                    return;
                }                

                GameManager.Instance.InvokeGameOver();
                {
                    Debug.Log("Missed target — GAME OVER");
                    return;
                }
            }
        }
    }
}