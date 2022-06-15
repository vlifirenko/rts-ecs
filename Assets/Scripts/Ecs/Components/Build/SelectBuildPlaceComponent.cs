using System.Collections.Generic;
using Rts.Models.Configs;
using Rts.View.Scene.Structure;
using UnityEngine;

namespace Rts.Ecs.Components.Build
{
    public struct SelectBuildPlaceComponent
    {
        public AStructureView HintView;
        public StructureConfig Config;
        public Dictionary<Renderer, Material> SavedMaterials;
    }
}