using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZNA
{
    public class View
    {
        public Vector2 position;
        private ZObject toFollow;

        public View(Vector2 pos)
        {
            position = pos;
            toFollow = null;
        }

        public View(ZObject gobj)
        {
            toFollow = gobj;
            position = gobj.Position() - new Vector2(136, 240);
        }

        public void Update()
        {
            if (toFollow != null)
            {
                position = toFollow.Position() - new Vector2(136, 240);
            }
        }

        public void ChangePosition(int x, int y)
        {
            position = new Vector2(x, y);
        }
    }
}