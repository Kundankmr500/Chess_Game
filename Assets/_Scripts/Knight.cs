using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessMan {

	public override bool[,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		//Up left
		KnightMove(CurrentX - 1, CurrentY + 2, ref r);

		//Up right
		KnightMove(CurrentX + 1, CurrentY + 2, ref r);

		//Right Up
		KnightMove(CurrentX + 2, CurrentY + 1, ref r);

		//Right Down
		KnightMove(CurrentX + 2, CurrentY - 1, ref r);

		//Down left
		KnightMove(CurrentX - 1, CurrentY - 2, ref r);

		//Down right
		KnightMove(CurrentX + 1, CurrentY - 2, ref r);

		//Left Up
		KnightMove(CurrentX - 2, CurrentY + 1, ref r);

		//Left Down
		KnightMove(CurrentX - 2, CurrentY - 1, ref r);



		return r;
	}


	public void KnightMove(int x, int y, ref bool[,] r)
	{
		ChessMan c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) 
		{
			c = BoardManager.instance.chessmans [x, y];
				
				if(c == null)
					r[x,y] = true;
				else if (isWhite != c.isWhite)
					r[x,y] = true;
		}
			
	}

}
