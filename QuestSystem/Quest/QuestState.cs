using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum QuestState
{
    UNDISCOVERED = 0, 

    CAN_START = 1,

    IN_PROGRESS = 2,

    CAN_FINISH = 3,

    FINISHED = 4,
}