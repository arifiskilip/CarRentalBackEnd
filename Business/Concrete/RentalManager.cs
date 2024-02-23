using Business.Abstract;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly IUow _uow;

        public RentalManager(IRentalDal rentalDal, IUow uow)
        {
            _rentalDal = rentalDal;
            _uow = uow;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public async Task<IDataResult<Rental>> AddAsync(Rental rental)
        {
            var rentalCar = await _rentalDal.AddAsync(rental);
            await _uow.SaveAsync();
            return new SuccessDataResult<Rental>(rentalCar, "Kiralama işlemi başarılı!");
        }
    }
}
