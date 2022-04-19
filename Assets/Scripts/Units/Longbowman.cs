using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longbowman : Piece
{
    void Start()
    {
        mPhaseOneMovementArray.Add(new Movement(0, -1, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(1, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(0, 1, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(-1, 0, Ptype.Move));

        mPhaseTwoMovementArray.Add(new Movement(-1, 1, Ptype.Move));
        mPhaseTwoMovementArray.Add(new Movement(1, 1, Ptype.Move));
        mPhaseTwoMovementArray.Add(new Movement(0, -2, Ptype.Strike));
        mPhaseTwoMovementArray.Add(new Movement(0, -3, Ptype.Strike));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}