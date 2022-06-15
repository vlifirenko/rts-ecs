using Leopotam.Ecs;
using Rts.Models;
using Rts.Models.Configs;
using UnityEngine;

namespace Rts.Ecs.Systems.Input
{
    public class ControlCameraSystem : IEcsRunSystem
    {
        private SceneData _sceneData;
        private CommonConfig _commonConfig;

        public void Run()
        {
            if (UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                MoveCamera(new Vector3(0, 0, -1f));
            else if (UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.RightArrow))
                MoveCamera(new Vector3(0, 0, 1f));
            else if (UnityEngine.Input.GetKey(KeyCode.W) || UnityEngine.Input.GetKey(KeyCode.UpArrow))
                MoveCamera(new Vector3(-1f, 0, 0f));
            else if (UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.DownArrow))
                MoveCamera(new Vector3(1f, 0, 0f));

            if (Mathf.Abs(UnityEngine.Input.mouseScrollDelta.y) > 0)
                ZoomCamera(UnityEngine.Input.mouseScrollDelta.y);
        }

        private void MoveCamera(Vector3 direction)
        {
            var move = direction * Time.deltaTime * _commonConfig.cameraSpeed;
            _sceneData.mainCamera.transform.Translate(move, Space.World);
        }

        private void ZoomCamera(float zoom)
        {
            var fov = _sceneData.mainCamera.fieldOfView;
            fov -= zoom * Time.deltaTime * _commonConfig.zoomSpeed;
            fov = Mathf.Clamp(fov, _commonConfig.zoomMin, _commonConfig.zoomMax);
            _sceneData.mainCamera.fieldOfView = fov;
        }
    }
}