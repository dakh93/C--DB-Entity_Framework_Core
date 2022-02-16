using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Initializer;


using (var db = new HospitalContext())
{
    DatabaseInitializer.SeedPatients(db, 50);
}
