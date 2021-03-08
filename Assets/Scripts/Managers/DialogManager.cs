using UnityEngine;
using UnityEngine.UI;

using TFGNarrativa.FileManagement;

public class DialogManager : MonoBehaviour
{
    // Dialog manager, to manage the different conversations and stuff
    // and changes between dialogs. 
    [Header("Dialogs configuration")]
    public int m_spaceBetweenRows = 12;
    public int m_topLimit = 5;
    public int m_bottomLimit = 10;
    static int s_numOptionsPerRow = 1;
    int m_currentNumOptions = 3;

    [Header("Dialog objects configuration")]
    public Canvas m_cnv;
    public GameObject m_dialogOptionPrefab;
    public RectTransform m_buttonZone;
    public RectTransform m_columns;
    public RectTransform m_raws;
    public DialogBundle[] m_bundles;

    [Header("Testing")]
    FileReader m_reader;

    // Start is called before the first frame update
    void Start()
    {
        m_reader = new FileReader();

        m_reader.NodeListLoader(m_bundles[0], 0);

        Debug.Log(m_reader.GetNumNodes());
        Debug.Log(m_reader);
    } // Start

    // Update is called once per frame
    void Update()
    {
        
    } // Update
} // DialogManager
