using UnityEngine;

public class SlowZone : MonoBehaviour
{
    PlayerReceiver _player;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerReceiver player))
        {
            _player = player;
            player.DecreaseMovementSpeedFromZone(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_player != null)
            _player.DecreaseMovementSpeedFromZone(false);
    }
}