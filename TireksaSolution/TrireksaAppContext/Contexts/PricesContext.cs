using ModelsShared;
using ModelsShared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
    public class PricesContext
    {
        public  Task<Prices> GetPricesByCustomer(Prices price)
        {
            if (price != null)
            {
                using (var db = new OcphDbContext())
                {
                    var result = db.Priceses.Where(O => O.ShiperId == price.ShiperId && O.ReciverId == price.ReciverId &&
                    O.From == price.From && O.To == price.To && O.PortType == price.PortType && O.PayType == price.PayType).FirstOrDefault();
                    if (result == null)
                    {
                        result = db.Priceses.Where(O => O.ShiperId == price.ShiperId && O.ReciverId == 0 &&
                        O.From == price.From && O.To == price.To).FirstOrDefault();
                        if (result == null)
                            return Task.FromResult(default(Prices));
                        else
                            return Task.FromResult( result);

                    }
                    return Task.FromResult(result);
                }
            }else
            {
                throw new SystemException("Pengirim atau Penerima tidak ditemukan");
            }
        }

        public Prices SetPrices(Prices price)
        {
            if (price != null)
            {
                using (var db = new OcphDbContext())
                {
                    try
                    {
                        price.Id = db.Priceses.InsertAndGetLastID(price);
                        if (price.Id>0)
                            return price;
                        else
                        {
                            throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (ex.Message == "Data Sudah Ada")
                            {
                               var updated= db.Priceses.Update(O => new { O.Price }, price, O => O.ShiperId == price.ShiperId && O.ReciverId == price.ReciverId &&
                                 O.From == price.From && O.To == price.To && O.PortType == price.PortType && O.PayType == price.PayType);
                                if (updated)
                                    return price;
                                throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
                            }
                        }
                        catch (Exception ex1)
                        {
                            throw new SystemException(ex1.Message);
                        }
                    }
                }
            }
            throw new SystemException("Pengirim atau Penerima tidak ditemukan");
        }


    }
}
