using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RockDodge.Engine.Particle;

public struct ParticleData
{
    public Texture2D Texture;
    public float lifespan = 2f;
    public Color ColorStart = Color.Yellow;
    public Color ColorEnd = Color.Red;
    public float OpacityStart = 1f;
    public float OpacityEnd = 0f;
    public float sizeStart = 8f;
    public float sizeEnd = 2f;
    public float speed = 300f;
    public float angle = 0f;

    public ParticleData(Texture2D texture)
    {
        Texture = texture;
    }
}