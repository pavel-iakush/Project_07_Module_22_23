using UnityEngine;
using UnityEngine.AI;

public class MouseInput : MonoBehaviour, IBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _layerMask;

    private Controller _characterController;

    private void Awake()
    {
        PointerVisualizer pointer = GetComponent<PointerVisualizer>();

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _characterController = new CompositeController(
            new ClickToMoveAgentController(_character.gameObject.GetComponent<NavMeshAgent>(), pointer, _layerMask),
            new AlongMovableVelocityRotatableController(_character, _character));

        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
    }
}