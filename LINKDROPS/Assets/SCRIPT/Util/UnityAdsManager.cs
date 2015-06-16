using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour
{
    

    void Awake()
    {
        Advertisement.Initialize("46640");
    }

	// Use this for initialization
	void Start () 
    {
       

	}
	
	// Update is called once per frame
	void Update ()
    {
        
       
	}

    static public void CallAD()
    {
        if (Advertisement.isReady()) { Advertisement.Show(); }
    }
}
