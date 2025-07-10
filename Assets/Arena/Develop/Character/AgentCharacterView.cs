using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsWalking");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }
}