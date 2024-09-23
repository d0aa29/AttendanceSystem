using AttendanceSystem.Data;
using Repository.IRepository;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
       
        }

        public void Save()
        {
            _db.SaveChanges();
        }
       

   
    }
}
