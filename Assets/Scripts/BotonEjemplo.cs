using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonEjemplo : MonoBehaviour
{
    /*
     * Vale, este script la idea es que se pueda utilizar en todos lo botones 
     * por igual simplemente cambiando algunas cosas, entonces necesitas meter
     * una serie de variables que se puedan cambiar desde el editor. 
     */
    public GameObject tick; // Este sería el tick
    // En este caso, aquí iría asignada la imagen que vas a cambiar del tick. 
    // Esto lo haces desde el editor, arrastrando el objeto hasta donde esté la opción.

    // Start is called before the first frame update
    void Start()
    {
        Button btn;

        // Esto es para evitar que haya errores asignando cosas
        // Se puede poner para que lo detecte desde el editor, 
        // pero eso ya en otro momento jajaj
        if (gameObject.GetComponent<Button>())
        {
            /*
             * Vale, esto lo que va a hacer es comprobar que el objeto al que le hayas asignado el componente sea un botón. Entonces
             * pillará el componente del botón y le asignará una función para que se llame cada vez que el botón se pulse. Esa 
             * función puede ser lo que tu quieras, puede instanciar objetos, cambiar sprites, mover cosas, activar componentes, etc.
             */
            btn = gameObject.GetComponent<Button>(); // Pilla componente.
            btn.onClick.AddListener(ChangeTick); // Añade una función que escuche el evento de pulsado del botón.
        } // if
        else
        {
            Debug.LogError("A ver, si esto no es un botón, no le pongas el componente del botón...");
        } // else
    } // Start

    void ChangeTick()
    {
        /*
         * Entonces, esta es la función que se va a ejecutar cada vez que el botón se pulse. Cuando digo botón, es cualquier botón. 
         * Aquí es donde controlarías el estado del tick. Aquí es donde entra la diferencia de cómo lo tengas configurado. Por lo que
         * me has dicho creo que lo tienes así:
         * 
         * Tienes 2 GameObjects: La cajita que contiene el tick y como hijo de esa caja tienes la imagen que representa el tick. Entonces, 
         * para saber si está activado o no, haces lo siguiente: 
         */
        if (tick.activeSelf)
        {
            tick.SetActive(false);
        } // if
        else
        {
            tick.SetActive(true);
        } // else
        /*
         * Con esto lo que haces es activar o desactivar el tick en función de cómo este en este momento. 
         */

    } // ChangeTick
} // BotonEjemplo
