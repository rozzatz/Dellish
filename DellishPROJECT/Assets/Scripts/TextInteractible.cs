using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextInteractible : MonoBehaviour
{
    [TextArea] public string[] messages;
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onActivated;

    public MessageList messageList;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            messageList.messages = messages;
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }
}
