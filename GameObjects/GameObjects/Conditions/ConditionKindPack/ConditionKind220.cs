﻿namespace GameObjects.Conditions.ConditionKindPack
{
    using GameObjects;
    using GameObjects.Conditions;
    using System;

    internal class ConditionKind220 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            return (person.QibingExperience >= this.number);
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

