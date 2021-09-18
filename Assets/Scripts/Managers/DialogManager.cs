using System.Collections.Generic;
using Narrative_Engine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class DialogManager : MonoBehaviour
    {
        private static DialogManager instance;
        private Narrative_Engine.Dialog dialog;
        private NPC character; // Character that initializes dialog
        private Narrative_Engine.Node currentNode; // Current options and text
        private int arrowPos = 0;
        // private int m_currentBundle;
        private bool onDialog = false;
        private int currentOptionPack = 0;
        private int numOptionPacks = 0;
    

        // Dialog manager, to manage the different conversations and stuff
        // and changes between dialogs. 
        [FormerlySerializedAs("m_spaceBetweenRows")] [Header("Dialogs configuration")]
        public int spaceBetweenRows = 12;
        [FormerlySerializedAs("m_topLimit")] 
        public int topLimit = 5;
        [FormerlySerializedAs("m_bottomLimit")] 
        public int bottomLimit = 10;

        [FormerlySerializedAs("m_name")] [Header("Dialogue normal config.")]
        public Text characterName; // Character name
        [FormerlySerializedAs("m_text")] 
        public Text displayedText; // Current text displayed
        [FormerlySerializedAs("m_plainText")] 
        public GameObject plainText;
        [FormerlySerializedAs("m_optionContainer")] 
        public GameObject optionContainer;


        [FormerlySerializedAs("m_cnv")] [Header("Dialog objects configuration")]
        public Canvas canvas;
        [FormerlySerializedAs("m_dialogOptionPrefab")] 
        public GameObject dialogOptionPrefab;
        [FormerlySerializedAs("m_optionsContainer")] 
        public RectTransform optionsContainer;
        // public DialogBundle[] m_bundles;
        [FormerlySerializedAs("m_selector")] 
        public Image selector;

        // [Header("Testing")]
        // FileReader m_reader;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // g_instance.m_reader = new FileReader(); // Initialize FileReader

            // Starting with dialogs deactivated
            instance.plainText.SetActive(false);
            instance.optionContainer.transform.parent.gameObject.SetActive(false);
        } // Start

        public static DialogManager GetInstance()
        {
            return instance;
        }

        public bool IsOnDialog()
        {
            return instance.onDialog;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (IsOnDialog())
            {
                if (context.performed)
                {
                    Debug.Log("SOMEBODY KILL ME");
                    if (instance.plainText.activeSelf)
                    {
                        Debug.Log("OWO");
                        instance.plainText.SetActive(false);

                        if (instance.currentNode.options.Count > 0)
                        {
                            Debug.Log("UWU");
                            ShowOptions();
                        } // if
                        else
                        {
                            int next = instance.currentNode.nextNode;
                            NextDialog(next);
                        } // else
                    } // If the plain text is currently active, change to options
                    else if (instance.optionContainer.transform.parent.gameObject.activeSelf)
                    {
                        // If not, check option and get next node
                        // int next = g_instance.m_current.options[g_instance.m_arrowPos * (3 * g_instance.m_currentOptionPack)].nodePtr;
                        int next = instance.currentNode.options[instance.arrowPos].nodePtr;

                        // Load next dialog
                        NextDialog(next);
                    } // else
                } // if
            }// if
        } // OnClick

        public void OnInput(InputAction.CallbackContext context)
        {
            if (IsOnDialog())
            {
                if (context.performed)
                {
                    if (instance.optionContainer.transform.parent.gameObject.activeSelf)
                    {
                        Vector2 dir = context.ReadValue<Vector2>();
                        dir.Normalize();

                        if (dir == Vector2.up)
                        {
                            if (instance.arrowPos - 1 < 0)
                            {
                                SelectorAtTop();
                            } // if
                            else
                            {
                                instance.arrowPos -= 1;
                            } // else
                        } // up
                        else if (dir == Vector2.down)
                        {
                            if (instance.arrowPos + 1 >= instance.optionContainer.transform.childCount)
                            {
                                SelectorAtBottom();
                            } // if
                            else if(!instance.optionContainer.transform.GetChild(instance.arrowPos + 1).gameObject.activeSelf)
                            {
                                SelectorAtBottom();
                            }
                            else
                            {
                                instance.arrowPos += 1;
                            } // else
                        } // down

                        instance.selector.transform.SetPositionAndRotation(
                            instance.optionsContainer.transform.GetChild(instance.arrowPos).GetChild(1).position,
                            instance.selector.transform.rotation); // Selector position
                    } // if
                } // if
            } // if
        } // OnSelectorMove

        private void SelectorAtTop()
        {
            if (instance.currentOptionPack - 1 < 0)
            {
                instance.currentOptionPack = instance.numOptionPacks - 1;
                Debug.Log("Actual paquete de opciones " + instance.currentOptionPack);
                LoadNextOptionPack();
            } // if
            else
            {
                instance.currentOptionPack--;
                Debug.Log("Actual paquete de opciones " + instance.currentOptionPack);
                LoadNextOptionPack();
            } // else

            bool stop = false; 
            int i = instance.optionContainer.transform.childCount - 1;

            while (!stop && i > -1)
            {
                if (instance.optionContainer.transform.GetChild(i).gameObject.activeSelf)
                {
                    instance.arrowPos = i;
                    stop = true;
                } // if
                else
                {
                    i--;
                } // else
            } // while
        } // SelectorAtTop

        private void SelectorAtBottom()
        {
            instance.arrowPos = 0;

            if (instance.numOptionPacks > 1)
            {
                if (instance.currentOptionPack + 1 < instance.numOptionPacks)
                {
                    instance.currentOptionPack++;
                    Debug.Log("Actual paquete de opciones " + instance.currentOptionPack);
                    LoadNextOptionPack();
                } // if
                else
                {
                    instance.currentOptionPack = 0;
                    Debug.Log("Actual paquete de opciones " + instance.currentOptionPack);
                    LoadNextOptionPack();
                } // else
            } // if
        } // SelectorAtBottom

        public void StartDialog(Dialog dialog, int index, NPC c)
        {
            if (!instance.onDialog)
            {
                Debug.Log("Starting dialog");
                // g_instance.m_reader.NodeListLoader(m_bundles[bundle], index);
            
                // g_instance.m_currentBundle = bundle;
                instance.dialog = dialog;
                instance.currentNode = dialog.nodes[index];
                instance.onDialog = true;
                instance.character = c;

                NextDialog(0);
            }
        } // StartDialogue

        public void NextDialog(int index)
        {
            if (index < -1)
            {
                instance.plainText.SetActive(false);
                instance.optionContainer.transform.parent.gameObject.SetActive(false);

                instance.onDialog = false;

                switch (index)
                {
                    case -2:
                        // Notify Dialog ending
                        character.DialogEnded(true);
                        break;
                    case -3:
                        character.DialogEnded(false);
                        break;
                } // switch
            } // if
            else
            {
                // Si da tiempo, meter una animación 
                // g_instance.m_current = g_instance.m_reader.GetNode(index); // Get the first one
                instance.currentNode = instance.dialog.nodes[index];
                instance.selector.gameObject.SetActive(false);

                instance.plainText.SetActive(true);
                instance.optionContainer.transform.parent.gameObject.SetActive(false);

                // g_instance.m_name.text = GameManager.GetInstance().GetCharacterName(g_instance.m_currentBundle, g_instance.m_currentIndex);
                instance.characterName.text = instance.currentNode.character;
                instance.displayedText.text = instance.currentNode.text;
            } // else
        } // NextDialog

        public void ShowOptions()
        {
            instance.arrowPos = 0;
            instance.optionContainer.transform.parent.gameObject.SetActive(true);

            int options = instance.optionContainer.transform.childCount;

            for(int i = 0; i < options; i++)
            {
                instance.optionsContainer.GetChild(i).gameObject.SetActive(false);
            } // for

            InstantiateOptions();

            instance.selector.transform.SetPositionAndRotation(
                instance.optionsContainer.transform.GetChild(0).GetChild(1).position,
                instance.selector.transform.rotation); // Selector position

            instance.selector.gameObject.SetActive(true);
        } // ShowOptions

        public void LoadNextOptionPack()
        {
            int pack = instance.currentOptionPack;

            int count = instance.optionContainer.transform.childCount; // Number of options
            Transform container = instance.optionContainer.transform;

            for (int i = 0; i < count; i++)
            {
                Transform option = container.GetChild(i);
                option.gameObject.SetActive(false); // Deactivate gameObject

                if ((i + (3 * pack)) < instance.currentNode.options.Count && instance.currentNode.options[i + (3 * pack)] != null)
                {
                    option.gameObject.SetActive(true);
                    option.transform.GetChild(0).GetComponent<Text>().text = instance.currentNode.options[i + (3 * pack)].text;
                } // if
            } // for
        } // LoadNextOptionPack

        public void InstantiateOptions()
        {
            float optCount = instance.currentNode.options.Count; // Counter for the options text
            float count = instance.optionContainer.transform.childCount; // Number of options

            instance.numOptionPacks = Mathf.CeilToInt(optCount / count);
            instance.currentOptionPack = 0;

            Debug.Log("Numero de paquetes de opciones " + instance.numOptionPacks);

            LoadNextOptionPack();
        } // InstantiateOptions

        public void LoadDialogues(Narrative_Engine.StoryScene engineScene)
        {
            NarrativeEngine.LoadDialogues(engineScene);
        } // loadDialogues

        public void LoadGenericDialogsByPlace(string place)
        {
            List<Dialog> genericDialogs = NarrativeEngine.LoadGenericDialogs(place);

            foreach(Dialog dialog in genericDialogs)
            {
                GameObject character = GameObject.Find(dialog.init);
                if(character != null)
                {
                    if(character.GetComponent<QuestStarterNPC>())
                    {
                        character.GetComponent<QuestStarterNPC>().GenericDialog = dialog;
                    }
                    else if (character.GetComponent<QuestFinisherNPC>())
                    {
                        character.GetComponent<QuestFinisherNPC>().GenericDialog = dialog;
                    }
                    else
                    {
                        character.AddComponent<NPC>().GenericDialog = dialog;
                    }
                }
            }
        }
    }
} // DialogManager
