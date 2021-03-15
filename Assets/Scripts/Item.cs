using UnityEngine;

public class Item : MonoBehaviour
{
    //Nombre del objeto
    public string name;
    //Tipo clave o consumible
    public string type;

    //Funcion de efecto
    public virtual void effect(){}

}
