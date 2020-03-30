using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlighter : MonoBehaviour {

	public static BoardHighlighter instance{ get; set;}

    public GameObject highlightPrefab;
	List<GameObject> highlights;

	// Use this for initialization
	void Start () {
		instance = this;
        highlights = new List<GameObject> ();

	}


	GameObject GetHighlightObject()
	{
		GameObject go = highlights.Find (g => !g.activeSelf);

		if (go == null) 
		{
			go = Instantiate (highlightPrefab);
			highlights.Add (go);
		} 
		return go;
	}


	public void HighlightAllowedMoves(bool[,] moves)
	{
		for (int i = 0; i < 8; i++) 
		{
			for (int j = 0; j < 8; j++) 
			{
				if (moves [i, j]) 
				{
					GameObject go = GetHighlightObject ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i + .5f, 0, j + .5f);
				}
			}
		}
	}

	public void HideHighlights()
	{
		foreach (GameObject go in highlights) 
		{
			go.SetActive (false);
		}
	}

}
