using UnityEngine;

public class PlayerControl : IControllable
{
    private AgentCharacter _character;
    private LayerMask _layerMask;
    private RaycastService _raycastService;
    private Camera _camera;

    public PlayerControl(AgentCharacter character, LayerMask layerMask, Camera camera)
    {
        _character = character;
        _layerMask = layerMask;
        _camera = camera;
    }

    public Controller Initialize()
    {
        _raycastService = new RaycastService();

        return new CompositeController(
                    new ClickToMoveAgentController(_character, _layerMask, _raycastService, _camera),
                    new AlongMovableVelocityRotatableController(_character, _character));
    }
}