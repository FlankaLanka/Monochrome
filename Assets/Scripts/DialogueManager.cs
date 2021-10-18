using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject chatPanel;
    public TextMeshProUGUI speechBox;
    public TextMeshProUGUI nameBox;
    public Image avatarUI;

    private Queue<string> nameOfSpeaker;
    private Queue<string> speeches;
    private Sprite avatar;
    private AudioClip[] sounds;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        speeches = new Queue<string>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(DialogueContainer dialogue)
    {
        speeches.Clear();

        nameBox.text = dialogue.name;

        for(int i = 0; i < dialogue.speech.Length; i++)
        {
            speeches.Enqueue(dialogue.speech[i]);
        }

        avatar = dialogue.avatar;
        sounds = dialogue.sounds;

        chatPanel.SetActive(true);
        avatarUI.sprite = avatar;

        /*
        for (int i = 0; i < dialogue.name.Length; i++)
        {
            nameOfSpeaker.Enqueue(dialogue.name[i]);
        }
        */

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        audioSource.Stop();
        if (speeches.Count <= 0) //|| nameOfSpeaker.Count <= 0)
        {
            EndDialogue();
            return;
        }

        //string currentSpeaker = nameOfSpeaker.Dequeue();
        string currentSentence = speeches.Dequeue();
        StopAllCoroutines();        
        StartCoroutine(TypeSentence(currentSentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        audioSource.clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
        audioSource.Play();
        speechBox.text = "";
        foreach (Char letter in sentence.ToCharArray())
        {
            speechBox.text += letter;
            yield return null;
        }
    }


    private void EndDialogue()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        //player.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        chatPanel.SetActive(false);
    }
}
