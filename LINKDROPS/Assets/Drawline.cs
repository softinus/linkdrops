using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour
{
    public GameObject gManager;

    private BlockManager2 _Mgr;
    private string strSelectedTag;
    private LineRenderer lineRenderer;
    
    private float fD = 0;
    private Vector3 v;
 
    // Use this for initialization
    void Start () 
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.red, Color.red);
        lineRenderer.SetWidth(10f, 10f);


        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //lineRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        //lineRenderer.sortingOrder = spriteRenderer.sortingOrder;
        //lineRenderer.sortingLayerName = "UI";

        _Mgr= gManager.GetComponent<BlockManager2>();
        v = _Mgr.vStartPoint;         
    }
     
    // Update is called once per frame
    void Update () 
    {
        strSelectedTag = _Mgr.groups[_Mgr.SelectedGroup].Tag;
        lineRenderer.SetPosition(0, new Vector3(v.x, v.y, 0));
        GameObject[] arrG = GameObject.FindGameObjectsWithTag(strSelectedTag);
        for (int i = 0; i<arrG.Length; ++i )
        {
            //fD += 5.0f;
            Vector3 vCurr = arrG[i].transform.position;
            lineRenderer.SetPosition(i + 1, new Vector3(vCurr.x, vCurr.y, 0));
        }
        
    }
}
