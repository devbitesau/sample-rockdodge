using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RockDodge.Engine.Particle;

public class Particle
{
    private ParticleData _data;
    private Vector2 _position;
    private float _lifespanLeft;
    private float _lifespanAmount;
    private Color _color;
    private float _opacity;

    private float _scale;
    private Vector2 _origin;
    private Vector2 _direction;

    public bool IsFinished = false;


    public Particle(Vector2 pos, ParticleData data)
    {
        _data = data;
        _position = pos;
        _lifespanLeft = data.lifespan;
        _lifespanAmount = 1f;
        _color = data.ColorStart;
        _opacity = data.OpacityStart;
        _origin = new Vector2(_data.Texture.Width/2f, _data.Texture.Height/2f);

        if (_data.speed != 0)
        {
            _data.angle = MathHelper.ToRadians(_data.angle);
            _direction = new Vector2((float)Math.Sin(_data.angle), (float)-Math.Cos(data.angle));
        }
        else
        {
            _direction = Vector2.Zero;
        }
    }

    public void Update(GameTime gameTime)
    {

        _lifespanLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_lifespanLeft <= 0)
        {
            IsFinished = true;
            return;
        }

        _lifespanAmount = MathHelper.Clamp(_lifespanLeft / _data.lifespan, 0f, 1f);
        _color = Color.Lerp(_data.ColorEnd, _data.ColorStart, _lifespanAmount);
        _opacity = MathHelper.Clamp(MathHelper.Lerp(_data.OpacityEnd, _data.OpacityStart, _lifespanAmount), 0f, 1f);
        _scale = MathHelper.Lerp(_data.sizeEnd, _data.sizeStart, _lifespanAmount) / _data.Texture.Height;
        _position += _direction * _data.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Draw(
            _data.Texture,
            _position,
            null,
            _color * _opacity,
            0f,
            _origin,
            _scale,
            SpriteEffects.None,
            1f
            );
    }
}