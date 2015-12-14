using System;
using System.Collections.Generic;
using CocosSharp;
using FeelTheBern;
//using BouncingGame.Common;

namespace BouncingGame
{

    public class GameLayer : CCLayer
    {
        int enemyPoll;
        int playerPoll;
        int threshold;
        private List<GameObject> objectsOfGAME;
        public List<GameObject> toBeAdded;

        public float deltaT{ get; set; }

        public Player bernie;
        //CCSprite BernieSandersSprite;
        string BernieSandersImageName;
        string HillaryClintonImageName;
        string DonaldTrumpImageName;
        string DarthVaderImageName;
        public string ProjectileImageName;
        string AR15ImageName;
        Random gen;

		string gunConIN;
		string fireIN;
		string healthIN;
		string tbigIN;
		string menuIN;

		CCSprite control;
		CCSprite fire;
		CCSprite health;
		CCSprite tooBig;
		CCSprite menu;

		List<CCSprite> sprites;


        public GameLayer()
        {
			sprites = new List<CCSprite> ();

			gunConIN = "GunControlPowerup";
			fireIN = "FireButton";
			healthIN = "SocializedHealthcarePowerUp";
			tbigIN = "TooBigToFailPowerup";
			menuIN = "MenuButton";

			control = new CCSprite (gunConIN);
			control.PositionX = 175;
			control.PositionY = 50;
			control.Scale = 2.0f;
			AddChild (control);
			sprites.Add (control);

			fire = new CCSprite (fireIN);
			fire.PositionX = 50;
			fire.PositionY = 50;
			fire.Scale = 1.0f;
			AddChild (fire);
			sprites.Add (fire);

			health = new CCSprite (healthIN);
			health.PositionX = 300;
			health.PositionY = 50;
			health.Scale = 2.0f;
			AddChild (health);
			sprites.Add (health);

			tooBig = new CCSprite (tbigIN);
			tooBig.PositionX = 425;
			tooBig.PositionY = 50;
			tooBig.Scale = 2.0f;
			AddChild (tooBig);
			sprites.Add (tooBig);

			menu = new CCSprite (menuIN);
			menu.PositionX = 550;
			menu.PositionY = 50;
			menu.Scale = 1.0f;
			AddChild (menu);
			sprites.Add (menu);


            gen = new Random();
            enemyPoll = 80;
            playerPoll = 20;
            threshold = 5;
            objectsOfGAME = new List<GameObject>();
            toBeAdded = new List<GameObject>();
            BernieSandersImageName = "BernieSandersFace";
            HillaryClintonImageName = "HillaryClintonFace";
            DonaldTrumpImageName = "DonaldTrumpImageName";
            DarthVaderImageName = "DarthVaderFace";
            ProjectileImageName = "Projectile";
            bernie = new Player(100.0f, 100.0f, BernieSandersImageName, this);
            objectsOfGAME.Add(bernie);

            // Load and instantate your assets here
            //Create Bernie Sanders

            // Make any renderable node objects (e.g. sprites) children of this layer

            //var touchListener = new CCEventListenerTouchAllAtOnce ();
            //touchListener.OnTouchesBegan = this.OnTouchesBegan;
            //touchListener.OnTouchesEnded = this.OnTouchesEnded;
            //touchListener.OnTouchesMoved = this.HandleTouchesMoved;
            //AddEventListener (touchListener, this);


            // New code:
            Schedule(GameUpdate);
        }

        public void changePolls(int num, bool isPlayer)
        {
            if (isPlayer)
            {
                playerPoll += num;

            }
            else
            {
                enemyPoll += num;
                if (enemyPoll < 0)
                {
                    enemyPoll = 0;
                }
            }
        }

        private void GameUpdate(float frameTimeInSecconds)
        {
            deltaT = frameTimeInSecconds;
            threshold = 11 - enemyPoll / 10;
            //check collions
            checkForCollisions();
            //call updates
            int numOfEnemies = 0;
            foreach (GameObject o in objectsOfGAME)
            {
                if (o.getTag() == "Enemy")
                {
                    numOfEnemies++;
                }
                o.Update();
            }

            if (numOfEnemies < threshold)
            {
                //spawn more enemies!
                float x = (float)(gen.NextDouble() * (VisibleBoundsWorldspace.MaxX - VisibleBoundsWorldspace.MinX) + VisibleBoundsWorldspace.MinX);
                float y = (float)(gen.NextDouble() * (VisibleBoundsWorldspace.MaxY - VisibleBoundsWorldspace.MinY) + VisibleBoundsWorldspace.MinY);
                objectsOfGAME.Add(new Enemy(x, y, HillaryClintonImageName, this));
            }
            foreach (GameObject o in toBeAdded)
            {
                objectsOfGAME.Add(o);

            }
            toBeAdded = new List<GameObject>();
            //remove
            for (int i = 0; i < objectsOfGAME.Count; i++)
            {
                if (objectsOfGAME[i].getRemove())
                {
                    objectsOfGAME[i].kill();
                    objectsOfGAME.RemoveAt(i);
                }
            }
        }


        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            CCRect bounds = VisibleBoundsWorldspace;

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            // new code:
            touchListener.OnTouchesBegan = OnTouchesBegan;
            touchListener.OnTouchesMoved = HandleTouchesMoved;

            AddEventListener(touchListener, this);
        }

        private void checkForCollisions()
        {
            //optimize later if needed
            foreach (GameObject a in objectsOfGAME)
            {
                foreach (GameObject b in objectsOfGAME)
                {

                    bool collide = a.GetCollisionBox().IntersectsRect(b.GetCollisionBox());
                    if (collide)
                    {
                        a.onCollision(b);
                        b.onCollision(a);
                    }
                }
            }
        }

        void OnTouchesBegan(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            //only pass touches not pressing a button
			List<CCTouch> noButtonTouches = new List<CCTouch>();
			foreach(CCTouch t in touches)
			{
				bool noButton = true;
				foreach(CCSprite s in sprites)
				{
					if(t.Location.X < s.BoundingBoxTransformedToParent.MaxX &&
						t.Location.X > s.BoundingBoxTransformedToParent.MinX &&
						t.Location.Y < s.BoundingBoxTransformedToParent.MaxY &&
						t.Location.Y > s.BoundingBoxTransformedToParent.MinY)
					{
						noButton = false;
					}
				}
				if(noButton)
				{
					noButtonTouches.Add(t);
				}
			}
			bernie.HandleTouchesMoved(noButtonTouches, touchEvent);
        }

        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            //only pass touches not pressing a button
			List<CCTouch> noButtonTouches = new List<CCTouch>();
			foreach(CCTouch t in touches)
			{
				bool noButton = true;
				foreach(CCSprite s in sprites)
				{
					if(t.Location.X < s.BoundingBoxTransformedToParent.MaxX &&
						t.Location.X > s.BoundingBoxTransformedToParent.MinX &&
						t.Location.Y < s.BoundingBoxTransformedToParent.MaxY &&
						t.Location.Y > s.BoundingBoxTransformedToParent.MinY)
					{
						noButton = false;
					}
				}
				if(noButton)
				{
					noButtonTouches.Add(t);
				}
			}
			bernie.HandleTouchesMoved(noButtonTouches, touchEvent);

        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            //only pass touches not pressing a button

            bernie.OnTouchesEnded(touches, touchEvent);
        }
    }

}
