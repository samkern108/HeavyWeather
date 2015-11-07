using UnityEngine;
using System.Collections;

public class SnowMakerController : MonoBehaviour {

    public ParticleSystem snow;
    public GameObject player;

    public float snowSpeed = 1;
    //public float snowVelocity = 0;
    public float snowAmount = 100;

    void Awake()
    {
        snow = gameObject.GetComponent<ParticleSystem>();
        UpdateSnowBehaviour();
    }

    private void UpdateSnowBehaviour()
    {
        snow.emissionRate = snowAmount;
        snow.startSpeed = snowSpeed;
    }

    public void ChangeSnowAmount(float amount)
    {
        snowAmount = amount;
        UpdateSnowBehaviour();
    }

    public void ChangeSnowSpeed(float speed)
    {
        snowSpeed = speed;
        UpdateSnowBehaviour();
    }
}
