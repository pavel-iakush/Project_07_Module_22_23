using UnityEngine;

public class ResultAgentController/* : MonoBehaviour*/
{
    /*[SerializeField] */private ControlSwitcher _controlSwitcher;

    private Controller _resultController;
    private Controller _lastController;

    public ResultAgentController(ControlSwitcher controlSwitcher)
    {
        _controlSwitcher = controlSwitcher;
    }

    public void Start()
    {
        ProcessControlSwitch();
    }

    public void Update()
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

        _resultController = new CompositeController(_controlSwitcher.CurrentController, _controlSwitcher.RotationController);

        _resultController.Enable();
    }
}