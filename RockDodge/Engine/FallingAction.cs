using System;
using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public class FallingAction : ISpriteAction
{
    private float _speed = 200f;

    public FallingAction()
    {
    }

    public void Update(Sprite sprite, GameTime gameTime)
    {
        sprite.Move(new Vector2(0, _speed * (float)gameTime.ElapsedGameTime.TotalSeconds) );

        if (sprite.GetPosition().Y > 800)
        {
            Random r = new Random();
            int x = r.Next(0, 350);
            sprite.MoveTo(new Vector2(x,0));
        }
    }
}