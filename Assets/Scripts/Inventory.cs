﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //Objetos del inventario
    private static List<Item> keyItemList = new List<Item>();
    private static Dictionary<Item, int> itemList = new Dictionary<Item, int>();

    //Funcion de añadir
    public static void AddItem(Item item){
        if(item.type == Item.ItemType.Key){
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
    public static void RemoveItem(Item item){
        if(item.type == Item.ItemType.Key){
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

    public static bool FindItem(Item item)
    {
        if (item.type == Item.ItemType.Key)
            return keyItemList.Contains(item);
        else
            return itemList.ContainsKey(item);
    }
    
    //Funcion consumir/usar
    public static void UseItem(Item item){
        //Realizas tu acción
        item.Effect();

        if(item.type == Item.ItemType.Consumable){
            RemoveItem(item);
        }
    }

}
