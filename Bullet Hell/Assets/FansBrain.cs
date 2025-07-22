using UnityEngine;

public class FansBrain : MonoBehaviour
{
    private GameObject player;
    [Header("Limbs")]
    public GameObject mainBody0;
    public GameObject leftFan;
    public GameObject rightFan;
    [Header("Attacks")]
    public string[] weaponSystems; // all systems (leftSlice, leftGust, rightSlice, rightGust, freezeBlast, fanMinionSummon)
    // to avoid long variable names, i have shortened weaponSystems to wS
    public string[] mainBodyWS; // freezeBlast, fanMinionSummon
    public string[] armLeftWS; // leftSlice, leftGust
    public string[] armRightWS; // rightSlice, rightGust
    [Header("Control")]
    public float cooldownTime;
    public float cooldownLength;
    public bool freezeActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = this.gameObject.GetComponent<BossData>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > cooldownTime)
        {
            if ((transform.position - player.transform.position).magnitude > 5) // mellee
            {
                if (!freezeActive)
                {
                    print("freezeBlast");
                }
                else if (freezeActive)
                {
                    if (Random.Range(0, 1) == 1)
                    {
                        if (leftFan.GetComponent<BossLimbLogic>().destroyed)
                        {
                            paralazed();
                        }
                        else
                        {
                            print("leftGust");
                        }
                    }
                    else
                    {
                        if (leftFan.GetComponent<BossLimbLogic>().destroyed)
                        {
                            paralazed();
                        }
                        else
                        {
                            print("rightGust");
                        }
                    }
                }

                cooldownTime = Time.time + cooldownLength;

            }
            else // ranged
            {
                
            }
        }
    }
    void paralazed()
    {
        print("limb needed for attempted attack is destroyed");
    }
}
