using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AreaPatrolAgentController : Controller
{
    private NavMeshAgent _agent;

    public AreaPatrolAgentController(NavMeshAgent agent)
    {
        _agent = agent;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
