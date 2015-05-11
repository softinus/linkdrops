using UnityEngine;
using System.Collections;

[System.Serializable]  //  MonoBehaviour가 아닌 클래스에 대해 Inspector에 나타내기.
public class RowItems
{
    public GameObject[] items;
    public ArrayList arr1row = new ArrayList();

    protected int nWidth;    
    protected int nSelectTag;
    protected string strSelectTag;
    protected int nPrevPos = -1;
    protected int nFixedPos = -1;
    //protected ArrayList arrColors = new ArrayList();

    public int FixedPos
    {
        get { return nFixedPos; }
        set { nFixedPos = value; }
    }
    public int PrevPos
    {
        get { return nPrevPos; }
        set { nPrevPos = value; }
    }
    public int Width
    {
        get { return nWidth; }
        set { nWidth = value; }
    }
    public int SelectTag
    {
        get { return nSelectTag; }
        set
        {
            nSelectTag = value;
            if (nSelectTag == 0)
                strSelectTag = "green";
            else if (nSelectTag == 1)
                strSelectTag = "red";
            else if (nSelectTag == 2)
                strSelectTag = "purple";
            else if (nSelectTag == 3)
                strSelectTag = "yellow";
            else if (nSelectTag == 4)
                strSelectTag = "blue";
        }
    }
    public string Tag
    {
        get { return strSelectTag; }
    }

    // find pos of selected block
    public int GetSelectedBlockPos()
    {
        for (int i = 0; i < arr1row.Count; ++i)
        {
            GameObject GO = arr1row[i] as GameObject;
            if (GO.tag == this.Tag)
                return i;
        }
        return -1;  // if cannot find the block
    }



    // fixed block
    //public void MakeFixedBlock()
    //{
    //    int nNewPos = -1;
    //    if (nPrevPos != -1)  // if it has previous position
    //    {
    //        while (true)
    //        {
    //            nNewPos = Random.Range(nPrevPos - 1, nPrevPos + 2);
    //            if (nNewPos == -1)
    //            { }
    //            else if (nNewPos == nWidth + 1)
    //            { }
    //            else
    //                break;
    //        }
    //    }
    //    arr1row[nNewPos] = new GameObject();
    //}

    public void ChangeSelectBlockTo()
    {
        if(nPrevPos == -1)  // hasn't previous position
        {
            int nCurrSel = this.GetSelectedBlockPos();  // get location of selected block
            if (nCurrSel == -1)  // if selected block is not located in current row, should make a new one.
            {
                int nNewPos = Random.Range(0, nWidth);
                arr1row[nNewPos] = items[nSelectTag];
            }
        }
        else    // if it has previous position
        {
            int nNewPos = -1;
            while(true) // find new proper position.
            {
                nNewPos = Random.Range(nPrevPos - 1, nPrevPos + 2);
                if (nNewPos == -1)
                { }
                else if (nNewPos == nWidth)
                { }
                else
                    break;
            }

            int nCurrSel = this.GetSelectedBlockPos();  // get location of selected block
            if(nCurrSel == -1)  // if selected block is not located in current row, should make a new one.
            {
                arr1row[nNewPos] = items[nSelectTag];
            }
            else // make sure to change position between selected block and normal block
            {
                if(nNewPos != nCurrSel) // if both are same 
                {   // change respectively 
                    GameObject tmpGO = arr1row[nNewPos] as GameObject;
                    arr1row[nNewPos] = arr1row[nCurrSel];
                    arr1row[nCurrSel] = tmpGO;
                }
            }
        }
    }

    
    public void Randomly(bool bInit)
    {
        if (bInit)
            arr1row.Clear();

        // look to make the random items.
        for (int i = 0; i < nWidth; ++i)
        {
            int nIdx = 0;
            bool bExist = true;
            while (bExist)
            {
                bool bCheck = false;
                nIdx = Random.Range(0, items.Length);
                foreach (GameObject GO in arr1row)
                {
                    if (GO == items[nIdx]) // if it has already existed in list
                    {
                        bCheck = true;
                        break;
                    }
                }

                bExist = bCheck;
            }
            arr1row.Add(items[nIdx]);
        }
    }

}

public class BlockManager2 : MonoBehaviour
{
	public int nWidth= 5;
    public int nHeight = 15;
	public float fSpeed= 40f;
    public float fYdistance = 120f;

    public RowItems[] groups;

