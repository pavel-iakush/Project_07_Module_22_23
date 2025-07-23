using UnityEngine;

public class ResultAgentController : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private ControlSwitcher _controlSwitcher;

    private Controller _resultController;
    private Controller _lastController;

    private void Start()
    {
        ProcessControlSwitch();
    }

    private void Update()
    {
        if (_controlSwitcher.CurrentController != _lastController)
        {
            _resultController.Disable();

            ProcessControlSwitch();
        }

        _resultController.Update(Time.deltaTime);
    }

    private void ProcessControlSwitch()
    {
        _lastController = _controlSwitcher.CurrentController;

        _resultController = new CompositeController(
                                _controlSwitcher.CurrentController,
                                new AlongMovableVelocityRotatableController(_character, _character));

        _resultController.Enable();
    }
}