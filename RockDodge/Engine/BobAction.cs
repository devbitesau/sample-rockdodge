using System;
using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public class BobAction : ISpriteAction
{
    public void Update(Sprite sprite, GameTime gameTime)
    {
        float y = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)/8;
        sprite.Move(new Vector2(0,y));
    }
}