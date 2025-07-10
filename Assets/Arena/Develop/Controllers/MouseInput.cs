using UnityEngine;
using UnityEngine.AI;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _layerMask;

    private Controller _characterController;

    private void Awake()
    {
        PointVisualizer pointer = GetComponent<PointVisualizer>();

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _characterController = new CompositeController(
            new MouseClickMovableController(_character.gameObject.GetComponent<NavMeshAgent>(), pointer, _layerMask),
            new AlongMovableVelocityRotatableController(_character, _character));

        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
    }
}