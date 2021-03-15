using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour{
    public bool range;

    public GameObject player;
    private PlayerInteractionManager playerInteractionManager;

    public void Start(){
        player = GameObject.FindWithTag("Player");
        playerInteractionManager = player.GetComponent<PlayerInteractionManager>();
        
    }

    public virtual void Interact (){}

    void Update(){
        
    }

    public void InputReceived(InputAction.CallbackContext c)
    {
        if (range)
        {
            Debug.Log("Interaccion con " + gameObject);
            Interact();
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con: " + gameObject);
            range = true;
            playerInteractionManager.objectToInteract = this;
        }
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            range = false;
            if(playerInteractionManager.objectToInteract == this)
                playerInteractionManager.objectToInteract = null;
        }
    }
}