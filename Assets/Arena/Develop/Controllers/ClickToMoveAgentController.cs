using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveAgentController : Controller
{
    private IDirectionalMovable _movable;

    private PointerVisualizer _visualizer;
    private LayerMask _layerMask;

    private int _leftMouseButton = 0;

    public ClickToMoveAgentController(IDirectionalMovable movable, PointerVisualizer visualizer, LayerMask layerMask)
    {
        _movable = movable;
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
                _movable.SetMoveDirection(hit.point);

                _visualizer.VisualizePoint(hit.point);
            }
        }
    }
}