using System;
using System.Collections.Generic;
using Rts.Models.Character;

namespace Rts.Models
{
    [Serializable]
    public class CharacterStats
    {
        private bool _isDirty;
        private Dictionary<ECharacterStateType, int> _stats = new Dictionary<ECharacterStateType, int>();

        public CharacterStats()
        {
            // todo fill stats
        }

        public int GetStat(ECharacterStateType state)
        {
            if (_isDirty)
                Recalculate();

            return _stats[state];
        }

        public void UpdateStat(ECharacterStateType state, int value)
        {
            _stats[state] = value;
            _isDirty = true;
        }

        private void Recalculate()
        {
            throw new NotImplementedException();
        }
    }
}