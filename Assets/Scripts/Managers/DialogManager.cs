using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Narrative_Engine;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    private static DialogManager g_instance;
    private Narrative_Engine.Dialog m_dialog;
    private GameObject character; // Character that initializes dialog
    private Narrative_Engine.Node m_current; // Current options and text
    private int m_arrowPos = 0;
    // private int m_currentBundle;
    private int m_currentIndex;
    private bool m_onDialog = false;
    private int m_currentOptionPack = 0;
    private int m_numOptionPacks = 0;
    

    // Dialog manager, to manage the different conversations and stuff
    // and changes between dialogs. 
    [Header("Dialogs configuration")]
    public int m_spaceBetweenRows = 12;
    public int m_topLimit = 5;
    public int m_bottomLimit = 10;

    [Header("Dialogue normal config.")]
    public Text m_name; // Character name
    public Text m_text; // Current text displayed
    public GameObject m_plainText;
    public GameObject m_optionContainer;


    [Header("Dialog objects configuration")]
    public Canvas m_cnv;
    public GameObject m_dialogOptionPrefab;
    public RectTransform m_optionsContainer;
    // public DialogBundle[] m_bundles;
    public Image m_selector;

    // [Header("Testing")]
    // FileReader m_reader;

    private void Awake()
    {
        if(g_instance == null)
        {
            g_instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        // g_instance.m_reader = new FileReader(); // Initialize FileReader

        // Starting with dialogs deactivated
        g_instance.m_plainText.SetActive(false);
        g_instance.m_optionContainer.transform.parent.gameObject.SetActive(false);
    } // Start

    //void FixedUpdate()
    //{
    //    Debug.Log(g_instance.m_onDialog);
    //    if (g_instance == null)
    //    {
    //        Debug.Log("Pero qué?");
    //    } // if
    //    if (Keyboard.current.kKey.wasReleasedThisFrame && g_instance.m_onDialog)
    //    {
    //        OnClick();
    //    } // if
    //} // Update

    public static DialogManager GetInstance()
    {
        return g_instance;
    }

    public bool IsOnDialog()
    {
        return g_instance.m_onDialog;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (IsOnDialog())
        {
            if (context.performed)
            {
                Debug.Log("SOMEBODY KILL ME");
                if (g_instance.m_plainText.activeSelf)
                {
                    Debug.Log("OWO");
                    g_instance.m_plainText.SetActive(false);

                    if (g_instance.m_current.options.Count > 0)
                    {
                        Debug.Log("UWU");
                        ShowOptions();
                    } // if
                    else
                    {
                        int next = g_instance.m_current.nextNode;
                        NextDialog(next);
                    } // else
                } // If the plain text is currently active, change to options
                else if (g_instance.m_optionContainer.transform.parent.gameObject.activeSelf)
                {
                    // If not, check option and get next node
                    int next = g_instance.m_current.options[g_instance.m_arrowPos * (3 * g_instance.m_currentOptionPack)].nodePtr;

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
                if (g_instance.m_optionContainer.transform.parent.gameObject.activeSelf)
                {
                    Vector2 dir = context.ReadValue<Vector2>();
                    dir.Normalize();

                    if (dir == Vector2.up)
                    {
                        if (g_instance.m_arrowPos - 1 < 0)
                        {
                            SelectorAtTop();
                        } // if
                        else
                        {
                            g_instance.m_arrowPos -= 1;
                        } // else
                    } // up
                    else if (dir == Vector2.down)
                    {
                        if (g_instance.m_arrowPos + 1 >= g_instance.m_optionContainer.transform.childCount)
                        {
                            SelectorAtBottom();
                        } // if
                        else if(!g_instance.m_optionContainer.transform.GetChild(g_instance.m_arrowPos + 1).gameObject.activeSelf)
                        {
                            SelectorAtBottom();
                        }
                        else
                        {
                            g_instance.m_arrowPos += 1;
                        } // else
                    } // down

                    g_instance.m_selector.transform.SetPositionAndRotation(
                        g_instance.m_optionsContainer.transform.GetChild(g_instance.m_arrowPos).GetChild(1).position,
                        g_instance.m_selector.transform.rotation); // Selector position
                } // if
            } // if
        } // if
    } // OnSelectorMove

    private void SelectorAtTop()
    {
        if (g_instance.m_currentOptionPack - 1 < 0)
        {
            g_instance.m_currentOptionPack = g_instance.m_numOptionPacks - 1;
            Debug.Log("Actual paquete de opciones " + g_instance.m_currentOptionPack);
            LoadNextOptionPack();
        } // if
        else
        {
            g_instance.m_currentOptionPack--;
            Debug.Log("Actual paquete de opciones " + g_instance.m_currentOptionPack);
            LoadNextOptionPack();
        } // else

        bool stop = false; 
        int i = g_instance.m_optionContainer.transform.childCount - 1;

        while (!stop && i > -1)
        {
            if (g_instance.m_optionContainer.transform.GetChild(i).gameObject.activeSelf)
            {
                g_instance.m_arrowPos = i;
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
        g_instance.m_arrowPos = 0;

        if (g_instance.m_numOptionPacks > 1)
        {
            if (g_instance.m_currentOptionPack + 1 < g_instance.m_numOptionPacks)
            {
                g_instance.m_currentOptionPack++;
                Debug.Log("Actual paquete de opciones " + g_instance.m_currentOptionPack);
                LoadNextOptionPack();
            } // if
            else
            {
                g_instance.m_currentOptionPack = 0;
                Debug.Log("Actual paquete de opciones " + g_instance.m_currentOptionPack);
                LoadNextOptionPack();
            } // else
        } // if
    } // SelectorAtBottom

    public void StartDialog(Narrative_Engine.Dialog dialog, int index, GameObject c)
    {
        if (!g_instance.m_onDialog)
        {
            Debug.Log("Starting dialog");
            // g_instance.m_reader.NodeListLoader(m_bundles[bundle], index);
            
            // g_instance.m_currentBundle = bundle;
            g_instance.m_currentIndex = index;
            g_instance.m_dialog = dialog;
            g_instance.m_current = dialog.nodes[index];
            g_instance.m_onDialog = true;
            g_instance.character = c;

            NextDialog(0);
        }
    } // StartDialogue

    public void NextDialog(int index)
    {
        if (index == -2)
        {
            g_instance.m_plainText.SetActive(false);
            g_instance.m_optionContainer.transform.parent.gameObject.SetActive(false);

            g_instance.m_onDialog = false;

            // Notify Dialog ending
            character.GetComponent<NPC>().DialogEnded();
        } // if
        else
        {
            // Si da tiempo, meter una animación 
            // g_instance.m_current = g_instance.m_reader.GetNode(index); // Get the first one
            g_instance.m_current = g_instance.m_dialog.nodes[index];
            g_instance.m_selector.gameObject.SetActive(false);

            g_instance.m_plainText.SetActive(true);
            g_instance.m_optionContainer.transform.parent.gameObject.SetActive(false);

            // g_instance.m_name.text = GameManager.GetInstance().GetCharacterName(g_instance.m_currentBundle, g_instance.m_currentIndex);
            g_instance.m_name.text = g_instance.m_current.character;
            g_instance.m_text.text = g_instance.m_current.text;
        } // else
    } // NextDialog

    public void ShowOptions()
    {
        g_instance.m_optionContainer.transform.parent.gameObject.SetActive(true);

        int options = g_instance.m_optionContainer.transform.childCount;

        for(int i = 0; i < options; i++)
        {
            g_instance.m_optionsContainer.GetChild(i).gameObject.SetActive(false);
        } // for

        InstantiateOptions();

        g_instance.m_selector.transform.SetPositionAndRotation(
            g_instance.m_optionsContainer.transform.GetChild(0).GetChild(1).position,
            g_instance.m_selector.transform.rotation); // Selector position

        g_instance.m_selector.gameObject.SetActive(true);
    } // ShowOptions

    public void LoadNextOptionPack()
    {
        int pack = g_instance.m_currentOptionPack;

        int count = g_instance.m_optionContainer.transform.childCount; // Number of options
        Transform container = g_instance.m_optionContainer.transform;

        for (int i = 0; i < count; i++)
        {
            Transform option = container.GetChild(i);
            option.gameObject.SetActive(false); // Deactivate gameObject

            if ((i + (3 * pack)) < g_instance.m_current.options.Count && g_instance.m_current.options[i + (3 * pack)] != null)
            {
                option.gameObject.SetActive(true);
                option.transform.GetChild(0).GetComponent<Text>().text = g_instance.m_current.options[i + (3 * pack)].text;
            } // if
        } // for
    } // LoadNextOptionPack

    public void InstantiateOptions()
    {
        float optCount = g_instance.m_current.options.Count; // Counter for the options text
        float count = g_instance.m_optionContainer.transform.childCount; // Number of options

        g_instance.m_numOptionPacks = Mathf.CeilToInt(optCount / count);
        g_instance.m_currentOptionPack = 0;

        Debug.Log("Numero de paquetes de opciones " + g_instance.m_numOptionPacks);

        LoadNextOptionPack();
    } // InstantiateOptions

    public void loadDialogues(Narrative_Engine.StoryScene engineScene)
    {
        NarrativeEngine.loadDialogues(engineScene);
        /*foreach (var engineDialog in engineScene.dialogs)
        {
            //TODO: PASAR DIALOGO DE MOTOR A UNITY
        }*/
    }
} // DialogManager
