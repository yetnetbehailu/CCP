using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCP.Data;
using CCP.Models.KennelModels;
using CCP.ViewModels;
using Amazon.S3;
using Amazon;
using Amazon.S3.Transfer;
using CCP.Models;
using Amazon.S3.Model;

namespace CCP.Controllers
{
    public class KennelsController : Controller
    {
        private readonly CCPContext _context;
        private readonly IConfiguration _configuration;

        public KennelsController(CCPContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Kennels
        public async Task<IActionResult> Index()
        {
            var cCPContext = _context.Kennel.Include(k => k.Country);
            return View(await cCPContext.ToListAsync());
        }

        // GET: Kennels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kennel == null)
            {
                return NotFound();
            }

            var kennel = await _context.Kennel
                .Include(k => k.Country)
                .Include(k => k.Logo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kennel == null)
            {
                return NotFound();
            }

            return View(kennel);
        }

        // GET: Kennels/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KennelLogoVM vm)
        {
            if(vm.Kennel != null)
            {
                Kennel newKennel = vm.Kennel;
                
                if(vm.Logo != null)
                {
                    string bucketName = "ccpgroupbucket";
                    var accessKey = _configuration["AwsAccessKey"];
                    var secretKey = _configuration["AwsSecretKey"];
                    using(var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.EUNorth1))
                    {
                        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Logo.FileName);
                        var fileTransfer = new TransferUtility(client);

                        using(var stream = vm.Logo.OpenReadStream())
                        {
                            await fileTransfer.UploadAsync(stream, bucketName, imageName);
                        }
                        newKennel.Logo = new ImagesMetaData
                        {
                            Name = imageName,
                            ImagePath = $"https://{bucketName}.s3.amazonaws.com/{imageName}",
                            UploadDate = DateTime.Now,
                            Kennel = newKennel
                        };
                    }
                }
                _context.Add(newKennel.Logo);
                _context.Add(newKennel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", vm.Kennel.CountryID);
            return View(vm);
        }

        // GET: Kennels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kennel == null)
            {
                return NotFound();
            }

            var kennel = await _context.Kennel.FindAsync(id);
            if (kennel == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", kennel.CountryID);
            return View(kennel);
        }

        // POST: Kennels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CountryID,Name,OwnerName,WebsiteURL,Address,Phone,Mobile,About")] Kennel kennel)
        {
            if (id != kennel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kennel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KennelExists(kennel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", kennel.CountryID);
            return View(kennel);
        }

        // GET: Kennels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kennel == null)
            {
                return NotFound();
            }

            var kennel = await _context.Kennel
                .Include(k => k.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kennel == null)
            {
                return NotFound();
            }

            return View(kennel);
        }

        // POST: Kennels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kennel == null)
            {
                return Problem("Entity set 'CCPContext.Kennel'  is null.");
            }
            var kennel = await _context.Kennel.FindAsync(id);
            if (kennel != null)
            {
                _context.Kennel.Remove(kennel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KennelExists(int id)
        {
          return (_context.Kennel?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GetLogo(string logoName)
        {
            string bucketName = "ccpgroupbucket";
            var accessKey = _configuration["AwsAccessKey"];
            var secretKey = _configuration["AwsSecretKey"];

            using var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.EUNorth1);
            // Specify the bucket name and object key (file path)
            string objectKey = logoName;

            // Create a request to get the object (image)
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey
            };

            // Get the object (image)
            GetObjectResponse response = await client.GetObjectAsync(request);
            byte[] imageBytes;
            var contentType = string.Empty;
            if (Path.GetExtension(objectKey) == ".jpg" || Path.GetExtension(objectKey) == ".jpeg")
            {
                contentType = "image/jpeg";
            }
            else if (Path.GetExtension(objectKey) == ".png")
            {
                contentType = "image/png";
            }

            // Read the image data from the response
            using (System.IO.Stream responseStream = response.ResponseStream)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await responseStream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }
            return File(imageBytes, contentType);
        }
    }
}
