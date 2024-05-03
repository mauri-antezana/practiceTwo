using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPB.BusinessLogic.Models;

namespace UPB.BusinessLogic.Managers
{
    public class PacientManager
    {
        private List<Pacient> _pacients;
        private readonly IConfiguration _configuration;

        public PacientManager(IConfiguration configuration)
        {
            _configuration = configuration;

            _pacients = new List<Pacient>();
            _pacients.Add(new Pacient{Ci = 7871719,Name = "Mauricio",LastName = "Antezana",BloodType = "O+"});
            _pacients.Add(new Pacient {Ci = 7672712, Name = "Juan", LastName = "Perez", BloodType = "A+" });
            _pacients.Add(new Pacient { Ci = 3423453, Name = "Maria", LastName = "Lopez", BloodType = "B+" });
        }

        public List<Pacient> GetAll()
        {
            return _pacients;
        }

        public Pacient GetPacientByCi(int ci)
        {
            throw new NotImplementedException();
        }

        public Pacient CreatePacient(Pacient pacient)
        {
            Random random = new Random();
            int r = random.Next(1, 4);
            string bloodType = "";

            switch (r)
            {
                case 1:
                    bloodType = "A+";
                    break;
                case 2:
                    bloodType = "B+";
                    break;
                case 3:
                    bloodType = "O+";
                    break;
                case 4:
                    bloodType = "AB+";
                    break;
            }

            Pacient createdPacient = new Pacient()
            {

                Ci = pacient.Ci,
                Name = pacient.Name,
                LastName = pacient.LastName,
                BloodType = bloodType
            };

            _pacients.Add(createdPacient);
            return createdPacient;
        }

        public Pacient UpdatePacient(int ci, Pacient pacient)
        {
            throw new NotImplementedException();
        }

        public Pacient DeletePacient(int ci)
        {
            throw new NotImplementedException();
        }
    }
}
