using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CorneTest.Models;
using ForAgents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.JSInterop;

namespace CorneTest.Controllers
{
    public class AdministrativeDetailsController : Controller
    {
        private readonly DatabaseContext _context;
        string listingCategory;
        public AdministrativeDetailsController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index(string category)
        {
            listingCategory = category;
            TempData["category"] = listingCategory;
            TempData.Keep("category");
            return View();
        }
        [HttpPost]
        public IActionResult Sale(IFormFile fileIDDocument, IFormFile fileProofOfResidence, IFormFile fileSARSDocument, IFormFile fileBankDocument)
        {
            //var listingID = GetListingID();
            TempData.Keep("listingId");
            TempData.Keep("category");

            if (fileIDDocument != null)
            {
                UploadFiles(fileIDDocument);
            }
            if (fileProofOfResidence != null)
            {
                UploadFiles(fileProofOfResidence);
            }
            if (fileSARSDocument != null)
            {
                UploadFiles(fileSARSDocument);
            }
            if (fileBankDocument != null)
            {
                UploadFiles(fileBankDocument);
            }
            if (TempData["category"].ToString() == "Sales")
            {
                return View("Price");
            }
            else if (TempData["category"].ToString() == "Rental")
            {
                return View("PriceRental");
            }
            return View("Price");
        }

        public int InsertListing(ListingInitiate listing)
        {
            Databases databases = new Databases();
            var i = databases.InsertListing(listing);

            return i;
        }

        public ActionResult PropertyDetails()
        {
            TempData.Keep("category");

            return View();
        }

        public ActionResult PropertyDetailsRental()
        {
            TempData.Keep("category");

            return View();
        }

        public ActionResult Seller()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Owner()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Price()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult PriceRental()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult BuildingInformation()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }
        public ActionResult StaffTenant()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult StaffTenantRental()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Features()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Kitchen()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Entertainment()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Security()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Garages()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Walls()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Windows()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Roof()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Ceiling()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Garden()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Pool()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Boundry()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Defects()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult ExtraAccommodation()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult LivingAreas()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Bedrooms()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Bathrooms()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult ExcludedIncluded()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult Summary()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public ActionResult History()
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            return View();
        }

        public int GetLastListingID()
        {
            Databases databases = new Databases();
            var listingId = databases.GetLastListingID();
            TempData["listingId"] = listingId;
            TempData.Keep();
            return listingId;
        }
        public void InsertListingType(TypeDto propertyType)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertListingType(propertyType);
        }
        public void InsertSellerDetails(Seller seller)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertSellerDetails(seller);
        }

        public void InsertOwnerDetails(Owner owner)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertOwnerDetails(owner);
        }

        public void InsertPrice(Price price)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertPrice(price);
        }

        public void InsertPriceRental(Price priceRental)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertPrice(priceRental);
        }

        public void InsertBuildingInformation(Building building)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertBuildingInformation(building);
        }

        public void InsertStaffTenantInformation(StaffTenant staffTenant)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertStaffTenant(staffTenant);
        }

        public void InsertFeatures(Features features)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertFeatures(features);
        }

        public void InsertKitchenInformation(Kitchen kitchen)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertKitchenInformation(kitchen);
        }

        public void InsertEntertainmentInformation(Entertainment entertainment)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertEntertainmentInformation(entertainment);
        }

        public void InsertSecurityInformation(Security security)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            databases.InsertSecurityInformation(security);
        }

        public void InsertGarageInformation(Garage garage)
        {
            Databases databases = new Databases();
            databases.InsertGarageInformation(garage);
        }

        public void InsertWallInformation(Wall wall)
        {
            Databases databases = new Databases();
            databases.InsertWallInformation(wall);
        }

        public void InsertWindowInformation(Window window)
        {
            Databases databases = new Databases();
            databases.InsertWindowInformation(window);
        }

        public void InsertRoofInformation(Roof roof)
        {
            Databases databases = new Databases();
            databases.InsertRoofInformation(roof);
        }

        public void InsertCeilingInformation(Ceiling ceiling)
        {
            Databases databases = new Databases();
            databases.InsertCeilingInformation(ceiling);
        }

        public void InsertGardenInformation(Garden garden)
        {
            Databases databases = new Databases();
            databases.InsertGardenInformation(garden);
        }

        public void InsertPoolInformation(Pool pool)
        {
            Databases databases = new Databases();
            databases.InsertPoolInformation(pool);
        }

        public void InsertBoundryInformation(Boundry boundry)
        {
            Databases databases = new Databases();
            databases.InsertBoundryInformation(boundry);
        }

        public void InsertDefectsInformation(Defects defects)
        {
            Databases databases = new Databases();
            databases.InsertDefectsInformation(defects);
        }

        public void InsertExtraAccommodationInformation(ExtraAccommodation extraAccommodation)
        {
            Databases databases = new Databases();
            databases.InsertExtraAccommodationInformation(extraAccommodation);
        }

        public void InsertLivingAreasInformation(LivingArea livingAreas)
        {
            Databases databases = new Databases();
            databases.InsertLivingAreasInformation(livingAreas);
        }

        public void InsertBedroomsInformation(Bedrooms bedrooms)
        {
            Databases databases = new Databases();
            databases.InsertBedroomsInformation(bedrooms);
        }

        public void InsertBathroomsInformation(Bathrooms bathrooms)
        {
            Databases databases = new Databases();
            databases.InsertBathroomsInformation(bathrooms);
        }

        public void InsertExclusionsInclusions(ExclusionsInclusions exclusionsInclusions)
        {
            Databases databases = new Databases();
            databases.InsertExclusionsInclusions(exclusionsInclusions);
        }

        public void InsertSummary(Summary summary)
        {
            Databases databases = new Databases();
            databases.InsertSummary(summary);
        }

        public void InsertHistory(History history)
        {
            Databases databases = new Databases();
            databases.InsertHistory(history);
        }

        public void UploadFiles(IFormFile files)
        {
            TempData.Keep("listingId");
            TempData.Keep("category");
            Databases databases = new Databases();
            int listingID = databases.GetListingID();

            if (files.Length > 0)
            {
                //Getting FileName
                var fileName = Path.GetFileName(files.FileName);
                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                var objfiles = new Files()
                {
                    ListingId = listingID,
                    DocumentId = 0,
                    Name = newFileName,
                    FileType = fileExtension,
                    CreatedOn = DateTime.Now
                };

                using (var target = new MemoryStream())
                {
                    files.CopyTo(target);
                    objfiles.DataFiles = target.ToArray();
                }

                _context.Files.Add(objfiles);
                _context.SaveChanges();

            }
        }
    }
}
