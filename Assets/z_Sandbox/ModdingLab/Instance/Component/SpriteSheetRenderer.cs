using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SpriteSheetRenderer : MonoBehaviour
    {
        static Mesh BuildQuad(float width, float height)
        {
            Mesh mesh = new Mesh();

            // Setup vertices
            Vector3[] newVertices = new Vector3[4];
            float halfHeight = height * 0.5f;
            float halfWidth = width * 0.5f;
            newVertices[0] = new Vector3(-halfWidth, -halfHeight, 0);
            newVertices[1] = new Vector3(-halfWidth, halfHeight, 0);
            newVertices[2] = new Vector3(halfWidth, -halfHeight, 0);
            newVertices[3] = new Vector3(halfWidth, halfHeight, 0);

            // Setup UVs
            Vector2[] newUVs = new Vector2[newVertices.Length];
            newUVs[0] = new Vector2(0, 0);
            newUVs[1] = new Vector2(0, 1);
            newUVs[2] = new Vector2(1, 0);
            newUVs[3] = new Vector2(1, 1);

            // Setup triangles
            int[] newTriangles = new int[] { 0, 1, 2, 3, 2, 1 };

            // Setup normals
            Vector3[] newNormals = new Vector3[newVertices.Length];
            for (int i = 0; i < newNormals.Length; i++)
            {
                newNormals[i] = Vector3.forward;
            }

            // Create quad
            mesh.vertices = newVertices;
            mesh.uv = newUVs;
            mesh.triangles = newTriangles;
            mesh.normals = newNormals;

            return mesh;
        }

        SpriteSheet spriteSheet;
        Vector2 spriteSize { get => spriteSheet.textureSize / new Vector2(spriteSheet.width, spriteSheet.height); }
        Vector2 spriteUV { get => Vector2.one / spriteSize; }

        MeshFilter filter;
        Renderer renderer;

        Material material;

        public void Initial()
        {
            filter = GetComponent<MeshFilter>();
            renderer = GetComponent<Renderer>();

            filter.mesh = BuildQuad(1, 1);

            material = new Material(Shader.Find("Unlit/SpriteSheetShader"));
            renderer.material = material;
        }

        public void SetSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;

            material.SetTexture("_MainTex", spriteSheet.texture);
        }
        public void SetSprite(int index, int frameNumber)
        {
            Vector2 offset = spriteUV * new Vector2(frameNumber, index);

            material.SetTextureScale("_MainTex", spriteUV);
            material.SetTextureOffset("_MainTex", offset);
        }
    }

}
