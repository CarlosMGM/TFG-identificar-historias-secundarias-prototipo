using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    //Nombre del objeto
    [FormerlySerializedAs("name")] public string itemName;
    //Tipo clave o consumible
    public string type;

    //Funcion de efecto
    public virtual void Effect(){}

}
