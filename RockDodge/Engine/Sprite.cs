using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RockDodge.Engine;

public class Sprite
{
    private SpriteOrigin _origin;
    private float _rotation;
    private Vector2 _currentPosition;
    private bool _flipX;
    private readonly Dictionary<string, Animation> _animations;
    private Animation _currentAnimation;
    private readonly List<Rectangle> _staticCollisions;

    private readonly Dictionary<string, ISpriteAction> _actions;

    public Sprite(Animation defaultAnimation, Vector2 startPosition, SpriteOrigin origin = SpriteOrigin.TopLeft)
    {
        _actions = new Dictionary<string, ISpriteAction>();
        _staticCollisions = new List<Rectangle>();

        _origin = origin;

        _currentPosition = startPosition;
        _currentAnimation = defaultAnimation;
        _animations = new Dictionary<string, Animation>
        {
            { "default", defaultAnimation }
        };

        _rotation = 0.0f;
    }

    public void AddStaticCollision(Rectangle collision)
    {
        _staticCollisions.Add(collision);
    }

    public List<Rectangle> GetStaticCollisions()
    {
        return _staticCollisions;
    }
    public Rectangle GetCollisionRectangle()
    {
        Vector2 frameSize = _currentAnimation.GetFrameSize();
        return new Rectangle(
            (int)_currentPosition.X,
            (int)_currentPosition.Y,
            (int)frameSize.X,
            (int)frameSize.Y);
    }

    public void AddRotation(float degrees)
    {
        _rotation += Microsoft.Xna.Framework.MathHelper.ToRadians(degrees);
    }

    public void SetRotation(float degrees)
    {
        _rotation = Microsoft.Xna.Framework.MathHelper.ToRadians(degrees);
    }
    public void AddStaticCollisions(List<Rectangle> collisions)
    {
        _staticCollisions.AddRange(collisions);
    }

    public void AddAction(string name, ISpriteAction action)
    {
        _actions.Add(name, action);
    }

    public void FlipX(bool flipX)
    {
        _flipX = flipX;
    }

    public void Move(Vector2 movement)
    {
        _currentPosition += movement;
    }

    public Vector2 GetPosition()
    {
        return _currentPosition;
    }

    public void MoveTo(Vector2 position)
    {
        _currentPosition = position;
    }

    public void RemoveAction(string name)
    {
        _actions.Remove(name);
    }


    public void AddAnimation(string name, Animation animation)
    {
        _animations.Add(name, animation);
    }

    public void SetAnimation(string name)
    {
        _currentAnimation = _animations[name];
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentAnimation.Draw(spriteBatch, _currentPosition, _flipX, _rotation, _origin);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var action in _actions.Values)
        {
            action.Update(this, gameTime);
        }

        _currentAnimation.Update(gameTime);
    }
}