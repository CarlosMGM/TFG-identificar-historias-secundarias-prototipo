using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFGNarrativa.Dialog
{
    [System.Serializable]
    public class NodeList
    {
        public Dialog[] nodes;
    } // NodeList

    [System.Serializable]
    public class Option
    {
        public int nodePtr;
        public string text;
    } // Option

    [System.Serializable]
    public class Dialog
    {
        // Dialog object, manages a dialog, having it's options and
        // texts and deploying them on screen. 
        public int character;
        public int emotion;
        public int nextNode;
        public string text;
        public Option[] options;

        // Class getters
        int GetCharacter()
        {
            return character;
        }

        int GetNextNode()
        {
            return nextNode;
        }

        int GetEmotion()
        {
            return emotion;
        }

        string GetText()
        {
            return text;
        }

        // Option getters
        int GetNumOptions()
        {
            return options.Length;
        } // GetNumOptions

        Option GetOption(int index)
        {
            if (options.Length > 0)
            {
                return options[index];
            } // if
            else
            {
                return null;
            } // else
        } // GetOption

        int GetNodePtrOption(int index)
        {
            if (options.Length > 0)
            {
                return options[index].nodePtr;
            } // if
            else
            {
                return -1;
            } // else
        } // GetNodePtrOption

        string GetOptionText(int index)
        {
            if (options.Length > 0)
            {
                return options[index].text;
            } // if
            else
            {
                return "";
            } // else
        } // GetOptionText
    } // Dialog
} // namespace
