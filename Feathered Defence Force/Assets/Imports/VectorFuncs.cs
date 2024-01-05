using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFuncs
{
    #region Clamps
    public static Vector3 ClampVector(Vector3 vectorToClamp, Vector3 min, Vector3 max)
    {
        Vector3[] minMax = SortVectorSize(min, max);
        min = minMax[0];
        max = minMax[1];
        float vectorX, vectorY, vectorZ;
        vectorX = Mathf.Clamp(vectorToClamp.x, min.x, max.x);
        vectorY = Mathf.Clamp(vectorToClamp.y, min.y, max.y);
        vectorZ = Mathf.Clamp(vectorToClamp.z, min.z, max.z);
        return new Vector3(vectorX, vectorY, vectorZ);
    }
    public static Vector2 ClampVector(Vector2 vectorToClamp, Vector3 min, Vector3 max)
    {
        Vector3[] minMax = SortVectorSize(min, max);
        min = minMax[0];
        max = minMax[1];
        float vectorX, vectorY;
        vectorX = Mathf.Clamp(vectorToClamp.x, min.x, max.x);
        vectorY = Mathf.Clamp(vectorToClamp.y, min.y, max.y);
        return new Vector2(vectorX, vectorY);
    }
    public static Vector3 ClampVector(Vector3 vectorToClamp, Vector2 min, Vector2 max)
    {
        Vector2[] minMax = SortVectorSize(min, max);
        min = minMax[0];
        max = minMax[1];
        float vectorX, vectorY;
        vectorX = Mathf.Clamp(vectorToClamp.x, min.x, max.x);
        vectorY = Mathf.Clamp(vectorToClamp.y, min.y, max.y);
        return new Vector3(vectorX, vectorY, vectorToClamp.z);
    }
    public static Vector2 ClampVector(Vector2 vectorToClamp, Vector2 min, Vector2 max)
    {
        Vector2[] minMax = SortVectorSize(min, max);
        min = minMax[0];
        max = minMax[1];
        float vectorX, vectorY;
        vectorX = Mathf.Clamp(vectorToClamp.x, min.x, max.x);
        vectorY = Mathf.Clamp(vectorToClamp.y, min.y, max.y);
        return new Vector2(vectorX, vectorY);
    }
    #endregion

    #region SizeSorter
    public static Vector3[] SortVectorSize(Vector3 vector1, Vector3 vector2)
    {
        float temp;
        if (vector1.x > vector2.x)
        {
            temp = vector2.x;
            vector2.x = vector1.x;
            vector1.x = temp;
        }
        if (vector1.y > vector2.y)
        {
            temp = vector2.y;
            vector2.y = vector1.y;
            vector1.y = temp;
        }
        if (vector1.z > vector2.z)
        {
            temp = vector2.z;
            vector2.z = vector1.z;
            vector1.z = temp;
        }
        Vector3[] returnVectors = new Vector3[2];
        returnVectors[0] = vector1;
        returnVectors[1] = vector2;
        return returnVectors;
    }
    public static Vector2[] SortVectorSize(Vector2 vector1, Vector2 vector2)
    {
        float temp;
        if (vector1.x > vector2.x)
        {
            temp = vector2.x;
            vector2.x = vector1.x;
            vector1.x = temp;
        }
        if (vector1.y > vector2.y)
        {
            temp = vector2.y;
            vector2.y = vector1.y;
            vector1.y = temp;
        }
        Vector2[] returnVectors = new Vector2[2];
        returnVectors[0] = vector1;
        returnVectors[1] = vector2;
        return returnVectors;
    }
    #endregion

    #region Deviation
    public static bool WithinDeviation(Vector3 object1, Vector3 object2, float deviation)
    {
        // Check if object1.x is between object2.x +- deviation
        /*
        if (object1.x >= object2.x - deviation && object1.x <= object2.x + deviation)
        {
            if (object1.y >= object2.y - deviation && object1.y <= object2.y + deviation)
            {
                if (object1.z >= object2.z - deviation && object1.z <= object2.z + deviation)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        */
        return
        object1.x >= object2.x - deviation && object1.x <= object2.x + deviation &&
        object1.y >= object2.y - deviation && object1.y <= object2.y + deviation &&
        object1.z >= object2.z - deviation && object1.z <= object2.z + deviation;

    }

    #endregion
}
