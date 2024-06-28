using System;
using Microsoft.Xna.Framework;

namespace RockDodge.Engine;

public class InfiniScrollAction : ISpriteAction
{
    private Vector2 _homePosition = Vector2.Zero;
    private float _speed = 0.0f;
    private Vector2 _horizontalBounds;
    private bool _goLeft;

    public InfiniScrollAction(float pixelPerSecond, Vector2 horizontalBounds, bool goLeft)
    {
        _speed = pixelPerSecond;
        _horizontalBounds = horizontalBounds;
        _goLeft = goLeft;
    }

    public void Update(Sprite sprite, GameTime gameTime)
    {
        if (_homePosition == Vector2.Zero)
            _homePosition = sprite.GetPosition();

        float movex = (float)(_speed * gameTime.ElapsedGameTime.TotalSeconds);
        if (_goLeft)
            movex = -movex;

        Vector2 pos = sprite.GetPosition();

        Random r = new Random();
        float y = r.Next(-90, 90);

        if (pos.X < _horizontalBounds.X)
        {
            pos.X = _horizontalBounds.Y;
            pos.Y = _homePosition.Y + y;
            sprite.MoveTo(pos);
        }

        if (pos.X > _horizontalBounds.Y)
        {
            pos.X = _horizontalBounds.X;
            pos.Y = _homePosition.Y + y;
            sprite.MoveTo(pos);
        }

        sprite.Move(new Vector2(movex, 0.0f));
    }
}