    protected float fDistance = 0.0f;
    protected int nSelectedGroup = 0;
    protected int nSelectedItem= 0;
    protected int nRowCount = 0;
    //protected GameObject gLatestGO;  // the latest game object
    public bool BeginStart = false;    // started?
	
	//Queue arr= new Queue();
    //GameObject gMainBlock;
    
	Vector3 vRootPos;

    // make main block
    private void MakeTouchBlock(float _screenWidth)
    {
        GameObject gStartBlock = Instantiate(groups[nSelectedGroup].items[nSelectedItem]);
        gStartBlock.name = "main_block";
        gStartBlock.transform.position = new Vector3(_screenWidth/2-100, 500 - fYdistance, 40);

        CircleCollider2D collider= gStartBlock.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;

        LinkedCheckMain gLinkScript= gStartBlock.AddComponent<LinkedCheckMain>();
        gLinkScript.gManager = this.gameObject; // send manager object
        gStartBlock.GetComponent<linkedCheck>().enabled = false;
        gStartBlock.GetComponent<Animator>().enabled = true;
        //gMainBlock.transform.position = GetComponent<TouchManager>().vTouchPos;
    }

    //private void AddColliderToSelectObjs()
    //{
    //    GetSelectedBlockPos();
    //    CircleCollider2D collider = gStartBlock.AddComponent<CircleCollider2D>();
    //    collider.isTrigger = true;
    //}

    private void Make1Row(int y)
    {
        GameObject gBoard = new GameObject();
        if(y<nHeight)   // first gen
        {
            gBoard = Instantiate(gBoard);
            gBoard.name = "board" + y;
            gBoard.transform.tag = "board";
            gBoard.transform.position = new Vector3(0, 500 + (fYdistance * y), 40);
        }
        else
        {
            GameObject l= GameObject.Find("board" + (y-1));
            gBoard = Instantiate(gBoard);
            gBoard.name = "board" + y;
            gBoard.transform.tag = "board";
            gBoard.transform.position = new Vector3(0, l.transform.position.y + fYdistance, 40); 
        }

        // destroy blank objects
        GameObject GOforDestory = GameObject.Find("New Game Object");
        if (GOforDestory)
            Destroy(GOforDestory);  
        
        vRootPos = gBoard.transform.position;
        Debug.Log(fDistance);

        groups[nSelectedGroup].Randomly(true);  // sorting
        groups[nSelectedGroup].ChangeSelectBlockTo();
        groups[nSelectedGroup].PrevPos = groups[nSelectedGroup].GetSelectedBlockPos();

        for (int x = 0; x < groups[nSelectedGroup].arr1row.Count; ++x)
        {
            GameObject o = null;//arr[i] as GameObject;
            o = Instantiate(groups[nSelectedGroup].arr1row[x] as GameObject,
                new Vector3(vRootPos.x + ((x + 1) * fDistance),
                    vRootPos.y,
                    100),
                    Quaternion.Euler(0, 0, 0)) as GameObject;
            o.transform.SetParent(gBoard.transform);

            if (groups[nSelectedGroup].PrevPos == x) // if it's selected color
            {
                CircleCollider2D collider = o.AddComponent<CircleCollider2D>(); // add collider
                collider.isTrigger = true;
            }
        }
    }

	void Start ()
	{
        nRowCount = nHeight;
        nSelectedGroup = Random.Range(0, groups.Length);    // select shape
        nSelectedItem = Random.Range(0, nWidth);    // select color
		//vRootPos= GameObject.Find("Manager").transform.position;
        
        foreach(RowItems BI in groups)
        {
            BI.Width = nWidth;
            BI.SelectTag = nSelectedItem;
            BI.Randomly(false);
            Debug.Log(BI.Tag);
        }
        
        var ScreenHeight = 2 * Camera.main.orthographicSize;
        var ScreenWidth  = ScreenHeight * Camera.main.aspect;
        fDistance  = ScreenWidth / (nWidth + 1);

        
        for(int y=0; y<nHeight; ++y)
        {
            Make1Row(y);            
        }

        MakeTouchBlock(ScreenWidth);
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (BeginStart == false)
            return;

        GameObject[] gBoards = GameObject.FindGameObjectsWithTag("board");
        foreach (GameObject GO in gBoards)
        {
            if (GO.transform.position.y < -50)
            {
                Destroy(GO);
                Make1Row(nRowCount++);
            }
            GO.transform.Translate(0, fSpeed * Time.deltaTime * -1, 0);
        }

        fSpeed += 0.15f;
	}
}
