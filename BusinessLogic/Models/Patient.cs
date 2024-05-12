﻿namespace UPB.BusinessLogic.Models
{
    public class Patient
    {
        private int _ci;
        public int Ci
        {
            get { return _ci; }
            set { _ci = value; }
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BloodType { get; set; }
        public string Code { get; set; }
    }
}
