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
            try
            {
                Pacient foundPacient = _pacients.Find(p => p.Ci == ci);
                return foundPacient;
            }
            catch
            {
                throw new Exception("Pacient not found");
            }
        }

        public Pacient CreatePacient(Pacient pacient)
        {
            Pacient createdPacient = new Pacient()
            {

                Ci = pacient.Ci,
                Name = pacient.Name,
                LastName = pacient.LastName,
                BloodType = this.RandomBloodType()
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

        public string RandomBloodType()
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
            return bloodType;
        }
        // Para el reader/writer
        // _configuration.GetSection("FilePaths").GetSection("PatientFilePath").Value

        // Split string by ','
        // string name = "Mauricio,Antezana,121212,O+"
        // string[] values = name.Split(',');
        public void Reader()
        {
            StreamReader reader = new StreamReader(_configuration.GetSection("FilePaths").GetSection("PatientFilePath").Value);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                Pacient pacient = new Pacient
                {
                    Ci = int.Parse(values[0]),
                    Name = values[1],
                    LastName = values[2],
                    BloodType = values[3]
                };

                _pacients.Add(pacient);
            }
        }

        public void Writer()
        {
            StreamWriter writer = new StreamWriter(_configuration.GetSection("FilePaths").GetSection("PatientFilePath").Value);

            foreach (Pacient pacient in _pacients)
            {
                writer.WriteLine($"{pacient.Ci},{pacient.Name},{pacient.LastName},{pacient.BloodType}");
            }

            writer.Close();
        }
    }
}
