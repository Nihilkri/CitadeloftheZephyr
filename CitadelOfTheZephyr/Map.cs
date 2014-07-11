using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadelOfTheZephyr {
	class Map {
		public struct MapDef {
			public bool Solid;		//Sets whether you can pass through an object
			public bool Healing; 	//Allows Player to Heal
			public bool UpStairs;	//Goes up a level
			public bool DownStairs;	//Goes down a level
		} //MapDef
		public static MapDef[,,] MapDat = new MapDef[30, 15, 5];			//Map size

	}
}
