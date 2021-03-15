using UnityEngine;
using UnityEngine.UI;

using TFGNarrativa.FileManagement;
using TFGNarrativa.Dialog;

public class DialogManager : MonoBehaviour
{
    private Dialog m_current; // Current options and text
    private int m_currentOption = 0;
    private int m_currentBundle, m_currentIndex;

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
    public RectTransform m_columns;
    public DialogBundle[] m_bundles;
    public Image m_selector;

    [Header("Testing")]
    FileReader m_reader;

    // Start is called before the first frame update
    void Start()
    {
        m_reader = new FileReader(); // Initialize FileReader

        StartDialog(0, 0);
    } // Start

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClick();
        } // if
        if (m_current.GetNumOptions() > 0 && m_optionContainer.activeSelf)
        {
            OnSelectorMove();
        }
    } // Update

    public void OnClick()
    {
        if (m_plainText.activeSelf)
        {
            m_plainText.SetActive(false);

            if (m_current.GetNumOptions() > 0)
            {
                ShowOptions();
            }
            else
            {
                int next = m_current.GetNextNode();
                NextDialog(next);
            }
        } // If the plain text is currently active, change to options
        else if (m_optionContainer.activeSelf)
        {
            // If not, check option and get next node
            int next = m_current.GetOption(m_currentOption).nodePtr;

            // Load next dialog
            NextDialog(next);
        } // else
    } // OnClick

    public void OnSelectorMove()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(m_currentOption - 1 < 0)
            {
                m_currentOption = m_current.GetNumOptions() - 1;
            } // if
            else
            {
                m_currentOption -= 1;
            } // else
        } // up
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (m_currentOption + 1 >= m_current.GetNumOptions())
            {
                m_currentOption = 0;
            } // if
            else
            {
                m_currentOption += 1;
            } // else
        } // down

        m_selector.transform.SetPositionAndRotation(
            m_columns.transform.GetChild(m_currentOption).GetChild(1).position,
            m_selector.transform.rotation); // Selector position
        // TODO: Play a sound (for example)
    } // OnSelectorMove

    public void StartDialog(int bundle, int index)
    {
        m_reader.NodeListLoader(m_bundles[bundle], index);
        m_currentBundle = bundle;
        m_currentIndex = index;

        NextDialog(0);
    } // StartDialogue

    public void NextDialog(int index)
    {
        if(index == -2)
        {
            Debug.Log("End of Dialog");

            m_plainText.SetActive(false);
            m_optionContainer.SetActive(false);
        } // if
        else
        {
            // Si da tiempo, meter una animación 
            m_current = m_reader.GetNode(index); // Get the first one
            m_selector.gameObject.SetActive(false);

            m_plainText.SetActive(true);
            m_optionContainer.SetActive(false);

            m_name.text = GameManager.GetInstance().GetCharacterName(m_currentBundle, m_currentIndex);
            m_text.text = m_current.GetText();
        } // else
    } // NextDialog

    public void ShowOptions()
    {
        m_optionContainer.SetActive(true);
        InstantiateOptions();

        m_selector.transform.SetPositionAndRotation(
            m_columns.transform.GetChild(0).GetChild(1).position, 
            m_selector.transform.rotation); // Selector position
        m_selector.gameObject.SetActive(true);
    } // ShowOptions

    public void InstantiateOptions()
    {
        GameObject o;

        float posOrY = m_columns.position.y + (m_columns.rect.height / m_current.GetNumOptions());

        for (int i = 0; i < m_current.GetNumOptions(); i++)
        {
            o = Instantiate(m_dialogOptionPrefab, m_columns.transform);
            float posY = posOrY - o.GetComponent<RectTransform>().rect.height * i;

            Vector3 pos = new Vector3(m_columns.transform.position.x, posY, m_columns.transform.position.z);
            o.transform.SetPositionAndRotation(pos, o.transform.rotation);
            o.transform.GetChild(0).GetComponent<Text>().text = m_current.GetOptionText(i);
        } // for
    } // InstantiateOptions
} // DialogManager
