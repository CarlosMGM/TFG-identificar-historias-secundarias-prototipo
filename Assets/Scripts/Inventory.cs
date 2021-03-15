using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //Objetos del inventario
    private List<Item> keyItemList;
    private Dictionary<Item, int> itemList;

    //Funcion de añadir
    public void addItem(Item item){
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
    public void removeItem(Item item){
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
    public void useItem(Item item){
        //Realizas tu acción
        item.effect();

        if(item.type != "key"){
            removeItem(item);
        }
    }

}
