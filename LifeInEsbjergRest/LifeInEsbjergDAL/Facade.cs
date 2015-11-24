using LifeInEsbjergDAL.DomainModel;
using LifeInEsbjergDAL.Repository;
using LifeInEsbjergDAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInEsbjergDAL
{
    public class Facade
    {
        private IRepository<Company> companyRepo;
        private IRepository<Category> categoryRepo;
        private IRepository<Rating> ratingRepo;

        public IRepository<Company> GetCompanyRepository()
        {
            if (companyRepo == null)
            {
                companyRepo = new CompanyRepository();
            }
            return companyRepo;
        }
        public IRepository<Category> GetCategoryRepository()
        {
            if (categoryRepo == null)
            {
                categoryRepo = new CategoryRepository();
            }
            return categoryRepo;
        }
        public IRepository<Rating> GetRatingRepository()
        {
            if (ratingRepo == null)
            {
                ratingRepo = new RatingRepository();
            }
            return ratingRepo;
        }

    }
}
