using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour{
    public bool range;
    //public string interactKey = "space";
    public bool interact = false;

    public GameObject player;

    public void Start(){
        player = GameObject.FindWithTag("Player");
    }

    public virtual void Interact (){}

    void Update(){
        if(Vector3.Distance(player.transform.position, transform.position) <= 5f){
            Debug.DrawLine(player.transform.position, transform.position, Color.green);
            range = true;
        }
        else
        {
            Debug.DrawLine(player.transform.position, transform.position, Color.red);
            range = false;
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