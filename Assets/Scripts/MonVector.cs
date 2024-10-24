using UnityEngine;

public struct MonVector
{
    private float _x;
    private float _y;
    private float _z;
    
    public float X { get => _x; set => _x = value; }
    public float Y { get => _y; set => _y = value; }
    public float Z { get => _z; set => _z = value; }
    
    
    public MonVector(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    
    public float Magnitude
    {
        get { return _x * _x + _y * _y; }
    }

    public float SqrMagnitude
    {
        get { return Mathf.Sqrt(Magnitude); }
    }

    public MonVector Normalized
    {
        get
        {
            MonVector vector = this;
            vector.X /= Mathf.Abs(vector.X);
            vector.Y /= Mathf.Abs(vector.Y);
            vector.Z /= Mathf.Abs(vector.Z);
            return vector;
        }
    }
    
    
    public MonVector _Normalized(MonVector vector)
    {
        vector.X /= Mathf.Abs(vector.X);
        vector.Y /= Mathf.Abs(vector.Y);
        vector.Z /= Mathf.Abs(vector.Z);

        return vector;
    }
}
