﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessMan {

	public override bool [,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		ChessMan c;
		int i;

		// Right Move
		i = CurrentX ;
		while (true) 
		{
			i++;
			if (i >= 8)
				break;


			c = BoardManager.instance.chessmans [i, CurrentY];
			if (c == null)
				r [i, CurrentY] = true;
			else {
				if (c.isWhite != isWhite)
					r [i, CurrentY] = true;
				break;
			}
		}

		// Left Move
		i = CurrentX ;
		while (true) 
		{
			i--;
			if (i < 0)
				break;


			c = BoardManager.instance.chessmans [i, CurrentY];
			if (c == null)
				r [i, CurrentY] = true;
			else {
				if (c.isWhite != isWhite)
					r [i, CurrentY] = true;
				break;
			}
		}

		// Up Move
		i = CurrentY ;
		while (true) 
		{
			i++;
			if (i >= 8)
				break;


			c = BoardManager.instance.chessmans [CurrentX, i];
			if (c == null)
				r [CurrentX, i] = true;
			else {
				if (c.isWhite != isWhite)
					r [CurrentX, i] = true;
				break;
			}
		}

		// Down Move
		i = CurrentY ;
		while (true) 
		{
			i--;
			if (i < 0)
				break;


			c = BoardManager.instance.chessmans [CurrentX, i];
			if (c == null)
				r [CurrentX, i] = true;
			else {
				if (c.isWhite != isWhite)
					r [CurrentX, i] = true;
				break;
			}
		}

		return r;
	}

}
