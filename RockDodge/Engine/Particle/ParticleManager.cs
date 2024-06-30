using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RockDodge.Engine.Particle;

public class ParticleManager
{
    private readonly List<Particle> _particles = new List<Particle>();
    private readonly List<ParticleEmitter> _particleEmitters = new List<ParticleEmitter>();

    public void AddParticle(Particle particle)
    {
        _particles.Add(particle);
    }

    public void Update(GameTime gameTime)
    {

        Parallel.ForEach(_particles, p =>
        {
            p.Update(gameTime);
        });

        // foreach (var particle in _particles)
        // {
        //     particle.Update(gameTime);
        // }

        Parallel.ForEach(_particleEmitters, e =>
        {
            e.Update(gameTime);
        });

        // foreach (var emitter in _particleEmitters)
        // {
        //     emitter.Update(gameTime);
        // }

        _particles.RemoveAll(x => x.IsFinished);

    }

    public void AddParticleEmitter(ParticleEmitter e)
    {
        _particleEmitters.Add(e);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var particle in _particles)
        {
            particle.Draw(spriteBatch, gameTime);
        }
    }
}