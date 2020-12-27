using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Take screenshot then save to gallery
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        string SaveToGallery(View view);

        /// <summary>
        /// Check permission 
        /// </summary>
        /// <returns></returns>
        Task CheckPermission();

    }
}
