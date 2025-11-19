using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    //the unique name for the quest (it uses the name u made for the scriptable object)
    [field: SerializeField] public string id {  get; private set; }

    //the values and stuff for the quest
    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    //public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int towerReward;

    //ensure id is always the name of Scriptable Object asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
