using Microsoft.EntityFrameworkCore;
using SecendWebAppi.DataBaseContextModel;
using SecendWebAppi.Models;
using SecendWebAppi.ViewModels;

namespace SecendWebAppi.Repository
{
    public class TruckRepository
    {
        private readonly dbContextEF _dbContextEF;
        public TruckRepository(dbContextEF dbContextEF)
        {
            this._dbContextEF = dbContextEF;

        }

        //Get All Trucks
        public async Task<IEnumerable<Truck>> GetAllTruck()
        {
            var lst = await _dbContextEF.Trucks.ToListAsync();
            return lst;

        }

        //Get Truck
        public async Task<Truck> GetTruck(int id)
        {
            var Result = await _dbContextEF.Trucks.FirstOrDefaultAsync(f => f.ID == id);
            return Result;
        }

        //Post Truck   Add Truck
        public async Task<Truck> Add(TruckViewModel truckViewModel)
        {
            var truk = new Truck()
            {
                Title = truckViewModel.Title,
                Barcode = truckViewModel.Barcode,
                Capacity = truckViewModel.Capacity,
                PelakPart1 = truckViewModel.PelakPart1,
                PelakPart2 = truckViewModel.PelakPart2,
                PelakPart3 = truckViewModel.PelakPart3,
                PelakPart4 = truckViewModel.PelakPart4,
                SmartCode = truckViewModel.SmartCode,
                ShasiNumber = truckViewModel.ShasiNumber,
                IRCode = truckViewModel.IRCode,
                OwnerName = truckViewModel.OwnerName,
                OwnerMelliCode = truckViewModel.OwnerMelliCode,
                OwnerMobile = truckViewModel.OwnerMobile,
                BlackListDescr = truckViewModel.BlackListDescr,
                BlackListFlag = truckViewModel.BlackListFlag,

            };
            await _dbContextEF.Trucks.AddAsync(truk);
            await _dbContextEF.SaveChangesAsync();
            return truk;

        }

        //Update Truck
        public async Task<bool> Edit(Truck truck)
        {
            if (truck != null)
            {
                if (truck.ID != 0)
                {
                    var Result = await _dbContextEF.Trucks.FirstOrDefaultAsync(f => f.ID == truck.ID);

                    if (Result != null)
                    {
                        Result.Title = truck.Title;
                        Result.Barcode = truck.Barcode;
                        Result.Capacity = truck.Capacity;
                        Result.PelakPart1 = truck.PelakPart1;
                        Result.PelakPart2 = truck.PelakPart2;
                        Result.PelakPart3 = truck.PelakPart3;
                        Result.PelakPart4 = truck.PelakPart4;
                        Result.SmartCode = truck.SmartCode;
                        Result.ShasiNumber = truck.ShasiNumber;
                        Result.IRCode = truck.IRCode;
                        Result.OwnerName = truck.OwnerName;
                        Result.OwnerMelliCode = truck.OwnerMelliCode;
                        Result.OwnerMobile = truck.OwnerMobile;
                        Result.BlackListDescr = truck.BlackListDescr;
                        Result.BlackListFlag = truck.BlackListFlag;

                        await _dbContextEF.SaveChangesAsync();

                        return true;

                    }

                }

            }

            return false;

        }

        //Delete Truck
        public async Task<bool> Delete(int id)
        {
            var Result = await _dbContextEF.Trucks.FirstOrDefaultAsync(f => f.ID == id);

            if (Result != null)
            {
                 _dbContextEF.Trucks.Remove(Result);
                await _dbContextEF.SaveChangesAsync();
                return true;
            }
            return false;


        }



    }
}
