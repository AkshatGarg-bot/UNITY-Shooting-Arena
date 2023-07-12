using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetHit : MonoBehaviour
{
    [SerializeField] private GameObject targetExplosion;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject popupCanvas;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    
    public void TargetDestroyed()
    {
        Debug.Log("###"+ GameController.currentGameStatus);
        if(GameController.currentGameStatus == GameController.GameState.Playing)
        {   
            Instantiate(targetExplosion, transform.position, transform.rotation);
            Debug.Log("inside");
            //calculate the score for hitting this target.
            float distanceFromPlayer = Vector3.Distance(transform.position, Vector3.zero);
            int bonusPoints = (int)distanceFromPlayer;

            int targetScore = 1;

            //set our text for the popup - then instantiate the popup
            popupCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = targetScore.ToString();

            GameObject targetPopup = Instantiate(popupCanvas, transform.position, Quaternion.identity);

            //adjust the scale of the popup
            targetPopup.transform.localScale = new Vector3(transform.localScale.x * (distanceFromPlayer / 10),
                                                             transform.localScale.y * (distanceFromPlayer / 10),
                                                             transform.localScale.z * (distanceFromPlayer / 10));

            //pass score to GameController
            gameController.UpdatePlayerScore(targetScore);
        }
        
        Destroy(this.gameObject);
    }
}
