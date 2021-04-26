using UnityEngine;

public class QuestStarterNPC : NPC
{
    // Start is called before the first frame update

    public Quest quest;
    public Item givenItem;
    public bool dialogConsumed; 
    
    public override void Interact()
    {
        if(!(quest.activated || quest.used) && !dialogConsumed)
        {
            DialogManager.GetInstance().StartDialog(0, 0, gameObject);
        } // if
        // Faltaría un else if con un diálogo básico
        else
        {
            base.Interact();
        } // else
    } // Interact

    public override void DialogEnded()
    {
        Debug.Log("starting quest " + quest);
        quest.activated = true;
        QuestManager.StartQuest(quest);
    } // DialogEnded

    new void Start()
    {
        base.Start();
        quest = new Quest();
        quest.givenItem = givenItem;
    }

}
