using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duke : ErDucaPiece
{
    void Start()
    {
        /*
        _PhaseOneMovementArray.Add(new Movement(0, -1, Ptype.Walk));
        _PhaseOneMovementArray.Add(new Movement(1, 0, Ptype.Walk));
        _PhaseOneMovementArray.Add(new Movement(0, 1, Ptype.Walk));
        _PhaseOneMovementArray.Add(new Movement(-1, 0, Ptype.Walk));
        */

        _PhaseOneMovementArray.Add(new Movement(0,1, Ptype.Slide));
        _PhaseOneMovementArray.Add(new Movement(1,0, Ptype.Slide));
        _PhaseOneMovementArray.Add(new Movement(0,-1, Ptype.Slide));
        _PhaseOneMovementArray.Add(new Movement(-1,0, Ptype.Slide));

        /*
        _PhaseOneMovementArray.Add(new Movement(-1, 0, Ptype.Slide));
        _PhaseOneMovementArray.Add(new Movement(0, 1, Ptype.Slide));
        _PhaseOneMovementArray.Add(new Movement(0, -1, Ptype.Slide));
        */
    }
}

