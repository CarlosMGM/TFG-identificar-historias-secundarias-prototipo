using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //Objetos del inventario
    private List<Item> keyItemList = new List<Item>();
    private Dictionary<Item, int> itemList = new Dictionary<Item, int>();

    //Funcion de añadir
    public void AddItem(Item item){
        if(item.type == "key"){
            keyItemList.Add(item);
        }
        else{
            //Comprobar si ya hay
            if(!itemList.ContainsKey(item)){
                itemList.Add(item, 1);
            }
            else{
                itemList[item]++;
            }
        }
    }

    //Función de quitar
    public void RemoveItem(Item item){
        if(item.type == "key"){
            keyItemList.Remove(item);
        }
        else{
            //Comprobar si ya hay
            if(itemList.ContainsKey(item)){
                if(itemList[item] == 1){
                    itemList.Remove(item);
                }
                else{
                    itemList[item]--;
                }
            }
        }
    }
    
    //Funcion consumir/usar
    public void UseItem(Item item){
        //Realizas tu acción
        item.Effect();

        if(item.type != "key"){
            RemoveItem(item);
        }
    }

}
