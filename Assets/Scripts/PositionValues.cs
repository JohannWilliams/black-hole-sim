using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionValues : MonoBehaviour
{
    public List<Vector3> position = new List<Vector3>();
    public List<Vector3> momentum = new List<Vector3>();

    void Start()
    {
        int posNeg = -1;
        for(int i = 1; i < 6; i++)
        {
            posNeg *= posNeg; 
            for(int j = 0; j < 10; j++)
            {
                float divisiser = (j + 10) / (i+1 * 2);
                float x = Mathf.Pow(i, j) * posNeg / divisiser;
                float y = Mathf.Pow(j, i) * posNeg / divisiser;
                if(x == 0){x = x + 2 * Mathf.Pow(2, i+j)%25; }
                else if(x < 4 && x > -4) { x = x * 2 * Mathf.Pow(2, i + 1); }
                else if(x > 90 || x < -90) 
                {
                    while(x > 90 || x < -90)
                    {
                        x = Mathf.Sqrt(x);
                    }
                }
                if(y == 0){ y = y + 3 * Mathf.Pow(2, j + j) % 31; }
                else if(y < 4 && y > -4) { y = y * 3 * Mathf.Pow(2, j + j) % 31; }
                else if (y > 90 || y < -90)
                {
                    while (y > 90 || y < -90)
                    {
                        y = Mathf.Sqrt(y);
                    }
                }
                if (x % 4 == 0) { x = -x; }
                if (y % 3 == 0) { y = -y; }
                if(x > 0 && y > 0) { y = -y; }
                momentum.Add(new Vector3(x,0, y));
            }

            for (int j = 10; j < 20; j++)
            {
                float divisiser = (j + 10) / (i + 1 * 2);
                float x = Mathf.Pow(i, j) * posNeg / divisiser;
                float y = Mathf.Pow(j, i) * posNeg / divisiser;
                if (x == 0) { x = x + 2 * Mathf.Pow(2, i + j) % 25; }
                else if (x < 4 && x > -4) { x = x * 2 * Mathf.Pow(2, i + 1); }
                else if (x > 90 || x < -90)
                {
                    while (x > 90 || x < -90)
                    {
                        x = Mathf.Sqrt(x);
                    }
                }
                if (y == 0) { y = y + 3 * Mathf.Pow(2, j + j) % 31; }
                else if (y < 4 && y > -4) { y = y * 3 * Mathf.Pow(2, j + j) % 31; }
                else if (y > 90 || y < -90)
                {
                    while (y > 90 || y < -90)
                    {
                        y = Mathf.Sqrt(y);
                    }
                }
                if(x % 4 == 0) { x = -x; }
                if(y % 3 == 0) { y = -y; }
                if(x > 0 && y > 0) { x = -x; }
                position.Add(new Vector3(x, 0, y));
            }

        }

        Debug.Log(position.ToString());
    }
}
