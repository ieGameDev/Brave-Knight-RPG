using Data;
using UnityEngine;

namespace Extensions
{
    public static class DataExtensions
    {
        public static Vector3Data VectorData(this Vector3 vector) =>
            new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 UnityVector(this Vector3Data vector) =>
            new Vector3(vector.X, vector.Y, vector.Z);
        
        public static T ToDeserialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}