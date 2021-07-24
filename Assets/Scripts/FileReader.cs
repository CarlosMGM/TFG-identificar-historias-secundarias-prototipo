namespace TFGNarrativa.FileManagement
{
    using UnityEngine;

    public class FileReader
    {
        //NodeList list;

        public int NodeListLoader(DialogBundle bnd, int index)
        {
            // Text data
            TextAsset data;

            if (index < bnd.dialogs.Length) {
                data = bnd.dialogs[index];
            } // if
            else
            {
                return -1;
            } // else

            // Load from JSON
            //list = JsonUtility.FromJson<NodeList>(data.text);

            return 0;
        } // NodeListLoader

        public int GetNumNodes()
        {
            //return list.nodes.Length;
            return 0;
        } // GetNumNodes

        //public Dialog GetNode(int index)
        //{
        //    if(list.nodes.Length > 0)
        //    {
        //        return list.nodes[index];
        //    } // if
        //    else
        //    {
        //        return null;
        //    } // else
        //} // GetNode
    } // FileReader
} // namespace
