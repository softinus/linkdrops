using UnityEngine;
using System.Collections;

public class BlockManager2 : MonoBehaviour
{

	public int nWidth= 5;
	public float fSpeed= 40f;
	//List<GameObject> arr= new List<GameObject>();
	//ArrayList arr= new ArrayList();	// 뭔가 바뀐듯 그냥 형을 안 집어넣고
    public GameObject[] arr;
	ArrayList arr1row= new ArrayList();
	//Queue arr= new Queue();

	Vector3 vRootPos;

	void Start ()
	{
		vRootPos= GameObject.Find("Manager").transform.position;

        for (int i = 0; i < nWidth; ++i)
        {
            int nIdx = 0;
            bool bExist = true;
            while (bExist)
            {
                bool bCheck = false;
                nIdx = Random.Range(0, arr.Length);
                foreach (GameObject GO in arr1row)
                {
                    if (GO == arr[nIdx]) // if it has already existed in list
                    {
                        bCheck = true;
                        break;
                    }
                }

                bExist = bCheck;
            }

            //GameObject o = arr[nIdx];
            //o.transform.position= vRootPos;
            //o.transform.Translate(10,0,100);
            //o.transform.position= new Vector3(vRootPos.x+((i+1)*0.45f), vRootPos.y, 100);

            arr1row.Add(arr[nIdx]);

        }

        //unsafe
        //{
        //    string str;
        //    //int a;
        //    str= string.Format("{0:X}", arr[0]);
        //    Debug.Log(str);
        //}

        GameObject gManager = GameObject.Find("Manager");
        for (int i = 0; i < arr1row.Count; ++i)
		{
            GameObject o = null;//arr[i] as GameObject;
			//o.tag= "balls";

            o = Instantiate(arr1row[i] as GameObject, new Vector3(vRootPos.x + ((i + 1) * 70f), vRootPos.y, 100), Quaternion.Euler(0, 0, 0)) as GameObject;
            o.transform.SetParent(gManager.transform);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
        //foreach(GameObject GO in arr1row)
        //{
        //    Debug.Log(GO.name);
        //    //Debug.Log(GO.transform.position.y);
        //    GO.transform.Translate(0, fSpeed * Time.deltaTime, 0);
        //}
        GameObject gManager = GameObject.Find("Manager");
        gManager.transform.Translate(0, fSpeed * Time.deltaTime * -1, 0); 

        //GameObject[] arrBalls= GameObject.FindGameObjectsWithTag("Player");
        //foreach(GameObject GO in arrBalls)
        //{
        //    GO.transform.Translate(0, fSpeed * Time.time, 0);
        //}


        //int nIdx= Random.Range(0, arr.Count);
		//Instantiate((GameObject)arr[nIdx], new Vector3(vRootPos.x, vRootPos.y, 100), Quaternion.Euler(0, 0, 0));

	}
}
