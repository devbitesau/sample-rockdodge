using System;
using Microsoft.Xna.Framework;

namespace RockDodge.Engine.Particle;

public class ParticleEmitter
{
    private ParticleEmitterData _data;
    private float _intervalLeft;
    private IEmitter _emitter;
    private ParticleManager _particleManager;
    private Random _random = new Random();

    public ParticleEmitter(IEmitter emitter, ParticleEmitterData data, ParticleManager particleManager)
    {
        _emitter = emitter;
        _data = data;
        _particleManager = particleManager;

        _intervalLeft = _data.interval;
    }

    private void Emit(Vector2 position)
    {

        ParticleData d = _data.particleData;
        d.lifespan = Randfloat(_data.lifespanMin, _data.lifespanMax);
        d.speed = Randfloat(_data.speedMin, _data.speedMax);
        float r = (float)(_random.NextDouble()*2)-1;
        d.angle += _data.angleVariance * r;

        Particle p = new(position, d);
        _particleManager.AddParticle(p);
    }

    public void Update(GameTime gameTime)
    {
        _intervalLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        while (_intervalLeft <= 0f)
        {
            _intervalLeft += _data.interval;
            var pos = _emitter.EmitPosition;

            for (int i = 0; i < _data.emitCount; i++)
            {
                Emit(pos);
            }
        }
    }

    private float Randfloat(float min, float max)
    {
        return (float)(_random.NextDouble() * (max - min) + min);
    }
}