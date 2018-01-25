using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories.Configs
{
    public class MultimediaRepository
    {
        public static bool base64Upload(string folder,string _b64, string imgName)
        {
            try
            {
                var base64Split = _b64.Split(',');  //Recorto el string, antes de "," datos y despues el encode.

                var bytes = Convert.FromBase64String(base64Split[1]);
                string path = System.Web.HttpContext.Current.Server.MapPath(@"~/Uploads/"+folder+"/") + imgName;
                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}