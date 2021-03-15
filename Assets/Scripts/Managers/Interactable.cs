using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour{
    public bool range;
    //public string interactKey = "space";
    public bool interact = false;

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
        Debug.Log("Hey, se ha pulsado una tecla");
        if (range && !interact)
        {
            Debug.Log("Esta en rango de: " + gameObject);
            Debug.Log("Interaccion");
            Interact();
            interact = true;
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