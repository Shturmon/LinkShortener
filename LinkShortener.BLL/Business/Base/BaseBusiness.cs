using LinkShortener.DAL.Contracts;

namespace LinkShortener.BLL.Business.Base
{
    public class BaseBusiness
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseBusiness(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
