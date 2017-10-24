using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 rotationOffset;
    public float followSpeed;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(target.transform.rotation.eulerAngles + rotationOffset), followSpeed * Time.deltaTime);
    }
}
