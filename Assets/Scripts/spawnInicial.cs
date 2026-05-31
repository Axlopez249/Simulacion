using UnityEngine;


public class TeleportOnStart : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider provider;
    public UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationAnchor anchor;

    void Start()
    {
        if (provider != null && anchor != null)
        {
            UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportRequest request = new UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportRequest
            {
                destinationPosition = anchor.transform.position,
                destinationRotation = anchor.transform.rotation
            };
            provider.QueueTeleportRequest(request);
        }
    }
}
