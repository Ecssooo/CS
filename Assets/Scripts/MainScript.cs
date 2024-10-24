using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField] private MonVector _monVector;
    
    void Start()
    {
        _monVector = new MonVector(3, 4, 0);
        Debug.Log(_monVector.Magnitude);
        MonVector newVector = _monVector.Normalized();
        Debug.Log(newVector.Magnitude);

        Vector3 vector3 = new Vector3(3, 4, 0);
        Debug.Log(vector3.magnitude);
        vector3 = vector3.normalized;
        Debug.Log(vector3.magnitude);
    }
}
