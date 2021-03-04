using UnityEngine;
public class ItemController : Interactable{
    public Item item;
    public bool itemPickUp = false;
    public override void Interact()
	{
		// base.Interact();
        item = GetComponent<Item>(); //guardamos el componente
		pickUp();
	}

    public void pickUp(){
        if(!itemPickUp){
            //funcion de añadir al inventario
            //bool itemPickedUp = Inventory.Add(item);
            itemPickUp = true;

            // Eliminar del mapa
            if (itemPickUp)
            {
                Debug.Log("You Pick Up item:" + item.name);
                Destroy(gameObject);
            }     
        }
    }
}