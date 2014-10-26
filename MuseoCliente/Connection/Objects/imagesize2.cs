﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

 

namespace WindowsFormsApplication1
{

        
   public  class imagesize2 
    {

        private Image Redimensionar2(Image Imagen, int Ancho, int Alto, int resolucion)
        {
            //Bitmap sera donde trabajaremos los cambios
            using (Bitmap imagenBitmap = new Bitmap(Ancho, Alto, PixelFormat.Format32bppRgb))
            {
                imagenBitmap.SetResolution(resolucion, resolucion);
                //Hacemos los cambios a ImagenBitmap usando a ImagenGraphics y la Imagen Original(Imagen)
                //ImagenBitmap se comporta como un objeto de referenciado
                using (Graphics imagenGraphics = Graphics.FromImage(imagenBitmap))
                {
                    imagenGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    imagenGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    imagenGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    imagenGraphics.DrawImage(Imagen, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Imagen.Width, Imagen.Height), GraphicsUnit.Pixel);
                    //todos los cambios hechos en imagenBitmap lo llevaremos un Image(Imagen) con nuevos datos a travez de un MemoryStream
                    MemoryStream imagenMemoryStream = new MemoryStream();
                    imagenBitmap.Save(imagenMemoryStream, ImageFormat.Jpeg);
                    Imagen = Image.FromStream(imagenMemoryStream);
                }
            }

            return Imagen;
        }

        private Image Redimensionar(Image image, int SizeHorizontalPercent, int SizeVerticalPercent)
        {
                       //Obtenemos la resolucion original 
            int resolucion = Convert.ToInt32(image.HorizontalResolution);

            return this.Redimensionar2(image, SizeHorizontalPercent, SizeVerticalPercent, resolucion);
        }


       public string cambia(string dirIN, int newancho , int newalto , string nuevonombre  )
        {
           // esta es la funcion principal en la cual dirIN = direccion de la imagen original sin modificar
           // newancho = es el nuevo ancho de la imagen
           // newalto = es el nuevo alto de la imagen
           // nuevonombre = es el nuevo nombre de laimagen ya tratada por la funcion esta sera proporcionada por matul
           string dir = Application.StartupPath;
           int x = dir.Length;
           string direccion = dir.Substring(0,(x-9));
           string dir2 = "tempimagenes2";
           string dir3 = direccion + dir2;
           System.IO.Directory.CreateDirectory(dir3);
           

            Image image = null;
            image = Image.FromFile(dirIN);
            //Redimensionamos la imagen
            image = this.Redimensionar(image, newancho,newalto);
            image.Save(string.Format(@"{0}{1}", dir3+"/", nuevonombre));
            image.Dispose();
            string dirimagen;
            dirimagen = dir3 + "\\" + nuevonombre;
                       
            return dirimagen;

        }


    }
}
