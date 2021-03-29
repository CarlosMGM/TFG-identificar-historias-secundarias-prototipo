using UnityEngine;
public class ItemController : Interactable
{
    public Item item;
    public bool itemPickUp = false;

    public override void Interact()
    {
        item = GetComponent<Item>(); //guardamos el componente
        pickUp();
    }

    public void pickUp()
    {
        if (!itemPickUp)
        {
            //funcion de añadir al inventario
            Inventory.AddItem(item); //debería ser bool?
            //Inventory.Instance.AddItem(item)
            itemPickUp = true;
            // Eliminar del mapa
            Debug.Log("You Pick Up item:" + item.itemName);
            Destroy(gameObject);
            
        }
    }
}
