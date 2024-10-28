using UnityEngine;

public struct MonVector
{
    //Field
    private float _x;
    private float _y;
    private float _z;
    
    //Propriety
    public float X { get => _x; set => _x = value; }
    public float Y { get => _y; set => _y = value; }
    public float Z { get => _z; set => _z = value; }
    
    //Operator
    public static MonVector operator +(MonVector a, MonVector b) => new MonVector(a.X += b.X, a.Y += b.Y, a.Z += b.Z);
    public static MonVector operator -(MonVector a, MonVector b) => new MonVector(a.X -= b.X, a.Y -= b.Y, a.Z -= b.Z);
    public static MonVector operator /(MonVector a, float b) => new MonVector(a.X /= b, a.Y /= b, a.Z /= b);
    public static MonVector operator *(MonVector a, float b) => new MonVector(a.X *= b, a.Y *= b, a.Z *= b);
    
    
    //Constructor
    public MonVector(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    
    public float Magnitude => Mathf.Sqrt(SqrtMagnitude);
    public float SqrtMagnitude => _x * _x + _y * _y + _z * _z;

    public void Normalized() => this /= Magnitude;
}
