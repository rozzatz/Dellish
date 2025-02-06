using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MessageList : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    public int currentMessage;
    public int currentLetter;
    public float textDelay = 0.05f;
    public UnityEvent endEvent;
    private MessageList messageList;

    [TextArea] public string[] messages;


    private void Awake() // Finds the text output and the current list of messages to be displayed.
    {
        messageList = this.GetComponent<MessageList>();
        textOutput = GameObject.Find("TextOutput").GetComponent<TextMeshProUGUI>();
    }

    void Start() // Starts at the first message and initiates the typewriter effect for it.
    {
        currentMessage = 0;
        StartCoroutine(typewriterEffect());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // E to continue onto the next message.
        {
            StartCoroutine(NextMessage());
        }
    }

    public IEnumerator NextMessage()
    {
        if (currentMessage < messages.Length - 1) // While the current message hasn't gone through all the messages, go to the next message after the current one is done.
        {
            currentMessage += 1;
            StartCoroutine(typewriterEffect());
            yield break;
        }
        else // Once all the messages are gone through, initiate end event (turn off gameObject)
        {
            currentMessage = 0;
            endEvent.Invoke();
        }
    }


    public void UpdateMessage()
    {
        if (currentMessage < messages.Length) // Ensure currentMessage is within bounds
        {
            StartCoroutine(typewriterEffect());
        }
    }

    public void PreviousMessage() // Go back to the last message
    {
        if (currentMessage > 0) // Ensure currentMessage does not go below 0
        {
            currentMessage -= 1;
            textOutput.text = messages[currentMessage];
        }
    }

    public IEnumerator typewriterEffect() // Does the typewriter effect
    {
        StopCoroutine(typewriterEffect());

        if (currentMessage < messages.Length) // Ensure currentMessage is within bounds
        {
            string currentText = "";
            currentLetter = 0;

            textOutput.text = currentText;

            for (currentLetter = 0; currentLetter < messages[currentMessage].Length + 1; currentLetter++)
            {
                currentText = messages[currentMessage].Substring(0, currentLetter);
                textOutput.text = currentText;

                yield return new WaitForSeconds(textDelay);
            }
        }
    }
}
