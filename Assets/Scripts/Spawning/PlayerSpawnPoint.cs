using UntitledCube.Maze.Generation;
using UnityEngine;
using UntitledCube.Player;

namespace UntitledCube.Spawning
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _spawnPoint;

        private PlayerWrapper _playerWrapper;

        private void OnEnable() => MazeGenerator.OnGenerated += SpawnPlayer;

        private void OnDisable() => MazeGenerator.OnGenerated -= SpawnPlayer;

        private void Awake() => _playerWrapper = _player.GetComponent<PlayerWrapper>();

        private void SpawnPlayer(string _)
        {
            _player.transform.SetPositionAndRotation(_spawnPoint.position, Quaternion.identity);
            
            foreach (Rigidbody rigidbody in _playerWrapper.Rigidbodies)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.freezeRotation = true;
                rigidbody.freezeRotation = false;
            }
        }
    }
}