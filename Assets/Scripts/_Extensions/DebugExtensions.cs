using UnityEngine;

public static class DebugExtensions
{
    /// <summary>
    /// ������ FOV-����� � ������, �������������� ���� ������.
    /// </summary>
    /// <param name="origin">��������� �����.</param>
    /// <param name="direction">����������� ������.</param>
    /// <param name="fovAngle">���� ������ (� ��������).</param>
    /// <param name="depth">������� ������.</param>
    /// <param name="segments">���������� ��������� ��� ��������� ������.</param>
    public static void DrawFOV(Vector3 origin, Vector3 direction, float fovAngle, float depth = 1f, int segments = 36)
    {
        direction = direction.normalized;

        // �������� ���� FOV
        float halfFov = fovAngle / 2f;

        // ���� ������ FOV
        Vector3 rightBoundary = Quaternion.Euler(0, halfFov, 0) * direction * depth;
        Vector3 leftBoundary = Quaternion.Euler(0, -halfFov, 0) * direction * depth;

        // ��������� ����� ������ FOV
        Debug.DrawLine(origin, origin + rightBoundary, Color.red);
        Debug.DrawLine(origin, origin + leftBoundary, Color.red);

        // ��������� ������
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
