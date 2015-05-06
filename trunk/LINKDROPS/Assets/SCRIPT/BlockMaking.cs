 using UnityEngine;
using System.Collections;

public class BlockMaking : MonoBehaviour
{

	//public int Life = 3;
	public float blockGap = 0f;
	public float destroyPositionY = 0f;
	public int matchingCount = 0;
	public int scrollCount = 0;
	public bool isStarted = false;
	public bool isGameOver = false;
	public bool isOverlapPath = false;
	public bool collCheck = false;
	public bool gameStop = false;
	public float scrollSpeed;
	public float addSpeed = 0.001f;
	public float currentSpeed = 0.0f;
	public int boardSizeX;
	public int boardSizeY;
	public GameObject blockBoard;
	public int blockPathMin;
	public int blockPathMax;
	public GameObject selectedBlock;
	public GameObject[] blockContainer;
	public GameObject pingerCollider;
	public Transform destroyPos;
	//public Transform currentTouched;
	//public Transform nowTouched;
	public Canvas gameOver;
	
	// Use this for initialization
	void Start ()
	{
		gameOver.enabled = false;
		isGameOver = false;
		for (int i = 0; i < boardSizeY; ++i)
			spawnRow ();
		gameStop = false;

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetMouseButtonUp (0)) {
			if (isStarted) {
				
				isGameOver = true;
			}
		}

		if (isGameOver == true) {

			gameStop = true;			
			scrollSpeed = 0;
			addSpeed = 0;
			currentSpeed = 0;
			gameOver.enabled = true;

			
			
		}
		
		if (isStarted) {
			
			currentSpeed = Mathf.Min (scrollSpeed, currentSpeed + addSpeed);
			
			for (int i = 0; i < blockBoard.transform.childCount; ++i)
				blockBoard.transform.GetChild (i).Translate (0.0f, -currentSpeed * Time.deltaTime, 0.0f);
			
			while (blockBoard.transform.childCount > 0 && blockBoard.transform.GetChild( 0 ).transform.position.y <= destroyPos.position.y) {
				
				for (int i = 0; i < boardSizeX; ++i)
					Object.DestroyImmediate (blockBoard.transform.GetChild (0).gameObject);
				
				++scrollCount;
			}
			
			while (blockBoard.transform.childCount < boardSizeX * boardSizeY)
				spawnRow ();
		}
		
	

		if (Input.GetMouseButton (0)) {

			Debug.Log (pingerCollider.transform.position);

			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			pingerCollider.GetComponent<Transform>().position = mousePosition;
		
			//pingerCollider.transform.position = mousePosition;

			/*
			for (int i = 0; i < blockBoard.transform.childCount; ++i) {
				GameObject checkBlock = blockBoard.transform.GetChild (i).gameObject;

				if (checkBlock.tag != selectedBlock.tag)
					continue;

				if (checkBlock.collider2D == null)
					continue;

				if (checkBlock.collider2D.OverlapPoint (mousePosition)) {
					isOverlapPath = true;

					SpriteRenderer spriteRenderer = checkBlock.GetComponent<SpriteRenderer>();
					spriteRenderer.sprite = touchedSprite;
				}

				-----------------------------------------------------------------------------

				if (collCheck == true) {


					isOverlapPath = true;
				}
			}
			*/
					
			if (isOverlapPath)
				++matchingCount;
		
			if (isStarted && !isOverlapPath)
				isGameOver = true;
		
			if (!isStarted && isOverlapPath)
				isStarted = true;

			//if (Life == 0)
				//isGameOver = true;

		//Out check
		
		}

	}
	
	//	
	void spawnRow ()
	{
		float blockY = blockBoard.transform.childCount > 0 ? blockBoard.transform.GetChild (blockBoard.transform.childCount - 1).transform.position.y + 1.12f: blockBoard.transform.position.y + 0.1f;
		
		int selectedBlockCount = Random.Range (blockPathMin, blockPathMax + 1);
		int startSelectedBlockIndex = Random.Range (0, boardSizeX - selectedBlockCount);
		
		if (blockBoard.transform.childCount > 0) {
			
			int minIndex = -1;
			int maxIndex = -1;
			
			for (int i = 0; i < boardSizeX; ++i) {
				bool isSelected = blockBoard.transform.GetChild (blockBoard.transform.childCount - boardSizeX + i).tag == selectedBlock.tag ? true : false;

				if (isSelected) {
					if (minIndex == -1 || i - 1 < minIndex)
						minIndex = Mathf.Max (0, i - 1);
					
					if (maxIndex == -1 || i + 1 > maxIndex)
						maxIndex = Mathf.Min (boardSizeX - 1, i + 1);
				}
			}
			
			startSelectedBlockIndex = Random.Range (minIndex, maxIndex + 1);
		}
		
		for (int i = 0; i < boardSizeX; ++i) {
			
			GameObject spawnedBlock = null;
			
			if (i >= startSelectedBlockIndex && i < startSelectedBlockIndex + selectedBlockCount)
				spawnedBlock = Instantiate (selectedBlock) as GameObject;
			else {
				GameObject candidateBlock = null;
				while (candidateBlock == null || candidateBlock == selectedBlock)
					candidateBlock = blockContainer [Random.Range (0, blockContainer.Length)];
				
				spawnedBlock = Instantiate (candidateBlock) as GameObject;
			}
			
			spawnedBlock.transform.position = new Vector3 (blockBoard.transform.position.x + i * blockGap, blockY, 0.0f);
			spawnedBlock.transform.SetParent (blockBoard.transform);
		}
	}
	
	/*IEnumerator reset ()
	{
		if (gameStop) {
		
			if (Input.GetMouseButtonDown (0)) {
				Application.LoadLevel (0);
				
			}
			yield return null;
		}
	}*/
	
}
