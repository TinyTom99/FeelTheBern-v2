using System;
using CocosSharp;

namespace FeelTheBern
{
    public interface GameObject
    {
        string getTag();

        void Update();

        CCRect GetCollisionBox();

        void onCollision(GameObject other);

        bool getRemove();

        void kill();
    }
}

