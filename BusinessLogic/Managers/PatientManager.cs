using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPB.BusinessLogic.Models;

namespace UPB.BusinessLogic.Managers
{
    public class PatientManager
    {
        private List<Patient> _patients;
        private readonly IConfiguration _configuration;

        public PatientManager(IConfiguration configuration)
        {
            _configuration = configuration;
            Reader();
        }

        public List<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetPacientByCi(int ci)
        {
            try
            {
                Patient foundPatient = _patients.Find(p => p.Ci == ci);
                return foundPatient;
            }
            catch
            {
                throw new Exception("Pacient not found");
            }
        }

        public Patient CreatePatient(Patient patient)
        {

            Patient createdPatient = new Patient()
            {
                Ci = patient.Ci,
                Name = patient.Name,
                LastName = patient.LastName,
                BloodType = RandomBloodType(),
            };

            Writer(createdPatient);
            _patients.Add(createdPatient);
            return createdPatient;
        }

        public Patient UpdatePatient(int ci, Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient DeletePatient(int ci)
        {
            throw new NotImplementedException();
        }

        public string RandomBloodType()
        {
            Random random = new Random();
            int r = random.Next(1, 5);
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

        public void Reader()
        {
            StreamReader reader = new StreamReader(_configuration.GetSection("Logging").GetSection("FilePaths").GetSection("PatientPath").Value);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                Patient patient = new Patient
                {
                    Ci = int.Parse(values[0]),
                    Name = values[1],
                    LastName = values[2],
                    BloodType = values[3]
                };

                _patients.Add(patient);
            }
        }

        public void Writer(Patient patient)
        {
            StreamWriter writer = new StreamWriter(_configuration.GetSection("Logging").GetSection("FilePaths").GetSection("PatientPath").Value);

            writer.WriteLine($"{patient.Ci},{patient.Name},{patient.LastName},{patient.BloodType}");
            
            writer.Close();
        }
    }
}
