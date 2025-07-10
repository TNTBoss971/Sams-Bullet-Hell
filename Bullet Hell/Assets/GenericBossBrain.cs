using UnityEngine;
public class GenericBossBrain : MonoBehaviour
{
    private GameObject player;
    [Header("Limbs")]
    public GameObject mainBody0;
    public GameObject armLeft0;
    public GameObject armRight0;
    [Header("Attacks")]
    public string[] weaponSystems; // all systems (leftMaxigun, leftMiniguns, rightMaxigun, rightMiniguns, centerMaxiguns)
    // to avoid long variable names, i have shortened weaponSystems to wS
    public string[] mainBodyWS; // centerMaxiguns
    public string[] armLeftWS; // leftMaxigun, leftMiniguns
    public string[] armRightWS; // rightMaxigun, rightMiniguns
    [Header("Control")]
    public float cooldownTime;
    public float cooldownLength;

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
            if ((transform.position - player.transform.position).magnitude > 5)
            {
                int decision;
                decision = Random.Range(0, 3);
                if (decision == 0)
                {
                    print("centerMaxiguns");
                    cooldownTime = Time.time + cooldownLength;
                }
                else if (decision == 1)
                {
                    if (armLeft0.GetComponent<BossLimbLogic>().destroyed)
                    {
                        print("limb needed for attempted attack is destroyed");
                    }
                    else
                    {
                        print("leftMaxigun");
                    }
                    cooldownTime = Time.time + cooldownLength;
                }
                else if (decision == 2)
                {
                    if (armRight0.GetComponent<BossLimbLogic>().destroyed)
                    {
                        print("limb needed for attempted attack is destroyed");
                    }
                    else
                    {
                        print("rightMaxigun");
                    }
                    cooldownTime = Time.time + cooldownLength;
                }
            }
            else
            {
                int decision;
                decision = Random.Range(0, 2);
                if (decision == 0)
                {
                    if (armLeft0.GetComponent<BossLimbLogic>().destroyed)
                    {
                        print("limb needed for attempted attack is destroyed");
                    }
                    else
                    {
                        print("leftMiniguns");
                    }
                    cooldownTime = Time.time + cooldownLength;
                }
                else if (decision == 1)
                {
                    if (armRight0.GetComponent<BossLimbLogic>().destroyed)
                    {
                        print("limb needed for attempted attack is destroyed");
                    }
                    else
                    {
                        print("rightMiniguns");
                    }
                    cooldownTime = Time.time + cooldownLength;
                }
            }
        }
    }
}
