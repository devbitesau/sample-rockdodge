using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RockDodge.Engine;

public class MoveAction : ISpriteAction
{
    private string _idleAnimation;
    private string _runAnimation;
    private int _pixelsPerSecond;

    private KeyboardState _prevKeyState;

    private bool _isOnGround;
    private Vector2 _velocity = Vector2.Zero;

    public MoveAction(string idleAnimation, string runAnimation, int pixelsPerSecond)
    {
        _idleAnimation = idleAnimation;
        _runAnimation = runAnimation;
        _pixelsPerSecond = pixelsPerSecond;
        _prevKeyState = Keyboard.GetState();
    }

    public void Update(Sprite sprite, GameTime gameTime)
    {
        KeyboardState kState = Keyboard.GetState();

        Rectangle playerRect;
        List<Rectangle> staticRects = sprite.GetStaticCollisions();

        Vector2 movement = Vector2.Zero;
        float xMove = _pixelsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
        {
            sprite.FlipX(true);
            movement= new Vector2(-xMove, 0);
            sprite.SetAnimation(_runAnimation);
        }
        else if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
        {
            sprite.FlipX(false);
            movement = new Vector2(xMove, 0);
            sprite.SetAnimation(_runAnimation);
        }
        else
        {
            sprite.SetAnimation(_idleAnimation);
        }

        sprite.Move(movement);
        playerRect =  sprite.GetCollisionRectangle();

        foreach (var rect in staticRects)
        {
            if (playerRect.Intersects(rect))
            {
                int x = rect.X;
                if (movement.X <= 0) x += rect.Width;
                else x -= playerRect.Width;
                sprite.MoveTo(new Vector2(x, playerRect.Y));
            }
        }




        if (kState.IsKeyDown(Keys.Space) && !_prevKeyState.IsKeyDown(Keys.Space) && _isOnGround)
        {
            _velocity.Y = -18.6f;
            _isOnGround = false;
        }

        float gravity = 1.8f;
        float pixelPerSec = 140;

        _velocity.Y += gravity;



        movement = (new Vector2(0, (float) (_velocity.Y * pixelPerSec * gameTime.ElapsedGameTime.TotalSeconds)));

        sprite.Move(movement);
        playerRect = sprite.GetCollisionRectangle();

        _isOnGround = false;
        foreach (var rect in staticRects)
        {
            if (playerRect.Intersects(rect))
            {
                int y = (rect.Y - playerRect.Height);
                sprite.MoveTo(new Vector2((int)playerRect.X, y));

                _isOnGround = true;
                _velocity.Y = 0.0f;
                break;
            }
        }

        _prevKeyState = Keyboard.GetState();
    }
}