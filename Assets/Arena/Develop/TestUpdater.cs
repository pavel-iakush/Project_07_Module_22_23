using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpdater : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Camera _camera;

    private ControlSwitcher _controlSwitcher;
    private ResultAgentController _resultAgentController;


    private void Awake()
    {
        _controlSwitcher = new ControlSwitcher(_character, _layerMask, _camera);

        _controlSwitcher.Awake();

        _resultAgentController = new ResultAgentController(_controlSwitcher);

        _resultAgentController.Start();
    }


    private void Update()
    {
        _controlSwitcher.Update();

        _resultAgentController.Update();
    }
}
