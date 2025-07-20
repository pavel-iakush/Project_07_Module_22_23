using UnityEngine;

public class ClickToMoveAgentController : Controller
{
    private IDirectionalMovable _movable;

    private PointerVisualizer _visualizer;
    private LayerMask _layerMask;
    private RaycastService _raycastService;
    private Camera _camera;

    private int _leftMouseButton = 0;

    public ClickToMoveAgentController(IDirectionalMovable movable, PointerVisualizer visualizer, LayerMask layerMask, RaycastService raycastService, Camera camera)
    {
        _movable = movable;
        _visualizer = visualizer;
        _layerMask = layerMask;
        _raycastService = raycastService;
        _camera = camera;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_raycastService.HasHit(ray.origin, ray.direction, _layerMask, out RaycastHit hit))
            {
                _movable.SetMoveDirection(hit.point);

                _visualizer.VisualizePoint(hit.point);
            }
        }
    }
}