using System;
using CocosSharp;
using System.Collections.Generic;
//using Android.Content;
//using Android.Runtime;
using System.Threading;
//using Android.Widget;
using BouncingGame;


namespace FeelTheBern
{
	public class Player : GameObject
	{
		int decreaseAmount;
		string tag;
		GameLayer Layer;
		CCSprite bSanders;
		int health;
		float targetX;
		float targetY;
		float speed;
		bool toBeRemoved;
		public Player (float x, float y, string sprite, GameLayer layer)
		{
			
			targetX = x;
			targetY = y;
			tag = "player";
			speed = 180.0f;
			toBeRemoved = false;
			health = 10;
			decreaseAmount = -5;
			bSanders = new CCSprite (sprite);
			bSanders.PositionX = x;
			bSanders.PositionY = y;
			bSanders.Scale = 2.0f;
			layer.AddChild (bSanders);
			Layer = layer;
		}
		public void kill()
		{
			
		}
		public string getTag()
		{
			return tag;
		}
		public void Update()
		{
			//arctan(deltaY / deltaX) * 180 / PI
			
			float angle = (float)Math.Atan((targetY - bSanders.PositionY)/(targetX - bSanders.PositionX));
			if (bSanders.PositionX != targetX || bSanders.PositionY != targetY) {
				if ((speed * Layer.deltaT) *(float) Math.Cos(angle) >= (float)Math.Abs (bSanders.PositionX - targetX)) {
					bSanders.PositionX = targetX;
				} else {
					if (bSanders.PositionX < targetX) {
						bSanders.PositionX += (speed * Layer.deltaT) * (float)Math.Abs( Math.Cos(angle));
					} else {
						bSanders.PositionX -= (speed * Layer.deltaT) *(float) Math.Abs(Math.Cos(angle));
					}
				}		
				if ((speed * Layer.deltaT) *(float) Math.Sin(angle)>= (float)Math.Abs(bSanders.PositionY - targetY)) {
					bSanders.PositionY = targetY;
				} else {
					if (bSanders.PositionY < targetY) {
						bSanders.PositionY += (speed * Layer.deltaT) *(float) Math.Abs(Math.Sin(angle));
					} else {
						bSanders.PositionY -= (speed * Layer.deltaT) *(float) Math.Abs(Math.Sin(angle));
					}

				}
			}
		}
		public CCRect GetCollisionBox()
		{
			return bSanders.BoundingBoxTransformedToParent;
		}
		public void onCollision (GameObject other)
		{
			if (other.getTag () == "projectile") {
				Layer.changePolls (decreaseAmount, true);
				health -= 1;
			}
			if (other.getTag () == "Enemy") {
				

            }
        }

        public bool getRemove()
        {
            return toBeRemoved;
        }

        //touches n stuff
        public void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            //get the position of the moved touch.
			if (touches.Count > 0) {
				targetX = touches[0].Location.X;
				targetY = touches[0].Location.Y;
			}
            
            //bSanders.PositionX = touches [0].Location.X;
            //bSanders.PositionY = touches [0].Location.Y;
            //put it in a todo to be handeled in an update.
            //new Thread(new ThreadStart(delegate {((MainActivity)this.sim_view.Context).RunOnUiThread(()=> Toast.MakeText(((MainActivity)this.sim_view.Context),
            //	"Message",ToastLength.Short).Show());})).Start();

            //Toast.MakeText(Layer.GameView.Context,"Message",ToastLength.Short).Show();

        }

        public void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            //Toast.MakeText(Layer.GameView.Context,"Message",ToastLength.Short).Show();
            //touches.Remove ();
            targetX = bSanders.PositionX;
            targetY = bSanders.PositionY;
            if (touches.Count > 0)
            {

                //single touches are shooting if they are not on a button for a powerup.
            }
        }

        public void OnTouchBegan(List<CCTouch> touches, CCEvent touchEvent)
        {

			}
		}
		

}

