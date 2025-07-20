using UnityEngine.AI;

public class AgentAIControl : IControllable
{
    private AgentCharacter _character;
    private NavMeshQueryFilter _queryFilter;

    public AgentAIControl(AgentCharacter character, NavMeshQueryFilter queryFilter)
    {
        _character = character;
        _queryFilter = queryFilter;
    }

    public Controller Initialize()
    {
        return new CompositeController(
                    new AreaPatrolAgentController(_character, _queryFilter),
                    new AlongMovableVelocityRotatableController(_character, _character));
    }
}