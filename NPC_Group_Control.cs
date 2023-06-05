using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Group_Control : MonoBehaviour
{
    public GameObject NPCPrefabRef;
    [SerializeField]
    private int NPCNum = 10;
    public NPCColour initialColor = NPCColour.Blue;
    public Vector3 SpawnPosition = new Vector3 ( 0.0f,0.0f,0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NPCGenerator()
    {
        for (int i = 0; i<NPCNum;i++)
        {
            GameObject NPCObject = Instantiate(NPCPrefabRef, SpawnPosition, Quaternion.identity);
            NPC npcComponent = NPCObject.GetComponent<NPC>();
            SpawnPosition.x += 2; 
            npcComponent.SetColour(initialColor);
        }
       
    }


    public class Group
    {

        static int CurrentGroupID = 0;
        public int GroupID { get; private set; }

        private List<NPC> npcInGroup = new List<NPC>();

        public Group()
        {
            AssignGroupID();
            string GroupTag = "GroupEncapsulator";
            GroupTag += GroupID.ToString();
            GameObject GroupEncapsulator = new GameObject(GroupTag);
        }
        public void AddNPC(NPC npc)
        {
            npcInGroup.Add(npc);
        }

        private void AssignGroupID()
        {
            GroupID = CurrentGroupID;
            CurrentGroupID++;
        }
    }
}
