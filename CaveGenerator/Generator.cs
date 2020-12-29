﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CaveGenerator
{
	internal static class Generator
	{
		internal static Cave CreateCave(Size size) => new Cave(size);

		internal static Random RND = new Random();

		internal static IEnumerable<T> CreateActors<T>(uint amount = 0) where T : Actor, new()
		{
			amount = amount == 0 ? (uint)RND.Next(1, 5) : amount;
			T[] actors = new T[amount];
			for (int i = 0; i < amount; ++i)
			{
				actors[i] = CreateActor<T>();
			}

			return actors;
		}

		internal static T CreateActor<T>(Location? loc = null) where T : Actor, new()
		{
			var cave = GameEngine.Cave;
			Tile actorTile;

			if (loc.HasValue)
			{
				actorTile = cave.Tiles[loc.Value.X, loc.Value.Y];
			}
			else
			{
				do
				{
					uint x = (uint)RND.Next(0, (int)cave.Size.Width);
					uint y = (uint)RND.Next(0, (int)cave.Size.Height);
					actorTile = cave.Tiles[x, y];
				}
				while (actorTile.IsObstacle || actorTile.IsOccupied);
			}

			T actor = new T() { OccupiedTile = actorTile };
			actor.OccupiedTile.Occupier = actor;

			return actor;
		}
	}
}
