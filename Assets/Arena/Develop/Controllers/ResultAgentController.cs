using UnityEngine;

public class ResultAgentController : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private ControlSwitcher _controlSwitcher;

    private Controller _resultController;

    private void Start()
    {
        if (_controlSwitcher == null)
        {
            _controlSwitcher = GetComponent<ControlSwitcher>();
        }

        SetResultController();
        _resultController.Enable();
    }

    private void Update()
    {
            _resultController.Update(Time.deltaTime);
    }

    private void SetResultController()
    {
        _resultController = new CompositeController(
            _controlSwitcher.CurrentController,
            new AlongMovableVelocityRotatableController(_character, _character));
    }
}