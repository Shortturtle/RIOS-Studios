using UnityEngine;

public class ChangeFurnace : MonoBehaviour
{
    private Renderer objectRenderer;
    public Material materialToPut;
    public Material originalMaterial;

    public int questDisplayNo;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.displayNumber == questDisplayNo)
        {
            if (quest.state.Equals(QuestState.IN_PROGRESS))
            {
                //put material on box
                objectRenderer.material = materialToPut;
            }
            else if (quest.state.Equals(QuestState.CAN_FINISH))
            {
                //put material on box
                objectRenderer.material = originalMaterial;
            }
        }
    }
}
