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
        if(Vector3.Distance(player.transform.position, transform.position) <= 5f){
            Debug.DrawLine(player.transform.position, transform.position, Color.green);
            range = true;
            if(playerInteractionManager.objectToInteract is null)
                playerInteractionManager.objectToInteract = this;
            
        }
        else
        {
            Debug.DrawLine(player.transform.position, transform.position, Color.red);
            range = false;
            if(playerInteractionManager.objectToInteract == this)
                playerInteractionManager.objectToInteract = null;
        }
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
}