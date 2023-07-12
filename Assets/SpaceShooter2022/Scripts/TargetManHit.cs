using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TargetManHit : MonoBehaviour
{
  
    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private TextMeshProUGUI DelayText;

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    
    public void TargetDestroyed()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        popupCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PERFECT HIT";
            Debug.Log("destroyed");

            DelayText.text = "Perfect HIT";
            // Vector3 canvasPos = new Vector3(transform.position.x-2, transform.position.y+2, player.transform.position.z);
            // GameObject targetPopup = Instantiate(popupCanvas, canvasPos, Quaternion.identity);

            // //adjust the scale of the popup
            // targetPopup.transform.localScale = new Vector3(transform.localScale.x * (distanceFromPlayer / 10),
            //                                                  transform.localScale.y * (distanceFromPlayer / 10),
            //                                                  transform.localScale.z * (distanceFromPlayer / 10));
    }
    
}
