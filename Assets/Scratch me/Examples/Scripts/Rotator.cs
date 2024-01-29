using UnityEngine;

namespace ScratchMe.Examples
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 360.0f;

        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
    }
}
