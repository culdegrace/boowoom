using UnityEngine;
using System.Collections.Generic;

public class DelayedCameraFollower : MonoBehaviour
{
    private struct PointInSpace
    {
        public Vector3 Position;
        public float Time;
    }

    [SerializeField]
    [Tooltip("The transform to follow")]
    private Transform target;

    [SerializeField]
    [Tooltip("Offset on the ground")]
    private Vector3 offset;

    [SerializeField]
    [Tooltip("Offset in the air")]
    private Vector3 airoffset;

    [Tooltip("The delay before the camera starts to follow the target")]
    [SerializeField]
    private float delay = 0.0125f;

    [SerializeField]
    [Tooltip("The speed used in the lerp function when the camera follows the target")]
    private float speed = 5;

    ///<summary>
    /// Contains the positions of the target for the last X seconds
    ///</summary>
    private Queue<PointInSpace> pointsInSpace = new Queue<PointInSpace>();

    void LateUpdate()
    {
        // Add the current target position to the list of positions
        pointsInSpace.Enqueue(new PointInSpace() { Position = target.position, Time = Time.time });

        // Move the camera to the position of the target X seconds ago
        while (pointsInSpace.Count > 0 && pointsInSpace.Peek().Time <= Time.time - delay + Mathf.Epsilon)
        {
            Vector3 newPosition = transform.position;
            var newY = pointsInSpace.Dequeue().Position.y;

            var threshold = 1f;

            var val = Mathf.Min(newY, threshold); // Smoothly interpolate between ground and air offs until threshold units above ground.
            var diff = offset.y - airoffset.y; // Assumes offset.y > airoffset.y.

            var chosenOffs = offset.y - diff * val / threshold;

            newPosition.y = Mathf.Lerp(newPosition.y, pointsInSpace.Dequeue().Position.y + chosenOffs, Time.deltaTime * speed);
            transform.position = newPosition;

        }
    }
}