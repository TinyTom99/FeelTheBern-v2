using System;
using CocosSharp;
using BouncingGame;


namespace FeelTheBern
{
	public class Projectile : GameObject
	{
		string tag;
		float speed;
		CCSprite proSprite;
		bool toBeRemoved;
		float vX;
		float vY;
		GameLayer layer;
		public Projectile(float x, float y, float tX, float tY, string tag, string sprite, GameLayer layer)
		{
			speed = 300.0f;
			proSprite = new CCSprite(sprite);
			proSprite.PositionX = x;
			proSprite.PositionY = y;
			layer.AddChild(proSprite);
			this.layer = layer;
			this.tag = tag;
			toBeRemoved = false;
			//vX = speed * cos(orientation)
			//vY = speed * sin(orienatation)
			float angle = (float)Math.Atan((tY - y) / (tX - x));
			if (x != tX || y != tY)
			{
				if (x < tY)
				{
					vX = speed * (float)Math.Abs(Math.Cos(angle));
				}
				else
				{
					vX = -1.0f * (speed * (float)Math.Abs(Math.Cos(angle)));
				}
				if (y < tY)
				{
					vY = speed * (float)Math.Abs(Math.Sin(angle));
				}
				else
				{
					vY = -1.0f * (speed * (float)Math.Abs(Math.Sin(angle)));
				}
				if (Math.Abs(tY - y) <= 0.5)
				{
					vY = 0;
					if (x < tX)
					{
						vX = speed;
					}
					else
					{
						vX = -speed;
					}
				}
			}

		}

		public string getTag()
		{
			return tag;
		}

		public void kill()
		{
			proSprite.Visible = false;
		}

		public void Update()
		{
			proSprite.PositionX += vX * layer.deltaT;
			proSprite.PositionY += vY * layer.deltaT;
			if (proSprite.PositionX > proSprite.VisibleBoundsWorldspace.MaxX ||
				proSprite.PositionY > proSprite.VisibleBoundsWorldspace.MaxY ||
				proSprite.PositionX < proSprite.VisibleBoundsWorldspace.MinX ||
				proSprite.PositionY < proSprite.VisibleBoundsWorldspace.MinY)
			{
				toBeRemoved = true;
			}
		}

		public CCRect GetCollisionBox()
		{
			return proSprite.BoundingBoxTransformedToParent;
		}

		public void onCollision(GameObject other)
		{
			if (other.getTag() != "projectile" || other.getTag() != "enemyProjectile")
			{
				//toBeRemoved = false;
			}
			if (other.getTag() == "player")
			{
				toBeRemoved = true;
			}
		}

		public bool getRemove()
		{
			return toBeRemoved;
		}
	}
}

