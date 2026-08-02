using UnityEngine;

[System.Serializable]
public class QuestData
{
    public QuestState state;

    public int questStepIndex;

    public QuestStepState[] questStepStates;

    //required by jsonutility for deserialization (wtf how was i supposed to know)
    public QuestData() { }

    public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates)
    {
        this.state = state;
        this.questStepIndex = questStepIndex;
        this.questStepStates = questStepStates;
    }
}
