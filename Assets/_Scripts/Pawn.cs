using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessMan {


	public override bool [,] PossibleMove()
	{
		bool[,] r = new bool[8,8];	

		ChessMan c, c2;

		// White team Move
		if (isWhite) {
			// Diagonal left
			if (CurrentX != 0 && CurrentY != 7) {
				c = BoardManager.instance.chessmans [CurrentX - 1, CurrentY + 1];

				if (c != null && !c.isWhite) {
					r [CurrentX - 1, CurrentY + 1] = true;
				}
			}
			// Diagonal right
			if (CurrentX != 7 && CurrentY != 7) {
				c = BoardManager.instance.chessmans [CurrentX + 1, CurrentY + 1];

				if (c != null && !c.isWhite) {
					r [CurrentX + 1, CurrentY + 1] = true;
				}
			}

			//Middle
			if (CurrentY != 7) {
				c = BoardManager.instance.chessmans [CurrentX, CurrentY + 1];

				if (c == null)
					r [CurrentX, CurrentY + 1] = true;
			}

			//Middle in first Move
			if (CurrentY == 1) {
				c = BoardManager.instance.chessmans [CurrentX, CurrentY + 1];
				c2 = BoardManager.instance.chessmans [CurrentX, CurrentY + 2];

				if (c == null && c2 == null)
					r [CurrentX, CurrentY + 2] = true;
			}
		} 

		// Black Team Move
		else 
		{
			// Diagonal left
			if (CurrentX != 0 && CurrentY != 0) 
			{
				c = BoardManager.instance.chessmans [CurrentX - 1, CurrentY - 1];

				if (c != null && c.isWhite) {
					r [CurrentX - 1, CurrentY - 1] = true;
				}
			}

			// Diagonal right
			if (CurrentX != 7 && CurrentY != 0) 
			{
				c = BoardManager.instance.chessmans [CurrentX + 1, CurrentY - 1];

				if (c != null && c.isWhite) {
					r [CurrentX + 1, CurrentY - 1] = true;
				}
			}

			//Middle
			if (CurrentY != 0) {
				c = BoardManager.instance.chessmans [CurrentX, CurrentY - 1];

				if (c == null)
					r [CurrentX, CurrentY - 1] = true;
			}

			//Middle in first Move
			if (CurrentY == 6) {
				c = BoardManager.instance.chessmans [CurrentX, CurrentY - 1];
				c2 = BoardManager.instance.chessmans [CurrentX, CurrentY - 2];

				if (c == null && c2 == null)
					r [CurrentX, CurrentY - 2] = true;
			}
		}

		return r;
	}
}
