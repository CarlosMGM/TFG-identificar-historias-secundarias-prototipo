using UnityEngine;
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
            range = true;
        }
        else{
            range = false;
        }
        if(range && !interact){
            Debug.Log("Esta en rango de: " +  gameObject);
            //if(Input.GetKeyDown(KeyCode.Space)){
                //Debug.Log("Interaccion");
                Interact();
                //interact = true;
           // }
        }
    }
}