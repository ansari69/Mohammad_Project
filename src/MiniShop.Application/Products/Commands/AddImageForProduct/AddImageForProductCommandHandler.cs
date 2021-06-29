using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Commands.AddImageForProduct
{
    public class AddImageForProductCommandHandler
        : IRequestHandler<AddImageForProductCommand, bool>
    {
        private readonly IAppDbContext _context;

        public AddImageForProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddImageForProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _context.Products
               .SingleOrDefaultAsync(e => e.ProductId == request.ProductId);

            if (product == null || !product.IsConfirm)
                throw new EntryValidationException();


            #region Upload File


            var filePaths = new List<string>();
    
                if (request.Files.Length > 0)
                {

                    string mimeType = "application/unknown";
                    string ext = System.IO.Path.GetExtension(request.Files.FileName).ToLower();
                    Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                    if (regKey != null && regKey.GetValue("Content Type") != null)
                        mimeType = regKey.GetValue("Content Type").ToString();


                string typeFile = null;

                int idx = request.Files.ContentType.LastIndexOf('/');

                if (idx != -1)
                    typeFile = request.Files.ContentType.Substring(idx + 1);
    

                var fileName = Guid.NewGuid().ToString();

                    var filePath = Path.Combine("ImagesFolder", fileName + "." + typeFile);
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Files.CopyToAsync(stream);
                    }

                    product.ImageName = fileName + "." + typeFile;

                   await _context.SaveChangesAsync();

            }


            #endregion


            return true;
        }
    }
}
