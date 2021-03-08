namespace TFGNarrativa.FileManagement
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Dialogs", menuName = "DialogBundle")]
    public class DialogBundle : ScriptableObject
    {
        public TextAsset[] dialogs;

        /*
         Aquí habría que meter más info, como por ejemplo a qué capítulo pertenecen y demás, entra dentro ya de toda la parte de entendimiento del motor. Problema que puede
         suponer esto: esto haría que el motor ya no sea independiente de Unity. Sin embargo, esto se puede hacer independiente de alguna manera. Por ejemplo, que sea el 
         propio motor el que instancie esos objetos(?) en el proyecto con un par de opciones que le demos. Habría que testearlo y prototiparlo y demás. No debería ser muy 
         complejo, pero aún así. 
         */

    } // DialogBundle
} // namespace
