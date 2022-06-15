using UnityEngine;

namespace Rts.Utils
{
    /// <summary>
    /// Small helper class to convert viewport, screen or world positions to canvas space.
    /// Only works with screen space canvases.
    /// Usage:
    /// objectOnCanvasRectTransform.anchoredPosition = specificCanvas.WorldToCanvasPoint(worldspaceTransform.position);
    /// </summary>
    public static class CanvasPositioningExtensions
    {
        public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera)
        {
            var viewportPosition = camera.WorldToViewportPoint(worldPosition);
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }

        public static Vector3 ScreenToCanvasPosition(this Canvas canvas, Vector3 screenPosition)
        {
            var viewportPosition = new Vector3(screenPosition.x / UnityEngine.Screen.width,
                                               screenPosition.y / UnityEngine.Screen.height,
                                               0);
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }

        public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
        {
            var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.sizeDelta;
            return Vector3.Scale(centerBasedViewPortPosition, scale);
        }
    }
}