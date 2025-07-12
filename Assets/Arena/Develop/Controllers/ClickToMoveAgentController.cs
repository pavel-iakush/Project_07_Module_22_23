using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveAgentController : Controller
{
    private NavMeshAgent _agent;
    private PointerVisualizer _visualizer;
    private LayerMask _layerMask;

    private int _leftMouseButton = 0;

    public ClickToMoveAgentController(NavMeshAgent agent, PointerVisualizer visualizer, LayerMask layerMask)
    {
        _agent = agent;
        _visualizer = visualizer;
        _layerMask = layerMask;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _layerMask))
            {
                _agent.destination = hit.point;
                _visualizer.VisualizePoint(hit.point);
            }
        }
    }
}