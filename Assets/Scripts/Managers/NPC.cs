using UnityEngine;
public class NPC : Interactable{

    private bool dialogDone;

    public override void Interact(){
        if(!dialogDone){
            dialogDone = true;
            Debug.Log("Dialog is done");
        }
    }
}