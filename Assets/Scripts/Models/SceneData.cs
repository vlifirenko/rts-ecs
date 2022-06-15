using Rts.Factory;
using Rts.Factory.Structure;
using Rts.Factory.Unit;
using Rts.View.Scene.Character;
using Rts.View.Scene.Resource;
using Rts.View.Scene.Structure;
using Rts.View.Scene.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rts.Models
{
    public class SceneData : MonoBehaviour
    {
        public UnitContainer unitContainer;
        public StructuresContainer structuresContainer;
        public MineContainer mineContainer;
        public CharacterContainer characterContainer;
        public Camera mainCamera;
        public AUnitFactory unitFactory;
        public AStructureFactory structureFactory;
    }
}