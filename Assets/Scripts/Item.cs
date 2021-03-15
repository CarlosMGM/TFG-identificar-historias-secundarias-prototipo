using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Key,
        Consumable
    }
    
    //Nombre del objeto
    [FormerlySerializedAs("name")] public string itemName;
    //Tipo clave o consumible
    public ItemType type;

    //Funcion de efecto
    public virtual void Effect(){}

}
