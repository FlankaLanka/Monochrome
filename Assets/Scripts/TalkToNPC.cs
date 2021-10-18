using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToNPC : MonoBehaviour
{
    public DialogueContainer dialogue;

    private bool dialogueNotStarted = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && dialogueNotStarted)
            {
                dialogueNotStarted = false;

                //helps prevent a bug of player moving while talking
                //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                collision.gameObject.GetComponent<PlayerMovement>().enabled = false;

                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            dialogueNotStarted = true;
        }    
    }
}
