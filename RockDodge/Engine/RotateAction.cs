using System;
using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public class RotateAction : ISpriteAction
{
    private float _degreesPerSecond;

    public RotateAction(float degreesPerSecond)
    {
        _degreesPerSecond = degreesPerSecond;
    }
    public void Update(Sprite sprite, GameTime gameTime)
    {
        sprite.AddRotation(_degreesPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds);
    }
}