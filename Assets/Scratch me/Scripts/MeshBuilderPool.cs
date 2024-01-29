using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScratchMe
{
    public class MeshBuilderPool
    {
        private Stack<MeshBuilder> builders = new Stack<MeshBuilder>();

        public MeshBuilder Allocate()
        {
            MeshBuilder builderToAllocate = null;
            if (builders.Count == 0)
                builderToAllocate = new MeshBuilder();
            else
                builderToAllocate = builders.Pop();

            builderToAllocate.Clear();

            return builderToAllocate;
        }

        public void Release(MeshBuilder builder)
        {
            builders.Append(builder);
        }
    }
}