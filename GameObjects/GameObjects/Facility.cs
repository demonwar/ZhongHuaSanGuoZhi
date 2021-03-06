﻿namespace GameObjects
{
    using GameObjects.ArchitectureDetail;
    using GameObjects.Influences;
    using System;

    public class Facility : GameObject
    {
        private int endurance;
        private FacilityKind kind;
        private int kindID;


        public void DecreaseEndurance(int decrement)
        {
            this.endurance -= decrement;
        }

        public void DoWork(Architecture a)
        {
            foreach (Influence influence in this.Kind.Influences.Influences.Values)
            {
                influence.DoWork(a);
            }
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Kind.GetInfluenceList();
        }

        public GameObjectList GetConditionList()
        {
            return this.Kind.GetConditionList();
        }

        public void RecoverEndurance(int extraInc)
        {
            if (this.endurance != this.EnduranceCeiling)
            {
                int increase = (this.EnduranceCeiling / this.Days) / 2 + extraInc;
                this.endurance += increase < 1 ? 1 : increase;
                if (this.endurance > this.EnduranceCeiling)
                {
                    this.endurance = this.EnduranceCeiling;
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public int Days
        {
            get
            {
                return this.Kind.Days;
            }
        }

        public string Description
        {
            get
            {
                return this.Kind.Description;
            }
        }

        public string ConditionString
        {
            get
            {
                return this.Kind.ConditionString;
            }
        }

        public int Endurance
        {
            get
            {
                return this.endurance;
            }
            set
            {
                this.endurance = value;
            }
        }

        public int EnduranceCeiling
        {
            get
            {
                return this.Kind.Endurance;
            }
        }

        public int FundCost
        {
            get
            {
                return this.Kind.FundCost;
            }
        }

        public int InfluenceCount
        {
            get
            {
                return this.Kind.Influences.Count;
            }
        }

        public InfluenceTable Influences
        {
            get
            {
                return this.Kind.Influences;
            }
        }

        public FacilityKind Kind
        {
            get
            {
                if (this.kind == null)
                {
                    this.kind = base.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.kindID);
                }
                return this.kind;
            }
            set
            {
                this.kind = value;
                if (this.kind != null)
                {
                    this.kindID = this.kind.ID;
                }
            }
        }

        public int KindID
        {
            get
            {
                return this.kindID;
            }
            set
            {
                this.kindID = value;
            }
        }

        public string KindString
        {
            get
            {
                return this.Kind.Name;
            }
        }

        public int MaintenanceCost
        {
            get
            {
                return this.Kind.MaintenanceCost;
            }
        }

        public string Name
        {
            get
            {
                return this.KindString;
            }
        }

        public int PointCost
        {
            get
            {
                return this.Kind.PointCost;
            }
        }

        public int PositionOccupied
        {
            get
            {
                return this.Kind.PositionOccupied;
            }
        }

        public int TechnologyNeeded
        {
            get
            {
                return this.Kind.TechnologyNeeded;
            }
        }

        public bool UniqueInArchitecture
        {
            get
            {
                return this.Kind.UniqueInArchitecture;
            }
        }

        public string UniqueInArchitectureString
        {
            get
            {
                return this.Kind.UniqueInArchitectureString;
            }
        }

        public bool UniqueInFaction
        {
            get
            {
                return this.Kind.UniqueInFaction;
            }
        }

        public string UniqueInFactionString
        {
            get
            {
                return this.Kind.UniqueInFactionString;
            }
        }

        public Architecture location
        {
            get
            {
                foreach (Architecture a in base.Scenario.Architectures)
                {
                    if (a.Facilities.GameObjects.Contains(this))
                    {
                        return a;
                    }
                }
                return null;
            }
        }

        public double AIValue
        {
            get
            {
                return this.Kind.AIValue(this.location);
            }
        }
    }
}

