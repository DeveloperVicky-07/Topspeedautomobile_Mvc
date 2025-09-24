using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using TopSpeed.Application1.ApplicationConstants;
using TopSpeed.Application1.ApplicationConstants.Contracts.Presistance;
using TopSpeed.Domain1.Models;
using TopSpeed.Infrastructure1.Comman;




namespace TopSpeed.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public VehicleTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        { 
            _unitOfWork= unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }


        [HttpGet]


        public async Task<ActionResult> Index()
        {
            List<VehicleType>vehicleTypes = await _unitOfWork.VehicleType.GetAllAsync();

            return View(vehicleTypes);
    
       }

        [HttpGet]

            public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            
            if(ModelState.IsValid)
            {
                await _unitOfWork.VehicleType.Create(vehicleType);
                await _unitOfWork.SaveAsync();


                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));

            }
            return View();

        }
        [HttpGet]
       
        public async Task<IActionResult> Details(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);
            
            return View(vehicleType);


        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);


        }

        [HttpPost]

        public async Task <IActionResult> Edit(VehicleType vehicleType) 
        {

            
            if (ModelState.IsValid)

            {
                await _unitOfWork.VehicleType.Update(vehicleType);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction(nameof(Index));

            }

            return View();

        }

        [HttpGet]
        public async Task <IActionResult> Delete(Guid id)
        {
            VehicleType vehicleType =await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);


        }

        [HttpPost]

        public async Task <IActionResult> Delete(VehicleType vehicleType)
        {
                    await _unitOfWork.VehicleType.Delete(vehicleType);
                     await _unitOfWork.SaveAsync(); 

                        

            TempData["Delete"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }


    }


}
