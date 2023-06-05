using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NPCColour
{
    Blue,
    Green,
    Red,
    Yellow
}

public class NPC : MonoBehaviour
{
    [SerializeField]
    private int life = 100;
    [SerializeField]
    private int damage = 10;
    bool alive = true;
    bool attacking = false;
    public GameObject sphereObject;
    public Vector3 enemyDirection{ get; set;} = new Vector3(0f,0f,1f);
    private const int firingRange = 45;
    private float randNum;

    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    private Renderer NPCRenderer;

    private float timer = 0.0f;
    public float timeBetweenAttacks = 2.5f;

    static int currentID = 0;
    public int ID {  get; private set; }
    public NPCColour type { get; private set; }

    public NPC()
    {
        //NPCRenderer.material = blueMaterial;
        AssignID();
    }

    // Start is called before the first frame update
    void Start()
    {
        type = NPCColour.Blue;
        NPCRenderer = GetComponent<Renderer>();
        NPCRenderer.material = blueMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            alive = false;
            sphereObject.SetActive(false);
        }

        if (attacking)
        {
            Attacking(Time.deltaTime);
        }
    }

    void Attack()
    {
        randNum = Random.Range(-firingRange, firingRange + 1);
        Quaternion rotate = Quaternion.Euler(0f,randNum,0f);
        Vector3 firingDir = rotate* enemyDirection;

        Ray ray = new Ray(transform.position,firingDir);
        RaycastHit ContactInfo;

        if(Physics.Raycast(ray, out ContactInfo))
        {
            if (ContactInfo.collider.CompareTag("NPC"))
            {
                GameObject hitNPC = ContactInfo.collider.gameObject;

                NPC struckNPC = hitNPC.GetComponent<NPC>();
                if(struckNPC != null)
                {
                    struckNPC.life -= damage;
                }
                
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
    }

    void Attacking(float dt)
    {
        if(timer < timeBetweenAttacks)
        {
            timer += dt;
            return;
        }
        else
        {
            timer = 0;
            Attack();
        }
    }

    public void SetColour(NPCColour newColour)
    {
        type = newColour;
        ChangeNPCMaterial(newColour);
    }

    private void ChangeNPCMaterial(NPCColour newColour)
    {
        type = newColour;
        switch (newColour)
        {
            case NPCColour.Blue:
                NPCRenderer.material = blueMaterial;
                break;
            case NPCColour.Green:
                NPCRenderer.material = greenMaterial;
                break;
            case NPCColour.Red:
                NPCRenderer.material = redMaterial;
                break;
            case NPCColour.Yellow:
                NPCRenderer.material = yellowMaterial;
                break;
        }
    }



    private void AssignID()
    {
        ID = currentID;
        currentID++;
    }

}
