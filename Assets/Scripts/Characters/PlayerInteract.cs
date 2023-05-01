using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float offsetX;
    public float offsetY;
    public float distance;

    Vector3 offsetPos;
    public LayerMask LayerMask;

    private bool canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Instance.Subscribe<DialogueEvent>(typeof(DialogueEvent), onDialogue);
    }
    private void OnDrawGizmos()
    {
        offsetPos = transform.position + new Vector3(offsetX, offsetY, 0);
        Gizmos.DrawLine(offsetPos, offsetPos + Vector3.right * distance);
    }
    void checkInteract()
    {
        if (GameModel.Instance.CanInteract&&!GameModel.Instance.InTimeLine)
        {
            offsetPos = transform.position + new Vector3(offsetX, offsetY, 0);
            var hit = Physics2D.Raycast(offsetPos, Vector2.right, distance, LayerMask);
            if (hit.collider != null&& InteractPromptManager.Instance != null)
            {
                InteractPromptManager.Instance.ShowPrompt("Talk<E>", hit.collider.transform.position + Vector3.up);
            }
            else if(InteractPromptManager.Instance!=null)
            {
                InteractPromptManager.Instance.HidePrompt();
            }
            if (hit.collider != null && Input.GetKeyDown(KeyCode.E))
            {
                InteractPromptManager.Instance.HidePrompt();
                hit.collider.GetComponent<IInteractable>().Interact();
            }
        }
        else
        {
            InteractPromptManager.Instance.HidePrompt();
        }
    }
    public void Interact()
    {
        Debug.Log("interact");
        offsetPos = transform.position + new Vector3(offsetX, offsetY, 0);
        var hit = Physics2D.Raycast(offsetPos, Vector2.right, distance, LayerMask);
        if (hit.collider != null)
        {
            InteractPromptManager.Instance.HidePrompt();
            hit.collider.GetComponent<IInteractable>().Interact();
        }
    }
    void onDialogue(DialogueEvent e)
    {
        if (e.IsTalking)
        {
            GameModel.Instance.CanInteract = false;
        }
        else
        {
            GameModel.Instance.CanInteract = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkInteract();
    }
    private void OnDestroy()
    {
        EventSystem.Instance.Unsubscribe<DialogueEvent>(typeof(DialogueEvent), onDialogue);
    }
}
