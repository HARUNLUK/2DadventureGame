using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public bool isInRange;
    public KeyCode keyCode;
    public bool KeyPressed = false;
    public bool isSeller = false;
    public UnityEvent interactAction;
    public GameObject Info;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (isSeller)
                {
                    interactAction.Invoke();
                }else if (!KeyPressed)
                {
                    KeyPressed = true;
                    interactAction.Invoke();
                }

            }
        }
    }
    public virtual void Interacted()
    {
        Debug.Log("Interact");
    }
    void ShowInfo()
    {
        if (Info == null)
        {
            return;
        }
        if (KeyPressed)
        {
            return;
        }
        Info.SetActive(true);
    }
    void HideInfo()
    {
        if (Info == null)
        {
            return;
        }
        Info.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            isInRange = true;
            ShowInfo();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            isInRange = false;
            HideInfo();
        }
    }
}
