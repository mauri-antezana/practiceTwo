using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPB.BusinessLogic.Exceptions;
using UPB.BusinessLogic.Models;

namespace UPB.BusinessLogic.Managers
{
    public class PatientManager
    {
        private List<Patient> _patients = new List<Patient>();
        private readonly IConfiguration _configuration;

        public PatientManager(IConfiguration configuration)
        { 
            _configuration = configuration;
            _patients.Clear();
            Reader();
        }

        public List<Patient> GetAll()
        {
            _patients.Clear();
            Reader();
            return _patients;
        }

        public Patient GetPatientByCi(int ci)
        {
            try
            {
                Patient foundPatient = _patients.Find(p => p.Ci == ci);
                return foundPatient;
            }
            catch (Exception ex) 
            {
                PatientNotFoundException pnfEx = new PatientNotFoundException("GetPatientByCi");
                Log.Error(pnfEx.GetMessage());
                Log.Error("GetPatientByCi StackTrace: " + pnfEx.StackTrace);

                throw new Exception("Patient not found", ex);
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

            _patients.Add(createdPatient);
            Writer(createdPatient);
            return createdPatient;
        }

        public Patient UpdatePatient(int ci, Patient patientToUpdate)
        {
            Patient updatedPatient = new Patient()
            {
                Name = patientToUpdate.Name,
                LastName = patientToUpdate.LastName,
                BloodType = patientToUpdate.BloodType
            };

            try
            {
                int index = _patients.FindIndex(p => p.Ci == ci);
                _patients[index] = updatedPatient;
                Rewrite();
                return updatedPatient;
            }
            catch (Exception ex)
            {
                PatientNotFoundException pnfEx = new PatientNotFoundException("UpdatePatient");
                Log.Error(pnfEx.GetMessage());

                throw new Exception("Patient not found", ex);
            }   
        }

        public void DeletePatient(int ci)
        {
            try
            {
                int index = _patients.FindIndex(p => p.Ci == ci);
                _patients.RemoveAt(index);
                Rewrite();
            }
            catch (Exception ex)
            {
                PatientNotFoundException pnfEx = new PatientNotFoundException("DeletePatient");
                Log.Error(pnfEx.GetMessage());

                throw new Exception("Patient not found", ex);
            }
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
            reader.Close();
        }

        public void Writer(Patient patient)
        {
            StreamWriter writer = new StreamWriter(_configuration.GetSection("Logging").GetSection("FilePaths").GetSection("PatientPath").Value,true);

            writer.WriteLine($"\n{patient.Ci},{patient.Name},{patient.LastName},{patient.BloodType}");
            
            writer.Close();
        }

        public void Rewrite()
        {
            StreamWriter writer = new StreamWriter(_configuration.GetSection("Logging").GetSection("FilePaths").GetSection("PatientPath").Value);

            foreach (var patient in _patients)
            {
                writer.WriteLine($"{patient.Ci},{patient.Name},{patient.LastName},{patient.BloodType}");
            }

            writer.Close();
        }
    }
}
