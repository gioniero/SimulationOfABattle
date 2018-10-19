
using UnityEngine;
namespace CSToU
{
    public interface IMovable
    {
        void MoveTo(Vector3 targetPosition);

        void Wait(float timeToWait);

        Vector3 AvoidPosition();

    }
}
