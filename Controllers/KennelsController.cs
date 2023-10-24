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
using Microsoft.AspNetCore.Identity;
using CCP.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;

namespace CCP.Controllers
{
    public class KennelsController : Controller
    {
        private readonly CCPContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<CCPUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KennelsController(CCPContext context, IConfiguration configuration, UserManager<CCPUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kennel == null)
            {
                return NotFound();
            }

            return View(kennel);
        }

        // GET: Kennels/Create
        public async Task<IActionResult>  Create()
        {
            //Get user
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                //Get user kennel
                var userKennel = await _context.Kennel.FirstOrDefaultAsync(k => k.UserId == user.Id);
                if (userKennel != null)
                {
                    return RedirectToAction(nameof(Details), new { id = user.Kennel.ID });
                }
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KennelLogoVM vm)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == vm.Kennel.UserId);
            if(user == null || user.Id != vm.Kennel.UserId)
            {
                return RedirectToAction("Login", "Account");
            }
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
                    _context.Add(newKennel.Logo);
                }
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

            var kennel = await _context.Kennel.Include(k => k.Logo).FirstOrDefaultAsync(k => k.ID == id);
            if (kennel == null)
            {
                return NotFound();
            }
            KennelLogoVM vm = new KennelLogoVM
            {
                Kennel = kennel
            };
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name", kennel.CountryID);
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KennelLogoVM vm)
        {
            if (id != vm.Kennel.ID)
            {
                return NotFound();
            }
            
            if(vm.Kennel != null)
            {
                try
                {
                    // Handle kennel details update
                    var existingKennel = await _context.Kennel.Include(k => k.Logo).FirstOrDefaultAsync(k => k.ID == id);

                    if (existingKennel == null)
                    {
                        return NotFound();
                    }

                    existingKennel.Name = vm.Kennel.Name;
                    existingKennel.OwnerName = vm.Kennel.OwnerName;
                    existingKennel.CountryID = vm.Kennel.CountryID;
                    existingKennel.WebsiteURL = vm.Kennel.WebsiteURL;
                    existingKennel.Address = vm.Kennel.Address;
                    existingKennel.Phone = vm.Kennel.Phone;
                    existingKennel.Mobile = vm.Kennel.Mobile;
                    existingKennel.About = vm.Kennel.About;

                    if (vm.Logo != null && vm.Logo.Length > 0)
                    {
                        string bucketName = "ccpgroupbucket";
                        var accessKey = _configuration["AwsAccessKey"];
                        var secretKey = _configuration["AwsSecretKey"];
                        using (var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.EUNorth1))
                        {
                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Logo.FileName);
                            var fileTransfer = new TransferUtility(client);

                            using (var stream = vm.Logo.OpenReadStream())
                            {
                                await fileTransfer.UploadAsync(stream, bucketName, imageName);
                            }
                            // Update the logo information in the existing kennel
                            if (existingKennel.Logo != null)
                            {
                                if (existingKennel.Logo.Name != imageName || existingKennel.Logo.ImagePath != $"https://{bucketName}.s3.amazonaws.com/{imageName}")
                                {
                                    _context.ImagesMetaData.Remove(existingKennel.Logo);
                                    existingKennel.Logo = new ImagesMetaData
                                    {
                                        Name = imageName,
                                        ImagePath = $"https://{bucketName}.s3.amazonaws.com/{imageName}",
                                        UploadDate = DateTime.Now
                                    };
                                }
                            }
                            else
                            {
                                existingKennel.Logo = new ImagesMetaData
                                {
                                    Name = imageName,
                                    ImagePath = $"https://{bucketName}.s3.amazonaws.com/{imageName}",
                                    UploadDate = DateTime.Now
                                };
                            }
                            _context.ImagesMetaData.Update(existingKennel.Logo);
                        }
                    }
                    _context.Update(existingKennel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KennelExists(vm.Kennel.ID))
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
                
            
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name", vm.Kennel.CountryID);
            return View(vm);
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
