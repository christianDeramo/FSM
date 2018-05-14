using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMc
{
    class PlayScene : Scene
    {
        public List<Player> Players { get; protected set; }


        public override void Start()
        {
            base.Start();
            GfxManager.Init();

            GfxManager.AddTexture("player1", Game.ASSETS_PATH + "player_1.png");
            GfxManager.AddTexture("player2", Game.ASSETS_PATH + "player_2.png");
            GfxManager.AddTexture("enemy0", Game.ASSETS_PATH + "enemy_0.png");
            GfxManager.AddTexture("enemy3", Game.ASSETS_PATH + "enemy_3.png");
            GfxManager.AddTexture("bg", Game.ASSETS_PATH + "hex_grid_green.png");
            GfxManager.AddTexture("bullet", Game.ASSETS_PATH + "fireball.png");
            GfxManager.AddTexture("powerup", Game.ASSETS_PATH + "heart.png");

            GfxManager.AddTexture("loadingBar_bar", Game.ASSETS_PATH + "loadingBar_bar.png");
            GfxManager.AddTexture("loadingBar_frame", Game.ASSETS_PATH + "loadingBar_frame.png");

            UpdateManager.Init();
            DrawManager.Init();
            PhysicsManager.Init();
            BulletManager.Init();
            //SpawnManager.Init();


            //Texture bgTExture = GfxManager.GetTexture("bg");

            //backGround = new GameObject(new Vector2(-bgTExture.Width, -600), "bg");
            //backGround.IsCullingAffected = false;
            //TilingTexture tl = new TilingTexture(backGround, bgTExture, 2);
            GameObject bg = new GameObject(Vector2.Zero, "bg",DrawManager.Layer.Background, Game.window.Width, Game.window.Height);

            Players = new List<Player>();


            Player player = new Player("player1",new Vector2(5, 6));
            Player player2 = new Player("player2",new Vector2(1, 1));

            player2.UP = KeyCode.W;
            player2.DOWN = KeyCode.S;
            player2.RIGHT = KeyCode.D;
            player2.LEFT = KeyCode.A;
            player2.FIRE = KeyCode.F;

            Enemy enemy0 = new Enemy(new Vector2(1, 10), "enemy0");
            Enemy enemy3 = new Enemy(new Vector2(5, 4), "enemy3");


            Players = new List<Player>();

            Players.Add(player);
            Players.Add(player2);


        }
        public override void Draw()
        {
            DrawManager.Draw();
        }

        public override void Input()
        {
            for(int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsActive)
                    Players[i].Input();
            }
        }

        public override void Update()
        {
            PhysicsManager.Update();
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
            SpawnManager.Update();
        }

        public override void OnExit()
        {
            UpdateManager.RemoveAll();
            DrawManager.RemoveAll();
            PhysicsManager.RemoveAll();
            GfxManager.RemoveAll();
        }
    }
}
