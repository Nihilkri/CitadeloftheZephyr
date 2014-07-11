using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadelOfTheZephyr {
	public class Actor {
		#region Variables
		public Bitmap sprite;
		public int x = -1, y = -1, z = -1;
		public bool dead = false;
		public int mhp, hp;
		public RectangleF BHitPlayer, FHitPlayer;
		public int damage, speed;
		public bool chestArmor;

		#endregion Variables
		public Actor(){}
		public Actor(int nx, int ny, int nhp, int ndamage, int nspeed, bool ndead) {
			x = nx;
			y = ny;
			hp = mhp = nhp;
			damage = ndamage;
			speed = nspeed;
			dead = ndead;
		} //Player

	}
}
