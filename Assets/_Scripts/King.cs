using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessMan {

	public override bool[,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		ChessMan c;


		int i, j;

		// Top Side
		i = CurrentX - 1;
		j = CurrentY + 1;
		if (CurrentY != 7) {
			for (int k = 0; k < 3; k++) {
			
				if (i >= 0 || j < 8) {
				
					c = BoardManager.instance.chessmans [i, j];
					if (c == null)
						r [i, j] = true;
					else if (isWhite != c.isWhite)
						r [i, j] = true;
				}
				i++;
			}
		}


		// Down Side
		i = CurrentX - 1;
		j = CurrentY - 1;
		if (CurrentY != 0) {
			for (int k = 0; k < 3; k++) {

				if (i >= 0 || j < 8) {

					c = BoardManager.instance.chessmans [i, j];
					if (c == null)
						r [i, j] = true;
					else if (isWhite != c.isWhite)
						r [i, j] = true;
				}
				i++;
			}
		}


		// Middle Left
		i = CurrentX - 1;
		j = CurrentY;
		if (CurrentX != 0) {
			c = BoardManager.instance.chessmans [i, j];
			if (c == null)
				r [i, j] = true;
			else if (isWhite != c.isWhite)
				r [i, j] = true;
		}


		// Middle Left
		i = CurrentX + 1;
		j = CurrentY;
		if (CurrentX != 7) {
			c = BoardManager.instance.chessmans [i, j];
			if (c == null)
				r [i, j] = true;
			else if (isWhite != c.isWhite)
				r [i, j] = true;
		}


		return r;
	}

}
