using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitadelOfTheZephyr {
	public partial class CotZ : Form {
		#region Variables
		#region Graphics
		#region Form
		public static int tileSize = 64;			//Sets the space between tiles
		public static int fx = 15 * tileSize + 16;	//Form Width
		public static int fy = 15 * tileSize + 38;	//Form Height
		public static Graphics gb, gf;
		public static Bitmap gi;					//Graphics objects
		#endregion Form
		#region Sprites
		#region Paths
		public static string Location_Sprites = "\\\\ZYKRONOS\\Programming\\Sprites\\";
		public static string Tile_Sprites = Location_Sprites + "Tiles\\";
		public static string Screen_Sprites = Location_Sprites + "Screens\\";
		public static string Item_Sprites = Location_Sprites + "Items\\";
		public static string Character_Sprites = Location_Sprites + "Characters\\";
		public static string Spawner_Sprites = Tile_Sprites + "Spawners\\";
		public static string Player_Sprites = Character_Sprites + "Player\\";
		public static string SpawnPlayer_Sprites = Spawner_Sprites + "Player\\";
		public static string Enemy_Sprites = Character_Sprites + "Enemy\\";
		public static string SpawnEnemy_Sprites = Spawner_Sprites + "Enemy\\";
		#endregion Paths
		#region Map
		#region Tiles
		#region Basic
		public static string Basic_Sprites = Tile_Sprites + "Basic\\";
		public static Bitmap Tile = new Bitmap(Basic_Sprites + "Tile.png");
		public static Bitmap Crate = new Bitmap(Basic_Sprites + "Crate.png");
		public static Bitmap Grass = new Bitmap(Basic_Sprites + "Grass.png");
		public static Bitmap LeftRed = new Bitmap(Basic_Sprites + "Red Left.png");
		public static Bitmap MidRed = new Bitmap(Basic_Sprites + "Red Middle.png");
		public static Bitmap RightRed = new Bitmap(Basic_Sprites + "Red Right.png");
		#endregion Basic
		#region Stat
		public static string Stat_Sprites = Tile_Sprites + "Stat\\";
		public static Bitmap Road = new Bitmap(Stat_Sprites + "Road.png");
		public static Bitmap Water = new Bitmap(Stat_Sprites + "Water.png");
		public static Bitmap HealthPack = new Bitmap(Stat_Sprites + "Health.png");
		#endregion Stat
		#region Level
		public static string Level_Sprites = Tile_Sprites + "Level\\";
		public static Bitmap Ascend = new Bitmap(Level_Sprites + "ASCEND.png");
		public static Bitmap Descend = new Bitmap(Level_Sprites + "DESCEND.png");
		#endregion Level
		#region System
		public static string System_Sprites = Tile_Sprites + "System\\";
		public static Bitmap Mapdat = new Bitmap(System_Sprites + "Map.png");
		public static Bitmap ErrorTile = new Bitmap(System_Sprites + "ERROR.png");
		#endregion System
		#region Spawners
		#region Player
		public static Bitmap Start = new Bitmap(SpawnPlayer_Sprites + "START.png");
		#endregion Player
		#region Enemy
		public static Bitmap SpawnBoss = new Bitmap(SpawnEnemy_Sprites + "Spawn Boss.png");
		public static Bitmap SpawnSlime = new Bitmap(SpawnEnemy_Sprites + "Spawn Slime.png");
		public static Bitmap SpawnBat = new Bitmap(SpawnEnemy_Sprites + "Spawn Bat.png");
		#endregion Enemy
		#endregion Spawners
		#endregion Tiles
		#endregion Map
		#region Items
		#region Weapons

		#endregion Weapons
		#region Armor
		public static string Item_Armor = Item_Sprites + "Armor\\";
		public static Bitmap Chest_Armor = new Bitmap(Item_Armor + "Tier 1 Chest Armor.png");
		#endregion Armor
		#endregion Items
		#endregion Sprites
		#endregion Graphics
		#region Actors
		public static List<Player> Players;
		public static List<Enemy> Enemies;
		public static bool Combat = false;
		#endregion Actors
		#region Screens
		public enum eGS : int { Title, Game, GameOver }
		public static eGS gameState = eGS.Title;
		public enum eTS : int { Title = -1, NewGame, LoadGame, Options, Quit }
		public static eTS titleState = eTS.Title;

		#endregion Screens
		#endregion Variables
		#region Form Events
		public CotZ() {InitializeComponent();}
		private void CotZ_Load(object sender, EventArgs e) {
			Width = fx; Height = fy;
			Left = (Screen.PrimaryScreen.WorkingArea.Width - fx) / 2;
			Top = (Screen.PrimaryScreen.WorkingArea.Height - fy) / 2;
			gf = CreateGraphics();
			gi = new Bitmap(fx, fy);
			gb = Graphics.FromImage(gi);

			Players = new List<Player>();
			Players.Add(new Player());
			Enemies = new List<Enemy>();
			Enemies.Add(new Enemy());

			Draw();
		} //FotB_Load
		private void CotZ_Paint(object sender, PaintEventArgs e) {
			gf.DrawImage(gi, 0, 0);
		} //FotB_Paint
		private void CotZ_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); return;

				default:
					switch(gameState) {
						case eGS.Title:
							if(titleState == eTS.Title && e.KeyCode == Keys.Space) titleState = eTS.NewGame; else
							switch(e.KeyCode) {
								case Keys.Up: titleState = (eTS)(((int)titleState - 1 + ((int)eTS.Quit + 1)) % ((int)eTS.Quit + 1)); break;
								case Keys.Down: titleState = (eTS)(((int)titleState + 1) % ((int)eTS.Quit+1)); break;

							} break;
						case eGS.Game:
							switch(e.KeyCode) {

							} break;
						case eGS.GameOver:
							switch(e.KeyCode) {

							} break;
						default: break;

					} break;
			} Draw();
		}

		#endregion Form Events
		#region Heartbeat
		public static void Draw() {
			gb.Clear(Color.Black);
			switch(gameState) {
				case eGS.Title:
					Font titleFont = new Font(FontFamily.GenericSerif, 72, FontStyle.Italic, GraphicsUnit.Point);
					gb.DrawString("Citadel", titleFont, Brushes.White, 116, 20);
					gb.DrawString("of the", titleFont, Brushes.White, 220, 150);
					gb.DrawString("Zephyr", titleFont, Brushes.White, 160, 275);

					if(titleState == eTS.Title) gb.DrawString("Press Space", titleFont, Brushes.White, 90, 615);
					else {
						gb.DrawString("New Game", titleFont, (titleState == eTS.NewGame) ? Brushes.Cyan : Brushes.White, 200, 560);
						gb.DrawString("Load Game", titleFont, (titleState == eTS.LoadGame) ? Brushes.Cyan : Brushes.White, 160, 630);
						gb.DrawString("Options", titleFont, (titleState == eTS.Options) ? Brushes.Cyan : Brushes.White, 120, 700);
						gb.DrawString("Quit", titleFont, (titleState == eTS.Quit) ? Brushes.Cyan : Brushes.White, 80, 770);

					}


					break;
			}
			gf.DrawImage(gi, 0, 0);
		}
		#endregion Heartbeat

	}
}
