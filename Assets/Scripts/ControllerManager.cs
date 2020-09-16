using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
 

    public GameObject homePanel;
    public GameObject voicePanel;
    AudioSource audioSource;
    public AudioClip clipVoice;
    private string voiceClipName;
    
    public GameObject buttonVoicePrefeb;  
    public RectTransform parentPanel;
    
    

    private Object[] resourceVoices;
    public AudioClip[] myVoices;


    void Start()
    {
        //Panel Sets
        homePanel.SetActive(true);
        voicePanel.SetActive(false);

        audioSource = this.GetComponent<AudioSource>();


    }


    void Update()
    {

    }

    public void onClickBattlePassYear()
    {

        string battleYear = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;

        resourceVoices = Resources.LoadAll("Sounds/DotaChatWhellSounds/"+battleYear, typeof(AudioClip));
        myVoices = new AudioClip[resourceVoices.Length];
        for (int i = 0; i < resourceVoices.Length; i++)
        {
            myVoices[i] = (AudioClip)resourceVoices[i];
        }

        homePanel.SetActive(false);
        voicePanel.SetActive(true);

        createVoiceButtons();

    }

    void createVoiceButtons()
    {
        int arraySize = myVoices.Length;
        for (int i = 0; i < arraySize; i++)
        {
            GameObject newButton = (GameObject)Instantiate(buttonVoicePrefeb);
            newButton.transform.SetParent(parentPanel, false);
            newButton.transform.localScale = new Vector3(1, 1, 1);

            newButton.GetComponentInChildren<Text>().text = getNormalize(myVoices[i].name.ToString());
            newButton.GetComponent<Button>().onClick.AddListener(() => OnClickVoiceButton());
            newButton.name = "NewButton" + i;
        }
    }

    public void OnClickVoiceButton()
    {
        voiceClipName= EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        AudioClip[] voiceArray = myVoices;

        int i = 0;

        while (voiceClipName != getNormalize(voiceArray[i].name.ToString()))
        {
            i++;
        }

        clipVoice = myVoices[i];
        audioSource.clip = clipVoice;
        audioSource.Play();


    }

    public void OnClickBack()
    {
        for(int i = 0; i < myVoices.Length; i++)
        {
            Destroy(GameObject.Find("NewButton" + i));
        }

        myVoices = null;
        homePanel.SetActive(true);
        voicePanel.SetActive(false);
    }




    public string getNormalize(string word)
    {
        word=word.Substring(4);
        word = word.Replace("_", " ");
        word = word.Trim();

        return word;
    }



}
