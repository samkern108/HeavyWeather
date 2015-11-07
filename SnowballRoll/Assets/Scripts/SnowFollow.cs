using UnityEngine;
using System.Collections;

public class SnowFollow : MonoBehaviour {

    public static SnowFollow instance;

    void Start()
    {
        instance = this;
    }

    public void UpdatePos(Vector3 pos)
    {
        this.transform.position = new Vector3(pos.x, pos.y + 10, pos.z);
    }
}
