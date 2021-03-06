﻿namespace GameObjects.Conditions.ConditionKindPack
{
    using GameObjects;
    using GameObjects.Conditions;
    using System;

    internal class ConditionKind655 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            foreach (GameObjects.PersonDetail.Skill i in person.Skills.Skills.Values)
            {
                if (i.Influences.HasInfluence(this.number)) return false;
            }
            if (person.PersonalTitle.Influences.HasInfluence(this.number)) return false;
            if (person.CombatTitle.Influences.HasInfluence(this.number)) return false;
            foreach (GameObjects.PersonDetail.Stunt i in person.Stunts.Stunts.Values)
            {
                if (i.Influences.HasInfluence(this.number)) return false;
            }
            return true;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.number = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

