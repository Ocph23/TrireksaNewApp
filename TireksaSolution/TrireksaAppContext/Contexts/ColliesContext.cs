using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
    public class ColliesContext
    {
        public  Task<colly> Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Collies.Where(O => O.Id == id).FirstOrDefault();
                if (result != null)
                    return Task.FromResult(result);
                else
                    throw new SystemException("Data Tidak Ditemukan");
            }
        }

        public Task<colly> Post(colly value)
        {
            using (var db = new OcphDbContext())
            {
                Tuple<bool, string> validateResult = ValidateColly(value);
                if(validateResult.Item1==true)
                {
                    value.Id = db.Collies.InsertAndGetLastID(value);
                    if (value.Id > 0)
                        return Task.FromResult(value);
                    else
                        throw new SystemException("Data Tidak Tersimpan");
                }else
                {
                    throw new SystemException(validateResult.Item2);
                }
            }
        }

        private Tuple<bool, string> ValidateColly(colly value)
        {
            string message ="";
            bool valid = true;
            if (value.PenjualanId <= 0)
            {
                message = "Id Penjualan < 0";
                valid = false;
            }

            if (value.Weight <= 0)
            {
                valid = false;
                message = "Berat < 0";
            }

            if (value.CollyNumber<= 0)
            {
                valid = false;
                message = "Colly Number 0";
            }


            if (value.TypeOfWeight== TypeOfWeight.None)
            {
                valid= false;
                message = "Type Of Weight Can Not None";
            }

            return Tuple.Create(valid, message);
        }

        public Task<colly> Put(colly value)
        {
            using (var db = new OcphDbContext())
            {
                Tuple<bool, string> validateResult = ValidateColly(value);
                if (validateResult.Item1 == true)
                {
                    value.Id = db.Collies.InsertAndGetLastID(value);
                    if (value.Id > 0)
                        return Task.FromResult(value);
                    else
                        throw new SystemException("Data Tidak Tersimpan");
                }
                else
                {
                    throw new SystemException(validateResult.Item2);
                }
            }
        }

        public Task<colly> Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Collies.Where(O => O.Id == id).FirstOrDefault();
                if (result != null && db.Collies.Delete(O => O.Id == id))
                {
                    return Task.FromResult(result);
                }
                else
                {
                    if (result == null)
                        throw new SystemException("Data Tidak Ditemukan");
                    else
                    {
                        throw new SystemException("Data Tidak terhapus");
                    }
                }
            }
        }
    }
}
