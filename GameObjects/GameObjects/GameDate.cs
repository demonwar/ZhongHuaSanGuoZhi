﻿namespace GameObjects
{
    using System;
    using System.Runtime.CompilerServices;

    public class GameDate
    {
        public int Day = 1;
        public int DaysLeft;
        public bool IsRunning = false;
        public int Month = 1;
        public GameSeason Season;
        public int Year = 0xb8;

        public event DayPassedEvent OnDayPassed;

        public event DayRunningEvent OnDayRunning;

        public event DayStartingEvent OnDayStarting;

        public event MonthPassedEvent OnMonthPassed;

        public event MonthRunningEvent OnMonthRunning;

        public event MonthStartingEvent OnMonthStarting;

        public event SeasonChangeEvent OnSeasonChange;

        public event YearPassedEvent OnYearPassed;

        public event YearRunningEvent OnYearRunning;

        public event YearStartingEvent OnYearStarting;

        public bool EndRunning()
        {
            if (!this.IsRunning)
            {
                return false;
            }
            if ((this.OnDayPassed != null) && !this.OnDayPassed())
            {
                return false;
            }
            if (this.Day == 30)
            {
                if ((this.OnMonthPassed != null) && !this.OnMonthPassed())
                {
                    return false;
                }
                if ((this.Month == 12) && ((this.OnYearPassed != null) && !this.OnYearPassed()))
                {
                    return false;
                }
            }
            this.IsRunning = false;
            return true;
        }

        public float GetFoodRateBySeason(GameSeason season)
        {
            switch (season)
            {
                case GameSeason.春:
                    return 0.6f;

                case GameSeason.夏:
                    return 1f;

                case GameSeason.秋:
                    return 1f;

                case GameSeason.冬:
                    return 0.3f;
            }
            return 0f;
        }

        public GameSeason GetSeason(int dayslater)
        {
            if (((this.Day + dayslater) > 30) && ((this.Month % 3) == 0))
            {
                return (GameSeason.春 + ((int)this.Season % (int)GameSeason.冬));
            }
            return this.Season;
        }

        public void Go()
        {
            this.Day++;
            if (this.Day > 30)
            {
                this.Day = 1;
                this.Month++;
                if (this.Month > 12)
                {
                    this.Month = 1;
                    this.Year++;
                }
                this.SetSeason();
            }
            if (this.DaysLeft > 0)
            {
                this.DaysLeft--;
            }

        }

        public void Go(int i)
        {
            this.Day += i;
            while (this.Day > 30)
            {
                this.Day -= 30;
                this.Month++;
                if (this.Month > 12)
                {
                    this.Month = 1;
                    this.Year++;
                }
                this.SetSeason();
            }
        }

        public void LoadDateData(int year, int month, int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.SetSeason();
        }

        public void SetSeason()
        {
            GameSeason season = this.Season;
            if (this.Month >= 3 && this.Month<=5)
            {
                this.Season = GameSeason.春;
            }
            else if (this.Month >= 6 && this.Month <= 8)
            {
                this.Season = GameSeason.夏;
            }
            else if (this.Month >= 9 && this.Month <= 11)
            {
                this.Season = GameSeason.秋;
            }
            else
            {
                this.Season = GameSeason.冬;
            }
            if ((season != this.Season) && (this.OnSeasonChange != null))
            {
                this.OnSeasonChange(this.Season);
            }
        }

        public bool StartRunning()
        {
            if (this.IsRunning)
            {
                return false;
            }
            if ((this.OnDayStarting != null) && !this.OnDayStarting())
            {
                return false;
            }
            if (this.Day == 1)
            {
                if ((this.OnMonthStarting != null) && !this.OnMonthStarting())
                {
                    return false;
                }
                if ((this.Month == 1) && ((this.OnYearStarting != null) && !this.OnYearStarting()))
                {
                    return false;
                }
            }
            this.IsRunning = true;
            return true;
        }

        public string ToDateString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return string.Concat(new object[] { this.Year, "年", this.Month, "月", this.Day, "日" });
        }

        public int LeftDays
        {
            get
            {
                return (360 - this.PassedDays);
            }
        }

        public int PassedDays
        {
            get
            {
                return ((this.Month * 30) + this.Day);
            }
        }

        public GameDate() { }

        public GameDate(int y, int m, int d)
        {
            Year = y;
            Month = m;
            Day = d;
        }

        public GameDate(GameDate d)
        {
            Year = d.Year;
            Month = d.Month;
            Day = d.Day;
        }
        

        public delegate bool DayPassedEvent();

        public delegate bool DayRunningEvent();

        public delegate bool DayStartingEvent();

        public delegate bool MonthPassedEvent();

        public delegate bool MonthRunningEvent();

        public delegate bool MonthStartingEvent();

        public delegate void SeasonChangeEvent(GameSeason season);

        public delegate bool YearPassedEvent();

        public delegate bool YearRunningEvent();

        public delegate bool YearStartingEvent();
    }
}

