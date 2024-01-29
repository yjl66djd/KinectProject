using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScratchMe
{
    public class MeshBuilder
    {
        public Mesh Mesh { get; private set; }
        public Brush Brush { get; set; }

        private static List<Vector3> vertecies = new List<Vector3>();
        private static List<Vector2> uvs = new List<Vector2>();
        private static List<Color> colors = new List<Color>();
        private static List<int> triangles = new List<int>();

        private struct Node
        {
            public readonly float Scale;
            public readonly Vector3 Position;
            public readonly Color Color;

            public Node(Color color, float radius, Vector3 position)
            {
                Color = color;
                Scale = radius;
                Position = position;
            }
        }

        private float shift = 0.0f;
        private int triangleIndex = 0;

        private List<Node> nodes = new List<Node>();

        public void Append(Color color, float radius, Vector3 position)
        {
            nodes.Add(new Node(color, radius, position));
            UpdateMesh();
        }

        public void ClearPrevious(bool fullClear = false)
        {
            uvs.Clear();
            triangles.Clear();
            vertecies.Clear();
            triangleIndex = 0;

            var requiredAmount = fullClear ? 0 : 1;
            while (nodes.Count > requiredAmount)
                nodes.RemoveAt(0);

            if (Mesh != null)
                Mesh.Clear();
        }

        public void Clear()
        {
            uvs.Clear();
            triangles.Clear();
            nodes.Clear();
            vertecies.Clear();
            triangleIndex = 0;
            shift = 0.0f;

            if (Mesh != null)
                Mesh.Clear();
        }

        private void UpdateMesh()
        {
            var positionsCount = nodes.Count;
            if (positionsCount == 0)
                return;

            if (Mesh == null)
            {
                Mesh = new Mesh();
                //Mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            }

            if (positionsCount == 1)
            {
                //BuildLine(-Brush.Spacing, nodes[0], nodes[0], ref triangleIndex);
            }
            else
            {
                for (var index = 1; index < positionsCount; index++)
                    shift = BuildLine(shift, nodes[index - 1], nodes[index]);

                var beforeLastNode = nodes[positionsCount - 2];
                var lastNode = nodes[positionsCount - 1];

                nodes.Clear();

                nodes.Add(beforeLastNode);
                nodes.Add(lastNode);
            }

            Mesh.Clear();
            Mesh.SetVertices(vertecies);
            Mesh.SetUVs(0, uvs);
            Mesh.SetColors(colors);
            Mesh.SetTriangles(triangles, 0);

            Mesh.UploadMeshData(false);
        }

        private float BuildLine(float shift, Node start, Node end)
        {
            var difference = end.Position - start.Position;
            var direction = difference.normalized;
       
            var length = difference.magnitude;
            if (length == 0.0f)
                return shift;

            var position = shift;
            var brushSize = Brush.Size;

            var up = Vector3.up;
            var right = Vector3.right;

            var step = 0.0f;
            var addedCount = 0;
            do
            {
                var transformation = Matrix4x4.Rotate(Quaternion.Euler(Vector3.forward * Brush.Rotation.Value));
                if (Brush.AlignWithDirection)
                    transformation = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, direction)) * transformation;

                step = Mathf.Max(1.0f, Brush.Spacing.Value);
                position += step;
                if ((position - step) < step)
                    continue;

                addedCount++;
                var ratio = ((position - step) - shift) / length;
                var size = Mathf.Lerp(start.Scale, end.Scale, ratio) * brushSize.Value * 0.5f;
                var strokePosition = Vector3.Lerp(start.Position, end.Position, ratio);

                var upDir = (Vector3)(transformation * up * size);
                var rightDir = (Vector3)(transformation * right * size);
                vertecies.Add((upDir + rightDir) + strokePosition);
                vertecies.Add((rightDir - upDir) + strokePosition);
                vertecies.Add((-upDir - rightDir) + strokePosition);
                vertecies.Add((-rightDir + upDir) + strokePosition);

                uvs.Add(new Vector2(1.0f, 0.0f));
                uvs.Add(new Vector2(1.0f, 1.0f));
                uvs.Add(new Vector2(0.0f, 1.0f));
                uvs.Add(new Vector2(0.0f, 0.0f));

                triangles.Add(triangleIndex);
                triangles.Add(triangleIndex + 1);
                triangles.Add(triangleIndex + 2);
                triangles.Add(triangleIndex);
                triangles.Add(triangleIndex + 2);
                triangles.Add(triangleIndex + 3);

                triangleIndex += 4;
            }
            while (position <= length);

            if (addedCount == 0)
                return shift + length;

            position -= step;

            return length - position;
        }
    }
}