using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AYSIntroController : MonoBehaviour
{
    public GameObject millionaire;

    public GameObject cam;

    public Transform cameraPoint1;
    public Transform cameraPointFinal;

    public Transform millionairePoint1;
    public Transform millionairePoint2;

    public bool isMaleMillionaire = false;

    public CharacterBehaviourManager millionaireAnim;

    public string Idle = "Idle";
    public string Walk = "Walking";
    public string Talk = "Talking";
    public string Disappointed = "Disappointed";

    public GameObject speechBubble;

    public AnimatedTyping speechBubbleTyping;

    public GameObject tapToContinue;

    [TextArea(3, 4)] public string millionaireOpeningText;

    public TextMeshProUGUI speechBubbleText;

    public AudioSource maleMillionaireOpeningAudio;
    public AudioSource femaleMillionaireOpeningAudio;

    public float millionaireAudio1Delay = 2f;

    public GameObject introScene;
    public GameObject casinoScene;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartIntroSequence", 1.5f);
        GetPresidentGender();
    }

    public void GetPresidentGender()
    {
        isMaleMillionaire = CodeManager.Instance.PlayerPrefsManager_Script.GetGender();
    }


    // Start Intro Sequence
    public void StartIntroSequence()
    {
        millionaireAnim.SetAnimation(Walk);
        millionaire.transform.DOMove(millionairePoint1.position, 2f).OnComplete(RotateMillionaire);
    }

    public void RotateMillionaire()
    {
        millionaireAnim.SetAnimation(Idle);
        millionaire.transform.DORotateQuaternion(millionairePoint1.rotation, 0.5f).OnComplete(EnableSpeechBubble);
    }


    public void EnableSpeechBubble()
    {
        speechBubble.SetActive(true);

        switch (isMaleMillionaire)
        {
            case true:
                speechBubbleText.text = millionaireOpeningText;
                millionaireAnim.SetAnimation(Talk);
                speechBubbleTyping.Animate();

                maleMillionaireOpeningAudio.Play();
                break;
            case false:
                speechBubbleText.text = millionaireOpeningText;
                millionaireAnim.SetAnimation(Talk);
                speechBubbleTyping.Animate();

                femaleMillionaireOpeningAudio.Play();
                break;
        }
    }

    //[ContextMenu("Load Next Btn")]
    public void ShowTapToContinue()
    {
        tapToContinue.SetActive(true);
    }


    public void OnSpeechEnd()
    {
        millionaireAnim.SetAnimation(Walk);
        speechBubble.SetActive(false);
        millionaire.transform.DORotateQuaternion(millionairePoint2.rotation, 0.5f).OnComplete(MillionaireWalkIntoVault);
    }

    public void MillionaireWalkIntoVault()
    {

        millionaireAnim.SetAnimation(Walk);
        millionaire.transform.DOMove(millionairePoint1.position, 2f);
        cam.transform.DOMove(cameraPoint1.position, 2f);
        cam.transform.DORotateQuaternion(cameraPoint1.rotation, 2f).OnComplete(DisableMillionaire);   
    }

    public void DisableMillionaire()
    {
        millionaire.SetActive(false);
        Invoke("StartMinigame", 1.5f);
    }


    public void StartMinigame()
    {
        // set minigame objects active

    }
}
