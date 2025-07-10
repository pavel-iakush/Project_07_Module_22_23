using UnityEngine;
using UnityEngine.AI;

public class MouseClickMovableController : Controller
{
    private NavMeshAgent _agent;
    private PointVisualizer _visualizer;
    private LayerMask _layerMask;

    public MouseClickMovableController(NavMeshAgent agent, PointVisualizer visualizer, LayerMask layerMask)
    {
        _agent = agent;
        _visualizer = visualizer;
        _layerMask = layerMask;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
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