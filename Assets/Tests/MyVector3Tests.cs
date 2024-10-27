using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

#if true
public class MyVector3Tests
{
    [Test]
    public void MyVector3IsAStruct()
    {
        Assert.IsTrue(typeof(MonVector).IsClass == false);
    }

    [Test]
    public void MyVector3Constructor()
    {
        MonVector v = new MonVector(1, 2, 3);
        Assert.AreEqual(1, v.X);
        Assert.AreEqual(2, v.Y);
        Assert.AreEqual(3, v.Z);
    }

    [Test]
    public void MyVector3ConstructorWithNegativ()
    {
        MonVector v = new MonVector(-1, -2, -3);
        Assert.AreEqual( -1, v.X);
        Assert.AreEqual( -2, v.Y);
        Assert.AreEqual( -3, v.Z);
    }

    [Test]
    public void MyVector3TestSqrtMagnitude()
    {
        MonVector v = new MonVector(3, 1, 2);
        Assert.AreEqual(3 * 3 + 1 * 1 + 2 * 2, v.SqrtMagnitude);
    }
    [Test]
    public void MyVector3TestMagnitude()
    {
        MonVector v = new MonVector(3, 1, 2);
        Assert.AreEqual(Mathf.Sqrt(3 * 3 + 1 * 1 + 2 * 2), v.Magnitude);
    }

    [Test]
    public void MyVector3TestMagnitudeWithNegativ()
    {
        MonVector v = new MonVector(2, 10, -2);
        Assert.AreEqual(Mathf.Sqrt(-2 * -2 + 10 * 10 + 2 * 2), v.Magnitude);
    }

    [Test]
    public void CreateMyVector3ThenChangeX()
    {
        MonVector v = new MonVector(2, 10, -2);
        Assert.AreEqual(2, v.X);
        Assert.AreEqual(10, v.Y);
        Assert.AreEqual(-2, v.Z);

        v.X = 12;
        Assert.AreEqual(12, v.X);
        Assert.AreEqual(10, v.Y);
        Assert.AreEqual(-2, v.Z);
    }

    [Test]
    public void CreateMyVector3ThenChangeY()
    {
        MonVector v = new MonVector(2, 10, -2);
        Assert.AreEqual(2, v.X);
        Assert.AreEqual(10, v.Y);
        Assert.AreEqual(-2, v.Z);

        v.Y = 12;
        Assert.AreEqual(2, v.X);
        Assert.AreEqual(12, v.Y);
        Assert.AreEqual(-2, v.Z);
    }

    [Test]
    public void CreateMyVector3ThenChangeZ()
    {
        MonVector v = new MonVector(2, 10, -2);
        Assert.AreEqual(2, v.X);
        Assert.AreEqual(10, v.Y);
        Assert.AreEqual(-2, v.Z);

        v.Z = 12;
        Assert.AreEqual(2, v.X);
        Assert.AreEqual(10, v.Y);
        Assert.AreEqual(12, v.Z);
    }

    [Test]
    public void NormalizeWorks()
    {
        MonVector v = new MonVector(2, 10, -2);
        Vector3 unityVector = new Vector3(2, 10, -2).normalized;

        v.Normalized();

        Assert.AreEqual(unityVector.x, v.X);
        Assert.AreEqual(unityVector.y, v.Y);
        Assert.AreEqual(unityVector.z, v.Z);
    }

    [Test]
    public void AddVector3()
    {
        MonVector v1 = new MonVector(1, 2, 3);
        MonVector v2 = new MonVector(3, 2, -12);

        var result = v1 + v2;
        Assert.AreEqual(4, result.X);
        Assert.AreEqual(4, result.Y);
        Assert.AreEqual(-9, result.Z);
    }

    [Test]
    public void MultiplyVector3ByFloat()
    {
        MonVector v1 = new MonVector(1, 2, 3);
        var result = v1 * 2;
    
        Assert.AreEqual(2, result.X);
        Assert.AreEqual(4, result.Y);
        Assert.AreEqual(6, result.Z);
    }
}
;
#endif