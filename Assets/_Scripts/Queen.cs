﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessMan {

	public override bool[,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		ChessMan c;
		int i, j;


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



		// Top Left
		i = CurrentX;
		j = CurrentY;

		while (true) {

			i--;
			j++;
			if (i < 0 || j >= 8)
				break;
			c = BoardManager.instance.chessmans [i, j];

			if (c == null)
				r [i, j] = true;
			else 
			{
				if(isWhite != c.isWhite)
					r [i, j] = true;

				break;
			}
		}


		// Top Right
		i = CurrentX;
		j = CurrentY;

		while (true) {

			i++;
			j++;
			if (i >= 8 || j >= 8)
				break;
			c = BoardManager.instance.chessmans [i, j];

			if (c == null)
				r [i, j] = true;
			else 
			{
				if(isWhite != c.isWhite)
					r [i, j] = true;

				break;
			}
		}

		// Down left
		i = CurrentX;
		j = CurrentY;

		while (true) {

			i--;
			j--;
			if (i < 0 || j < 0)
				break;
			c = BoardManager.instance.chessmans [i, j];

			if (c == null)
				r [i, j] = true;
			else 
			{
				if(isWhite != c.isWhite)
					r [i, j] = true;

				break;
			}
		}

		//Down right
		i = CurrentX;
		j = CurrentY;

		while (true) {

			i++;
			j--;
			if (i >= 8 || j < 0)
				break;
			c = BoardManager.instance.chessmans [i, j];

			if (c == null)
				r [i, j] = true;
			else 
			{
				if(isWhite != c.isWhite)
					r [i, j] = true;

				break;
			}
		}


		return r;
	}

}
