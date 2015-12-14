using System;
using CocosSharp;
using BouncingGame;

namespace FeelTheBern
{
	public class Enemy : GameObject
	{
		int decreaseAmount;
		bool toBeRemoved;
		GameLayer layer;
		CCSprite eSprite;
		string tag;
		int shootCount;
		float targetX;
		float targetY;
		bool needNewTarget;
		float orientation;
		float maxSpeed;
		public Enemy (float x, float y, string sprite, GameLayer layer)
		{
			needNewTarget = true;
			maxSpeed = 180.0f;
			orientation = 90.0f;
			this.layer = layer;
			eSprite = new CCSprite (sprite);
			eSprite.PositionX = x;
			eSprite.PositionY = y;
			eSprite.Scale = 2.0f;
			layer.AddChild (eSprite);
			decreaseAmount = -1;
			toBeRemoved = false;
			tag = "Enemy";
		}
		public void kill()
		{
			eSprite.Visible = false;
		}
		public string getTag()
		{
			return tag;
		}
		public void Update()
		{
			shootCount++;
			if (shootCount == 50) {
				shoot ();
				shootCount = 0;
			}
			//random direction
			CCRect r = eSprite.VisibleBoundsWorldspace;
			if (needNewTarget) {
				Random gen = new Random ();
				targetX = (float) (gen.NextDouble() * (r.MaxX - r.MinX) + r.MinX);
				targetY = (float)(gen.NextDouble () * (r.MaxY - r.MinY) + r.MinY);
				needNewTarget = false;
			}
				

			float angle = (float)Math.Atan((targetY - eSprite.PositionY)/(targetX - eSprite.PositionX));
			if (eSprite.PositionX != targetX || eSprite.PositionY != targetY) {
				if ((maxSpeed * layer.deltaT) * (float) Math.Cos(angle) >= (float)Math.Abs (eSprite.PositionX - targetX)) {
					eSprite.PositionX = targetX;
				} else {
					if (eSprite.PositionX < targetX) {
						eSprite.PositionX += (maxSpeed * layer.deltaT) * (float)Math.Abs( Math.Cos(angle));
					} else {
						eSprite.PositionX -= (maxSpeed * layer.deltaT) *(float) Math.Abs(Math.Cos(angle));
					}
				}		
				if ((maxSpeed * layer.deltaT) *(float) Math.Sin(angle)>= (float)Math.Abs(eSprite.PositionY - targetY)) {
					eSprite.PositionY = targetY;
				} else {
					if (eSprite.PositionY < targetY) {
						eSprite.PositionY += (maxSpeed * layer.deltaT) *(float) Math.Abs(Math.Sin(angle));
					} else {
						eSprite.PositionY -= (maxSpeed * layer.deltaT) *(float) Math.Abs(Math.Sin(angle));
					}

				}
			}
			if (eSprite.PositionX == targetX && eSprite.PositionY == targetY) {
				needNewTarget = true;
			}


		}
		private void shoot()
		{
			Player b = layer.bernie;
			float xS;
			float yS;
			if (b.GetCollisionBox().Center.X < eSprite.PositionX)
			{
				xS = -1.0f;
			}
			else
			{
				xS = 1.0f;
			}
			if (b.GetCollisionBox().Center.Y < eSprite.PositionY)
			{
				yS = -1.0f;
			}
			else
			{
				yS = 1.0f;
			}
			float PositionX = eSprite.PositionX + (xS * 60);
			float PositionY = eSprite.PositionY + (yS * 60);

			layer.toBeAdded.Add(new Projectile(PositionX, PositionY, b.GetCollisionBox().Center.X, b.GetCollisionBox().Center.Y, "enemyProjectile", layer.ProjectileImageName, layer));

		}
		public CCRect GetCollisionBox()
		{
			return eSprite.BoundingBoxTransformedToParent;
		}
		public void onCollision(GameObject other)
		{
			if (other.getTag () == "projectile") {
				layer.changePolls (decreaseAmount, false);
				toBeRemoved = true;
			}

		}
		public bool getRemove()
		{
			return toBeRemoved;
		}
	}
}
