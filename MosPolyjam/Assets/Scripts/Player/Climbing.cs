using UnityEngine;

public class Climbing : MonoBehaviour
{
    [SerializeField] protected Transform _lowerStepChecker;
    [SerializeField] protected Transform _upperStepCheker;
    [SerializeField] protected float _stepHeight;
    [SerializeField] protected float _stepSmooth;
    [SerializeField] protected float _stepDistance;
    [SerializeField] protected float _footLength;
    protected Rigidbody _rb;

    protected void Start()
    {
        Vector3 checkerPos = _lowerStepChecker.position;
        checkerPos.y += _stepHeight;
        _upperStepCheker.position = checkerPos;

        _rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        Debug.DrawLine(_lowerStepChecker.position, _lowerStepChecker.position + transform.forward * _stepDistance);
        Debug.DrawLine(_upperStepCheker.position, _upperStepCheker.position + transform.forward * (_stepDistance + _footLength));
        StepChecker();
    } 

    private void StepChecker()
    {
        RaycastHit lowerHit;
        if (Physics.Raycast(_lowerStepChecker.position, transform.forward, out lowerHit, _stepDistance))
        {
            if(!Physics.Raycast(_upperStepCheker.position, transform.forward, _stepDistance + _footLength))
            {
                RaycastHit hit;
                Vector3 initialPos = lowerHit.point + transform.forward * _footLength;
                initialPos.y = _upperStepCheker.position.y;

                if(Physics.Raycast(initialPos, Vector3.down, out hit))
                {
                    if(Physics.Raycast(hit.point, -transform.forward, out RaycastHit backHit))
                    {
                        if(backHit.collider.CompareTag("Player"))
                        {
                            Vector3 pos = transform.position;
                            pos.y = hit.point.y + 0.0f;
                            transform.position = pos;
                        }
                        else 
                        {
                            transform.position += Vector3.up * _stepSmooth;
                        }
                    }
                }
            }
        }
    }
}