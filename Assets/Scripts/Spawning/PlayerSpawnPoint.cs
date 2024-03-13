using UntitledCube.Maze.Generation;
using UnityEngine;

namespace UntitledCube.Spawning
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private PlayerWrapper _player;
        [SerializeField] private Transform _spawnPoint;

        private void OnEnable() => MazeGenerator.OnGenerated += SpawnPlayer;

        private void OnDisable() => MazeGenerator.OnGenerated -= SpawnPlayer;

        private void SpawnPlayer(string _)
        {
            _player.transform.SetPositionAndRotation(_spawnPoint.position, Quaternion.identity);
            
            foreach (Rigidbody rigidbody in _player.Rigidbodies)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.freezeRotation = true;
                rigidbody.freezeRotation = false;
            }
        }
    }
}