using UnityEngine;

public static class DebugExtensions
{
    /// <summary>
    /// Рисует FOV-конус и кольцо, представляющее поле зрения.
    /// </summary>
    /// <param name="origin">Начальная точка.</param>
    /// <param name="direction">Направление конуса.</param>
    /// <param name="fovAngle">Угол обзора (в градусах).</param>
    /// <param name="depth">Глубина конуса.</param>
    /// <param name="segments">Количество сегментов для рисования кольца.</param>
    public static void DrawFOV(Vector3 origin, Vector3 direction, float fovAngle, float depth = 1f, int segments = 36)
    {
        direction = direction.normalized;

        // Половина угла FOV
        float halfFov = fovAngle / 2f;

        // Края границ FOV
        Vector3 rightBoundary = Quaternion.Euler(0, halfFov, 0) * direction * depth;
        Vector3 leftBoundary = Quaternion.Euler(0, -halfFov, 0) * direction * depth;

        // Рисование линий границ FOV
        Debug.DrawLine(origin, origin + rightBoundary, Color.red);
        Debug.DrawLine(origin, origin + leftBoundary, Color.red);

        // Рисование кольца
        float angleStep = fovAngle / segments;
        Vector3 previousPoint = origin + (Quaternion.Euler(0, -halfFov, 0) * direction * depth);

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -halfFov + angleStep * i;
            Vector3 currentPoint = origin + (Quaternion.Euler(0, currentAngle, 0) * direction * depth);
            Debug.DrawLine(previousPoint, currentPoint, Color.green);
            previousPoint = currentPoint;
        }
    }
}
