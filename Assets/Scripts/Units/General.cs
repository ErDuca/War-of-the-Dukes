using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : Piece
{
    
    void Start()
    {
        mPhaseOneMovementArray.Add(new Movement(0, -1, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(2, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(0, -1, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(-2, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(-2, -1, Ptype.Jump));
        mPhaseOneMovementArray.Add(new Movement(-2, 1, Ptype.Jump));

        mPhaseOneMovementArray.Add(new Movement(0, -1, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(1, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(2, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(-1, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(-2, 0, Ptype.Move));
        mPhaseOneMovementArray.Add(new Movement(1, -1, Ptype.Command));
        mPhaseOneMovementArray.Add(new Movement(1, -1, Ptype.Command));
        mPhaseOneMovementArray.Add(new Movement(1, 0, Ptype.Command));
        mPhaseOneMovementArray.Add(new Movement(1, 1, Ptype.Command));

        // Mancano 2 command che si sovrappongono





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}