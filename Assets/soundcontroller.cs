using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class soundcontroller : MonoBehaviour
{
    CharacterController cc;
    [SerializeField] public InputActionReference iar;
    [SerializeField]public AudioSource audio;
    private Vector2 position;
    // public AudioClip footstep;
    // Start is called before the first frame update
    private void test_sound(InputAction.CallbackContext obj)
    {
        Debug.Log("test_sound");
        position = obj.ReadValue<Vector2>();
        if(position != Vector2.zero && audio.isPlaying == false)
        {
            audio.Play();
        }
    }
    private void OnEnable()
    {
        Debug.Log("onEnable");
        iar.action.performed += test_sound;
    }
    private void OnDisable()
    {
        iar.action.performed -= test_sound;
    }
}
