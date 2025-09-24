using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using TopSpeed.Application1.ApplicationConstants;
using TopSpeed.Application1.ApplicationConstants.Contracts.Presistance;
using TopSpeed.Domain1.ApplicationEnums;
using TopSpeed.Domain1.Models;
using TopSpeed.Infrastructure1.Comman;




namespace TopSpeed.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public BrandController (IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        { 
            _unitOfWork= unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }


        [HttpGet]


        public async Task<ActionResult> Index()
        {
            List<Brand>brands = await _unitOfWork.Brand.GetAllAsync();

            return View(brands);
    
       }

        [HttpGet]

            public IActionResult Create()
        {
           

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            string webRootPath =_webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            if(file.Count > 0)
            {
                string  newfileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand");

                var extension = Path.GetExtension(file[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, newfileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                 
                brand.BrandLogo = @"\images\brand\" + newfileName + extension;
                        
                        
            }

            if(ModelState.IsValid)
            {
                await _unitOfWork.Brand.Create(brand);
                await _unitOfWork.SaveAsync();


                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));

            }
            return View();

        }
        [HttpGet]
       
        public async Task<IActionResult> Details(Guid id)
        {
            Brand brand = await _unitOfWork.Brand.GetByIdAsync(id);
            
            return View(brand);


        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Brand brand = await _unitOfWork.Brand.GetByIdAsync(id);

            return View(brand);


        }

        [HttpPost]

        public async Task <IActionResult> Edit(Brand brand) 
        {

            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newfileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand");

                var extension = Path.GetExtension(file[0].FileName);

                //delete old image

                var objfromDb = await _unitOfWork.Brand.GetByIdAsync(brand.Id);

                if (objfromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objfromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                }

                using (var fileStream = new FileStream(Path.Combine(upload, newfileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newfileName + extension;


            }

            if (ModelState.IsValid)

            {
                await _unitOfWork.Brand.Update(brand);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction(nameof(Index));

            }

            return View();

        }

        [HttpGet]
        public async Task <IActionResult> Delete(Guid id)
        {
            Brand brand =await _unitOfWork.Brand.GetByIdAsync(id);

            return View(brand);


        }

        [HttpPost]

        public async Task <IActionResult> Delete(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

           if(!string.IsNullOrEmpty(brand.BrandLogo))
            {
                //delete old image

                var objfromDb = await _unitOfWork.Brand.GetByIdAsync(brand.Id);

                if (objfromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objfromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                }

            }
                    await _unitOfWork.Brand.Delete(brand);
                     await _unitOfWork.SaveAsync(); 

                        

            TempData["Delete"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }


    }


}
