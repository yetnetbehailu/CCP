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
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using CCP.Models.DogModels;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index(string search = "", string country = "")
        {
            var query = _context.Kennel
        .Where(k => k.Name.Contains(search));

            if (!string.IsNullOrWhiteSpace(country) && country != "All Countries")
            {
                query = query.Where(k => k.Country.Name == country);
            }

            query = query.Include(k => k.Logo)
                         .Include(k => k.Country);

            ViewBag.Countries = _context.Kennel.Select(k => k.Country.Name).Distinct()
                   .OrderBy(name => name)
                   .Select(name => new SelectListItem
                   {
                       Value = name,
                       Text = name
                   }).ToList();

            return View(await query.ToListAsync());
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
            List<Dog> dogs = await _context.Dog.Where(d => d.KennelID == kennel.UserId).ToListAsync();
            
            ViewData["KennelDogs"] = dogs;
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KennelLogoVM vm)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == vm.Kennel.UserId);

                if (user == null || user.Id != vm.Kennel.UserId)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (vm.Kennel != null)
                {
                    Kennel newKennel = vm.Kennel;
                    Kennel kennelWithSameName = await _context.Kennel.FirstOrDefaultAsync(k => k.Name == newKennel.Name);

                    if (vm.Logo != null)
                    {
                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Logo.FileName);
                            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                            Directory.CreateDirectory(uploadsFolder); // Create the directory if it doesn't exist
                            var imagePath = Path.Combine(uploadsFolder, imageName);
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await vm.Logo.CopyToAsync(stream);
                            }
                            ImagesMetaData imagesMetaData = new ImagesMetaData { Name = imageName, ImagePath = imagePath, UploadDate = DateTime.Now, Kennel = vm.Kennel };
                        
                        _context.Add(imagesMetaData);
                    }
                    _context.Add(newKennel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException ex)
            {
                // Check if the exception is due to a unique constraint violation
                var innerException = ex.InnerException;
                if (innerException != null && innerException is SqlException sqlEx &&
                    (sqlEx.Number == 2601 || sqlEx.Number == 2627)) // These numbers correspond to unique constraint violations
                {
                    ModelState.AddModelError("Kennel.Name", "The kennel name is already taken.");
                }
                else
                {
                    // Handle other types of database exceptions or log the error
                    ModelState.AddModelError(string.Empty, "An error occurred while saving. Please try again.");
                }
            }
            
            
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "ID", vm.Kennel.CountryID);
            return View(vm);
        }

        // GET: Kennels/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _context.User.Include(u => u.Kennel).FirstOrDefaultAsync(u => u.Kennel.ID == id);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            if (user.Kennel == null)
            {
                return RedirectToAction(nameof(Index));
            }
           
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
            ViewData["CountryID"] = new SelectList(_context.Country.ToList(), "ID", "Name", kennel.CountryID);
            return View(vm);
        }

        [Authorize]
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
                    var user = await _context.User.Include(u => u.Kennel).FirstOrDefaultAsync(u => u.Kennel.ID == id);
                    //get kennel before changes for comparison
                    var originalKennelBeforeChanges = _context.Kennel.AsNoTracking().Include(k => k.Logo)
                        .FirstOrDefault(k => k.ID == id);
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
                    bool hasChanges = existingKennel.Name != originalKennelBeforeChanges.Name ||
                                       existingKennel.OwnerName != originalKennelBeforeChanges.OwnerName ||
                                          existingKennel.CountryID != originalKennelBeforeChanges.CountryID ||
                                          existingKennel.WebsiteURL != originalKennelBeforeChanges.WebsiteURL ||
                                          existingKennel.Address != originalKennelBeforeChanges.Address ||
                                          existingKennel.Phone != originalKennelBeforeChanges.Phone ||
                                          existingKennel.Mobile != originalKennelBeforeChanges.Mobile ||
                                          existingKennel.About != originalKennelBeforeChanges.About;
                    if (existingKennel.Logo != null && originalKennelBeforeChanges.Logo != null)
                    {
                        hasChanges = hasChanges ||
                                     existingKennel.Logo.Name != originalKennelBeforeChanges.Logo.Name ||
                                     existingKennel.Logo.ImagePath != originalKennelBeforeChanges.Logo.ImagePath;
                    }
                    else if ((existingKennel.Logo == null) != (originalKennelBeforeChanges.Logo == null))
                    {
                        // If one is null and the other isn't, then there's a change
                        hasChanges = true;
                    }

                    if (hasChanges)
                    {
                        var settings = new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                        };
                        ChangeLog changeLog = new ChangeLog
                        {
                            User = user,
                            ChangeTime = DateTime.Now,
                            ChangeType = "Edited kennel",
                            ModelName = existingKennel.Name,
                            OldValues = JsonConvert.SerializeObject(originalKennelBeforeChanges, settings),
                            NewValues = JsonConvert.SerializeObject(existingKennel, settings)
                        };
                        _context.ChangeLogs.Add(changeLog);
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _context.User.Include(u => u.Kennel).FirstOrDefaultAsync(u => u.Kennel.ID == id);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");

            }
            if (user.Kennel == null)
            {
                return RedirectToAction(nameof(Index));
            }
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

        // POST: Kennels/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kennel == null)
            {
                return Problem("Entity set 'CCPContext.Kennel'  is null.");
            }
            var kennel = await _context.Kennel.Include(k => k.Logo).FirstOrDefaultAsync(k => k.ID == id);
            if (kennel != null)
            {
                if(kennel.Logo != null)
                {
                    _context.ImagesMetaData.Remove(kennel.Logo);
                }
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
            if (string.IsNullOrEmpty(logoName))
            {
                return NotFound();
            }

            // Combine the uploads folder path with the image file name.
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var imagePath = Path.Combine(uploadsFolder, logoName);

            // Check if the file exists.
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var contentType = "image/jpeg"; 
            switch (Path.GetExtension(imagePath).ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
            }

            // Return the file.
            var fileBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(fileBytes, contentType, logoName);
        }
    }
}
