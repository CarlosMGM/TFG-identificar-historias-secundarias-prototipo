using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour{
    public bool range;

    public GameObject player;
    private PlayerInteractionManager playerInteractionManager;

    public virtual void Start(){
        player = GameObject.FindWithTag("Player");
        playerInteractionManager = player.GetComponent<PlayerInteractionManager>();
        
    }

    public virtual void Interact (){}

    public void InputReceived(InputAction.CallbackContext c)
    {
        if (range)
        {
            Debug.Log("Interaccion con " + gameObject);
            Interact();
            range = false;
            playerInteractionManager.objectToInteract = null;
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Colisión con: " + gameObject);
        if (col.gameObject.CompareTag("Player"))
        {
            range = true;
            playerInteractionManager.objectToInteract = this;
        }
    }

    public virtual void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            range = false;
            if(playerInteractionManager.objectToInteract == this)
                playerInteractionManager.objectToInteract = null;
        }
    }
}