using ForkKnight.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkKnight.Animations
{
    internal class AnimationList
    {
        public Dictionary<Direction, IdleAnimation> idleanimationList = new Dictionary<Direction, IdleAnimation>();
        public Dictionary<Direction, RunAnimation> runanimationList = new Dictionary<Direction, RunAnimation>();


        public AnimationList()
        {
            idleanimationList.Add(Direction.Left, new IdleAnimation(Direction.Left));
            idleanimationList.Add(Direction.Right, new IdleAnimation(Direction.Right));

            runanimationList.Add(Direction.Left, new RunAnimation(Direction.Left));
            runanimationList.Add(Direction.Right, new RunAnimation(Direction.Right));
        }
    }
}
