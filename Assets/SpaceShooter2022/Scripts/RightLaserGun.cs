using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RightLaserGun : MonoBehaviour
{
    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GameObject _bulletHolePrefab;
    [SerializeField] private float speed;
    [SerializeField] private GameObject targetman;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private TextMeshProUGUI DelayText;

    private AudioSource laserAudioSource;
    public int right_total = 0;
    public int right_hit = 0;
    private RaycastHit hit;

    public int rightHit()
    {
        return right_hit;

    }
    public int rightTotal()
    {
        return right_total;
    }
    private void Awake()
    {
       laserAudioSource = GetComponent<AudioSource>();
    }
    public void TargetMiss()
    {
            float distanceFromPlayer = Vector3.Distance(targetman.transform.position, player.transform.position);

            float gunAngle = Math.Abs(targetman.transform.rotation.y);
            
            Vector3 perp = new Vector3(targetman.transform.position.x, targetman.transform.position.y, player.transform.position.z);

            float gunAngle_rad = (gunAngle * ((float)(Math.PI))) / 180;

            float perp_dist = Vector3.Distance(player.transform.position,perp);
            float hor_dist = Vector3.Distance(perp , targetman.transform.position);


            float final_time  =  Math.Abs((hor_dist - perp_dist*(float)(Math.Tan(gunAngle_rad)))/speed);

            DelayText.text = "Delay:" + final_time.ToString();
            // popupCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = final_time.ToString();            
            
            // Vector3 canvasPos = new Vector3(targetman.transform.position.x-2, targetman.transform.position.y+2, targetman.transform.position.z);
            // GameObject targetPopup = Instantiate(popupCanvas, canvasPos, Quaternion.identity);

            // //adjust the scale of the popup
            // targetPopup.transform.localScale = new Vector3(targetman.transform.localScale.x * (distanceFromPlayer / 10),
            //                                                  targetman.transform.localScale.y * (distanceFromPlayer / 10),
            //                                                  targetman.transform.localScale.z * (distanceFromPlayer / 10));
    }        
    public void LaserGunFired() 
    {
        //animate the gun
        laserAnimator.SetTrigger("Fire");

        //play laser gun SFX
        laserAudioSource.PlayOneShot(laserSFX);
        right_total++;

        //raycast
        if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 800f))
        {
            // GameObject obj = Instantiate(_bulletHolePrefab , hit.point , Quaternion.LookRotation(hit.normal));
            // obj.transform.position += obj.transform.forward/1000;
            if(hit.transform.GetComponent<TargetManHit>() != null)
            {
                hit.transform.GetComponent<TargetManHit>().TargetDestroyed();
            }
            else
            {
                TargetMiss();
            }
            if(hit.transform.GetComponent<TargetHit>() != null)
            {
                right_hit++;
                hit.transform.GetComponent<TargetHit>().TargetDestroyed();
            }
            
            else if(hit.transform.GetComponent<IRaycastInterface>() != null)
            {
                hit.transform.GetComponent<IRaycastInterface>().HitByRaycast();
            }

        }
    }
}
