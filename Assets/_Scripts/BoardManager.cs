using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

	public static BoardManager instance{ get; set;}

    public int timerValue = 50;
    public Text TimerText;

	public Text WinText;
	public Material selectedMat;
	private Material previousMat;

	public ChessMan[,] chessmans{ get; set;}
	private ChessMan selectedChessman;

	public List<GameObject> chessmanPrefabs;
	private List<GameObject> activeChessman;

	const float _tileSize = 1.0f;
	const float _tileOffset = .5f;

	bool [,] allowedMoves{ get; set;}

	int selectionX = -1;
	int selectionY = -1;

	bool isWhiteTurn =  true;

	Quaternion orientation = Quaternion.Euler(0,90,0);

	// Use this for initialization
	void Start () {
		instance = this;
		SpawnAllChessmans ();
		WinText.GetComponent<Text> ().enabled = false;
        TimerText.text = ("Timer : " + timerValue);
        StartCoroutine(TimeLose());
    }
	
	// Update is called once per frame
	void Update () {
		UpdateSelection ();
		DrawChessBoard ();

        

		if (Input.GetMouseButtonDown (0)) {
			
			if (selectionX >= 0 && selectionY >= 0) {
				if (selectedChessman == null) {
					// select the chessman
					SelectChessman(selectionX, selectionY);
				} 
				else 
				{
					// Move the chessman
					MoveChessman(selectionX,selectionY);
				}
			}
		}
	}

    IEnumerator TimeLose()
    {
        while(true)
        {
            if (timerValue < 0)
            {
                EndGame();
                break;
            }

            TimerText.text = ("Timer : " + timerValue);
            yield return new WaitForSeconds(1);
            timerValue--;
           
        }
       
    }


	void SelectChessman(int x, int y)
	{
		if (chessmans [x, y] == null)
			return;

		if (chessmans [x, y].isWhite != isWhiteTurn)
			return;

		bool hasAtLeastOneMove = false;

		allowedMoves = chessmans [x, y].PossibleMove ();
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (allowedMoves [i, j])
					hasAtLeastOneMove = true;
			}
		}

		if (!hasAtLeastOneMove)
			return;

		selectedChessman = chessmans [x, y];
		previousMat = selectedChessman.GetComponent<MeshRenderer> ().material;
		selectedMat.mainTexture = previousMat.mainTexture;
		selectedChessman.GetComponent<MeshRenderer> ().material = selectedMat;
		BoardHighlighter.instance.HighlightAllowedMoves (allowedMoves);
	}


	void MoveChessman(int x, int y)
	{
		if (allowedMoves[x,y]) 
		{

			ChessMan c = chessmans [x, y];

			if (c != null && c.isWhite != isWhiteTurn) 
			{

				//If this is the King
				if (c.GetType () == typeof(King)) 
				{
					// End the game
					EndGame();
					return;
				}

				// Capture a piece
				activeChessman.Remove(c.gameObject);
				Destroy (c.gameObject);
			}

			if (selectedChessman.GetType () == typeof(Pawn)) {

				if (y == 7) {
				
					activeChessman.Remove(selectedChessman.gameObject);
					Destroy (selectedChessman.gameObject);
					SpawnChessman (1, x, y);
					selectedChessman = chessmans [x, y];
				}

				else if (y == 0) {

					activeChessman.Remove(selectedChessman.gameObject);
					Destroy (selectedChessman.gameObject);
					SpawnChessman (7, x, y);
					selectedChessman = chessmans [x, y];
				}
			}


			chessmans [selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.position = GetTileCenter (x, y);
			selectedChessman.SetPosition (x, y);
			chessmans [x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;
		}

		selectedChessman.GetComponent<MeshRenderer> ().material = previousMat;
		BoardHighlighter.instance.HideHighlights ();
		selectedChessman = null;

	}


	void UpdateSelection()
	{
		if (!Camera.main) {
			return;
		}

		RaycastHit hit;

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100.0f, LayerMask.GetMask ("ChessPlane"))) {
			Debug.DrawRay(Input.mousePosition, hit.point);
//			Debug.Log(hit.point);
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} 
		else 
		{
			selectionX = -1;
			selectionY = -1;
		}
	}


	void SpawnChessman(int index, int x , int z)
	{
		GameObject Go = Instantiate (chessmanPrefabs [index], GetTileCenter(x , z), orientation) as GameObject;
		Go.transform.parent = transform;
		chessmans [x, z] = Go.GetComponent<ChessMan>();
		chessmans [x, z].SetPosition (x, z);
		activeChessman.Add (Go);
	}


	void SpawnAllChessmans()
	{
		activeChessman = new List<GameObject> ();
		chessmans = new ChessMan[8, 8];

		// Spawn the White Team

		// King
		SpawnChessman (0,3,0);

		// Queen
		SpawnChessman (1,4,0);

		// Rooks
		SpawnChessman (2,0,0);
		SpawnChessman (2,7,0);

		// Bishops
		SpawnChessman (3,2,0);
		SpawnChessman (3,5,0);

		// Knights
		SpawnChessman (4,1,0);
		SpawnChessman (4,6,0);

		// Pawns
		for(int i = 0; i < 8; i++)
			SpawnChessman (5,i,1);

		// Spawn the Black Team

		// King
		SpawnChessman (6,4,7);

		// Queen
		SpawnChessman (7,3,7);

		// Rooks
		SpawnChessman (8,0,7);
		SpawnChessman (8,7,7);

		// Bishops
		SpawnChessman (9,2,7);
		SpawnChessman (9,5,7);

		// Knights
		SpawnChessman (10,1,7);
		SpawnChessman (10,6,7);

		// Pawns
		for(int i = 0; i < 8; i++)
			SpawnChessman (11,i,6);

	}


	Vector3 GetTileCenter(int x, int z)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (_tileSize * x) + _tileOffset;
		origin.z += (_tileSize * z) + _tileOffset;
		return origin;
	}


	void DrawChessBoard()
	{
		Vector3 widthLine = Vector3.right * 8;
		Vector3 hightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {

			Vector3 Start = Vector3.forward * i;
			Debug.DrawLine (Start, Start+ widthLine);

			for (int j = 0; j <= 8; j++) {				
				Start = Vector3.right * j;
				Debug.DrawLine (Start, Start+ hightLine);
			}
		}
		// Draw the Selection
		if (selectionX >= 0 && selectionY >= 0) {

			Debug.DrawLine (Vector3.forward * selectionY + Vector3.right * selectionX, Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine (Vector3.forward * (selectionY + 1) + Vector3.right * selectionX, Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
		}
	}


	void EndGame()
	{
        if (timerValue > 0)
        {
            if (isWhiteTurn)
            {
                print("White Player Wins");
                WinText.text = "White player Win";
            }
            else
            {
                print("Black Team Wins");
                WinText.text = "Black player Win";
            }
        }
        else
        {
            WinText.text = "Times Up";
        }


        WinText.GetComponent<Text>().enabled = true;
        foreach (GameObject go in activeChessman)
            Destroy(go);
        isWhiteTurn = true;
		BoardHighlighter.instance.HideHighlights ();
		transform.GetChild (1).gameObject.SetActive (false);
		StartCoroutine (StartGameAgain());
//		SpawnAllChessmans ();
	}


	IEnumerator StartGameAgain()
	{
		yield return new WaitForSeconds (5);
		WinText.text = "Play Again";
		yield return new WaitForSeconds (3);
		transform.GetChild (1).gameObject.SetActive (true);
		SpawnAllChessmans ();
		WinText.GetComponent<Text> ().enabled = false;
	}

	public void GameQuit()
	{
		Application.Quit ();
	}
}
