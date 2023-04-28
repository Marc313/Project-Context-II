using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    #region Float Helpers
    // Float Helpers \\

    /// <summary>
    /// Maps a float 'value' from its original range in between 'min1' and 'max1' to a new range between 'min2' and 'max2'
    /// </summary>
    public static float Map(float min1, float max1, float min2, float max2, float value)
    {
        float normalizedValue = Mathf.InverseLerp(min1, max1, value);
        float newValue = Mathf.Lerp(min2, max2, normalizedValue);
        return newValue;
    }

    public static bool NearlyEquals(this float _f1, float _f2, float _delta)
    {
        return Mathf.Abs(_f1 - _f2) <= _delta;
    }

    #endregion

    /// <summary>
    /// Rounds a Vector3 to a Vector3Int
    /// </summary>
    /// <param name="vector"> The original Vector </param>
    /// <returns></returns>
    #region Vector3 Helpers
    public static Vector3Int ToVector3Int(this Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
    #endregion

    #region Transform Helpers
    /// <summary>
    /// Sets the x value of _transform.position to _x
    /// </summary>
    /// <param name="_transform"> Original transform </param>
    /// <param name="_x"> New x value </param>
    public static void SetXPosition(this Transform _transform, float _x)
    {
        Vector3 newPos = _transform.position;
        newPos.x = _x;
        _transform.position = newPos;
    }

    /// <summary>
    /// Sets the y value of _transform.position to _y
    /// </summary>
    /// <param name="_transform"> Original transform </param>
    /// <param name="_y"> New y value </param>
    public static void SetYPosition(this Transform _transform, float _y)
    {
        Vector3 newPos = _transform.position;
        newPos.x = _y;
        _transform.position = newPos;
    }

    /// <summary>
    /// Sets the z value of _transform.position to _z
    /// </summary>
    /// <param name="_transform"> Original transform </param>
    /// <param name="_z"> New z value </param>
    public static void SetZPosition(this Transform _transform, float _z)
    {
        Vector3 newPos = _transform.position;
        newPos.x = _z;
        _transform.position = newPos;
    }
    #endregion

    public static T GetRandomElement<T>(this T[] _array)
    {
        return _array[Random.Range(0, _array.Length)];
    }

/*    public static T GetRandomElement<T>(this IEnumerator<T> _array)
    {
        return _array[Random.Range(0, _array)];
    }*/

    #region Direction enum Helpers
    /*    public static Direction Opposite(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                default: return Direction.None;
            }
        }

        public static Quaternion GetRotation(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Down: return Quaternion.Euler(0,180,0);
                case Direction.Left: return Quaternion.Euler(0,-90,0);
                case Direction.Right: return Quaternion.Euler(0,90,0);
                case Direction.Up: 
                default: return Quaternion.Euler(0, 0, 0);
            }
        }*/
    #endregion
}
