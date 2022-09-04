using UnityEngine;

namespace ScriptExtensions
{
    public static class CameraExtension
    {
        public static Vector4[] ViewPort2D(this Camera camera)
        {
            float halfHeight = camera.orthographicSize;
            float halfWidth = halfHeight * camera.aspect;
            Matrix4x4 cameraMat = camera.transform.localToWorldMatrix;

            Vector2 viewPortMin = cameraMat.MultiplyPoint3x4(new Vector2(-halfWidth, -halfHeight));
            Vector2 viewPortMax = cameraMat.MultiplyPoint3x4(new Vector2(+halfWidth, +halfHeight));

            return new Vector4[] { viewPortMin, viewPortMax };
        }
        public static Vector2 MousePosition2D(this Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }
        public static Vector2 MousePosition2D(this Camera camera, Matrix4x4 applyMatrix)
        {
            return applyMatrix.MultiplyPoint(camera.MousePosition2D());
        }
        public static Matrix4x4 ScreenUVToWorld2D(this Camera camera)
        {
            Vector2 scale = new Vector2((float)Screen.width / (float)Screen.height, 1f) * camera.orthographicSize * 2;
            Vector2 offset = (Vector2) camera.transform.position - (scale * 0.5f);

            Matrix4x4 matrix = Matrix4x4.TRS(offset, Quaternion.identity, scale);

            return matrix;
        }

    }
}
