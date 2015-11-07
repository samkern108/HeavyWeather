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
        this.transform.position = new Vector3(pos.x + 10, pos.y + 10, pos.z);
    }
}
