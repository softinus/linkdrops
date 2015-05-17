using UnityEngine;
using System.Collections;

public class DrawlineGL : MonoBehaviour
{
    public GameObject gManager;
    public Material mat;

    

    private BlockManager2 _Mgr;
    private string strSelectedTag;

    private Vector3 startVertex;
    private Vector3 mousePos;
    private float fFactor;

    void Start()
    {
        fFactor = 0.76f;
        _Mgr = gManager.GetComponent<BlockManager2>();
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Space))
            startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);



    }
    void OnPostRender()
    {
        //if (!mat || !_Mgr)
        //{
        //    Debug.LogError("Please Assign a material on the inspector");
        //    return;
        //}

        //GameObject gStartBlock = GameObject.Find("main_block");

        //GL.PushMatrix();
        //mat.SetPass(0);
        //GL.LoadOrtho();
        //GL.Begin(GL.LINES);
        //GL.Color(Color.red);
        //for (int i = 0; i < _Mgr.arrLinked.Count; ++i)
        //{
        //    GameObject gCurrObj = null;
        //    GameObject gNextObj = null;
        //    if (_Mgr.arrLinked.Count >= 2)
        //    {
        //        if (i == _Mgr.arrLinked.Count - 1)
        //        {
        //            //    GL.Vertex(new Vector3(gStartBlock.transform.position.x / Screen.width * fFactor
        //            //, gStartBlock.transform.position.y * fFactor / Screen.height, 0));
        //        }
        //        else
        //        {
        //            gCurrObj = (GameObject)_Mgr.arrLinked[i];
        //            gNextObj = (GameObject)_Mgr.arrLinked[i + 1];
        //            Vector2 vCurrTrans = gCurrObj.transform.position;
        //            Vector2 vNextTrans = gNextObj.transform.position;
        //            GL.Vertex(new Vector3(vCurrTrans.x / Screen.width * fFactor, vCurrTrans.y / Screen.height * fFactor, 0));
        //            GL.Vertex(new Vector3(vNextTrans.x / Screen.width * fFactor, vNextTrans.y / Screen.height * fFactor, 0));
        //            GL.Vertex(new Vector3(gStartBlock.transform.position.x / Screen.width * fFactor
        //            , gStartBlock.transform.position.y * fFactor / Screen.height, 0));

        //        }

        //    }
        //    else
        //    {
        //        gCurrObj = (GameObject)_Mgr.arrLinked[i];
        //        Vector2 vTrans = gCurrObj.transform.position;
        //        GL.Vertex(new Vector3(vTrans.x / Screen.width * fFactor, vTrans.y / Screen.height * fFactor, 0));
        //        GL.Vertex(new Vector3(gStartBlock.transform.position.x / Screen.width * fFactor
        //        , gStartBlock.transform.position.y / Screen.height * fFactor, 0));
        //    }

        //}
        //GL.End();
        //GL.PopMatrix();
    }
}
