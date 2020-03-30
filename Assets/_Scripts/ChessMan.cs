using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessMan : MonoBehaviour 
{
	public int CurrentX{ get; set;}
	public int CurrentY{ get; set;}
	public bool isWhite;

	public void SetPosition(int x, int z)
	{
		CurrentX = x;
		CurrentY = z;
	}


	public virtual bool[,] PossibleMove()
	{
		return new bool[8,8];
	}
}
