using System.Collections;
using UnityEngine;
using MyCat.Domain;

namespace MyCat.Runtime
{
    public class Cat : MonoBehaviour
    {
        private CatVisual _catVisual;
        private StateType _currentState = StateType.NotDetermined;
        private bool _isMoving = false;

        [SerializeField] private Transform _wayPointHolder;
        private Transform _currentWayPoint;

        private void Awake()
        {
            _catVisual = GetComponentInChildren<CatVisual>();
        }

        private void OnEnable()
        {
            StartCoroutine(PeriodicRun());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            if (_isMoving && _currentWayPoint != null)
            {
                Vector3 deltaPosition = _currentWayPoint.position - transform.position;
                if (deltaPosition.sqrMagnitude > 0.05f)
                {
                    if (deltaPosition.x > 0)
                    {
                        _catVisual.SetLookDirection(true, true);
                    }
                    else
                    {
                        _catVisual.SetLookDirection(false,true);
                    }
                    
                    transform.Translate(deltaPosition.normalized * Time.deltaTime);
                }
                else
                {
                    _isMoving = false;
                    _catVisual.SetIsMoving(_isMoving);
                }
            }
        }

        private IEnumerator PeriodicRun()
        {
            float elapsedTimeFromLastMoving = 0.0f;
            float movingInterval = 5.0f;
            var waitForSeconds = new WaitForSeconds(1f);
            while (true)
            {
                // 일정 시간마다 상태 갱신
                if (GameManager.Instance.CurrentStatus != null)
                {
                    StateType newState = GameManager.Instance.CurrentStatus.GetState();
                    if (_currentState != newState)
                    {
                        _currentState = newState;
                        _catVisual.SetCatState(_currentState);
                    }
                }

                if (_isMoving == false)
                {
                    if (elapsedTimeFromLastMoving > movingInterval)
                    {
                        elapsedTimeFromLastMoving = 0.0f;
                        int wayPointIndex = UnityEngine.Random.Range(0, _wayPointHolder.childCount);
                        _currentWayPoint = _wayPointHolder.GetChild(wayPointIndex);
                        _isMoving = true;
                        _catVisual.SetIsMoving(_isMoving);
                    }
                    else
                    {
                        elapsedTimeFromLastMoving += 1.0f;
                    }
                }

                yield return waitForSeconds;
            }
        }
    }
}