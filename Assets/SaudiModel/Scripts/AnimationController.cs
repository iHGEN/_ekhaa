using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator MainAnimator;
    public GameObject NormalCup;
    public GameObject NormalDallah;

    public GameObject AnimationCup;
    public GameObject AnimationDallah;

    public AudioClip[] audioClips; // Array of audio clips
    private AudioSource audioSource;
    public place_detect place_Detect;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void PlayGreetingSound()
    {
            audioSource.clip = audioClips[2];
            audioSource.Play();
    }

    public void PlayCoffeeSound()
    {
            audioSource.clip = audioClips[0];
            audioSource.Play();
    }

    public void PlayDrinkingSound()
    {
            audioSource.clip = audioClips[1];
            audioSource.Play();

    }
    public void RotateCup()
    {
        MainAnimator.Play("RotateCup",1);

    }

    public void RotateCupBack()
    {
        MainAnimator.Play("RotateCupBack",1);

    }

    public void HideAnimationCupAndDisplayNormalCup()
    {
        NormalCup.SetActive(true);
        AnimationCup.SetActive(false);

    }

    public void HideNormalDallahANDCUPAndDisplayAnimationDallahANDCup()
    {
        NormalDallah.SetActive(false);
        NormalCup.SetActive(false);
        AnimationDallah.SetActive(true);
        AnimationCup.SetActive(true);
        
    }
    public void HideAnimationDallahAndDisplayNormalDallah()
    {
        place_Detect.is_finish = true;
        NormalDallah.SetActive(true);
        AnimationDallah.SetActive(false);
    }
}
