using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 8f;
    public float offsetX = 2f;
    public float offsetY = 1.5f;

    void LateUpdate()
    {
        if (target == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) target = p.transform;
            return;
        }

        var desired = new Vector3(
            target.position.x + offsetX,
            target.position.y + offsetY,
            transform.position.z);

        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}
