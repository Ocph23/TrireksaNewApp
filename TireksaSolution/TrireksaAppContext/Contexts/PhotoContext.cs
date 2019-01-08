using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;

namespace TrireksaAppContext
{
   public class PhotoContext
    {
        public PhotoContext( string Path)
        {
            this.AppPath = Path;
        }

        public PhotoContext()
        {
        }

        public string AppPath { get; }

        public List<IPhoto> GetPhotoByPenjualanId(int Id)
        {
            using (var db = new OcphDbContext())
            {
                var data = from a in db.Photos.Where(O => O.PenjualanId == Id)
                           select new Photo { Ext = a.Ext, File = a.File, Id = a.Id, Path = a.Path, PenjualanId = a.PenjualanId };
                List<IPhoto> list = new List<IPhoto>();
                foreach(var item in data)
                {
                    list.Add(item);
                }
                return list;
            }
        }


        public Task<byte[]> GetPicture(IPhoto item)
        {
            try
            {
                var path = string.Format(@"{0}{1}.{2}", AppPath, item.File, item.Ext);
                using (var ms = new MemoryStream())
                {
                    var stream = new FileStream(path, FileMode.Open);
                    stream.CopyTo(ms);
                    byte[] data = ms.ToArray();
                    stream.Close();
                    return Task.FromResult(data);
                }
               
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
           
        }

        public IPhoto GetPhotoById(int id)
        {
            using (var db = new OcphDbContext())
            {
                var item = db.Photos.Where(O => O.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return new Photo { Ext = item.Ext, File = item.File, Id = item.Id, PenjualanId = item.PenjualanId, Path = item.Path };
                }
                else
                    throw new SystemException("Photo Tidak Ditemukan");
            }
        }

        public Task<byte[]> GetThumb(IPhoto item)
        {
            var path = string.Format(@"{0}{1}.{2}", AppPath, item.File, item.Ext);
            //@"D:\Trireksa\JustDesktop\Trireksa\TrireksaWebsite\TrireksaWebsite\PenjualanPhotoGalery\gambar.jpg";
            using (var ms = new MemoryStream())
            {
                var stream = new FileStream(path, FileMode.Open);
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();
                stream.Close();
                return Task.FromResult( Scale(data));
              
            }
        }

        public async Task<Photo> AddNewPhoto(Photo ph,string dir)
        {
           
            DateTime date = DateTime.Now;
            var random = new Random(DateTime.Now.Second);
          
            try
            {

                if (ph.PenjualanId == 0 && ph.STT > 0)
                {
                    using (var db = new OcphDbContext())
                    {
                        var penj = db.Penjualans.Where(O => O.STT == ph.STT).FirstOrDefault();
                        if (penj != null)
                            ph.PenjualanId = penj.Id;
                        else
                        {
                            throw new SystemException("Nomor SPB Tidak Ditemukan");
                        }
                    }
                }

                if(ph.PenjualanId>0)
                {
                    string fileName = string.Format("{0:D10}-{1}", ph.PenjualanId, random.Next(1000000, 9999999));
                    string fullFileName = string.Format("{0}{1}.{2}", AppPath, fileName, ph.Ext);
                    
                    using (FileStream file = new FileStream(fullFileName, FileMode.Create, System.IO.FileAccess.Write))
                    {
                        var ms = new MemoryStream(ph.Picture);
                        byte[] bytes = new byte[ms.Length];
                        ms.Read(bytes, 0, (int)ms.Length);
                        file.Write(bytes, 0, bytes.Length);
                        ms.Close();
                    }

                    ph.File = fileName;

                    using (var db = new OcphDbContext())
                    {
                        ph.Id = db.Photos.InsertAndGetLastID(new photo { Ext = ph.Ext, File = fileName, Path = dir, PenjualanId = ph.PenjualanId });
                        if (ph.Id > 0)
                        {
                            ph.Thumb = await GetThumb(ph);
                            return ph;
                        }
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }
                }else
                {
                    throw new SystemException("Tentukan Penjualan/SPB ");
                }


               
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }


        }

        public  Task<bool> DeletePhoto(IPhoto result)
        {
            string fullFileName = string.Format("{0}{1}.{2}", AppPath, result.File, result.Ext);
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    var isDeleted = db.Photos.Delete(O => O.Id == result.Id);
                    if(isDeleted)
                    {
                        File.Delete(fullFileName);
                        trans.Commit();
                        return Task.FromResult(isDeleted);
                    }else
                    {
                        throw new SystemException("Photo Tidak Terhapus");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
            }
           
        }

        public  byte[] Scale(byte[] byteArray)
        {
           
            try
            {
                int height = 70;
                int width = 70;
                //Grab the Original Image From SQL
                System.Drawing.Image imThumbnailImage;
              
                System.Drawing.Image OriginalImage;
                MemoryStream ms = new MemoryStream();

                // Stream / Write Image to Memory Stream from the Byte Array.
                ms.Write(byteArray, 0, byteArray.Length);

                OriginalImage = System.Drawing.Image.FromStream(ms);

                // Shrink the Original Image to a thumbnail size.
                imThumbnailImage = OriginalImage.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallBack), IntPtr.Zero);

                // Save Thumbnail to Memory Stream for Conversion to Byte Array.
                MemoryStream myMS = new MemoryStream();
                imThumbnailImage.Save(myMS, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] test_imge = myMS.ToArray();

                // Finally, Update Gallery's Thumbnail.
              
                imThumbnailImage.Dispose();
                OriginalImage.Dispose();
                return test_imge;
            }
            catch (Exception ex)
            {
                throw new SystemException("Resize Error.", ex);
            }
        }

        private bool ThumbnailCallBack()
        {
            return true;
        }
    }
}
