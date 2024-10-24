using Unity.VisualScripting;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public struct MonVector
{
    private float _x;
    private float _y;
    private float _z;
    
    public float X { get => _x; set => _x = value; }
    public float Y { get => _y; set => _y = value; }
    public float Z { get => _z; set => _z = value; }
    
    public static MonVector operator +(MonVector a, MonVector b) => new MonVector(a.X += b.X, a.Y += b.Y, a.Z += b.Z);
    public static MonVector operator /(MonVector a, float b) => new MonVector(a.X /= b, a.Y /= b, a.Z /= b);
    
    public MonVector(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    
    public float Magnitude { get { return Mathf.Sqrt(_x * _x + _y * _y + _z * _z); } }

    public float SqrMagnitude { get { return _x * _x + _y * _y + _z * _z; } }

    public MonVector Normalized()
    {
        float magn = Magnitude;
        if (magn > 0)
            return this / magn;
        else
        {
            return new MonVector(0, 0, 0);
        }
    }
}
