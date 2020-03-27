using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VehicleMVC.Models;
using VehicleService;

namespace VehicleMVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleModelService ModelService;
        private readonly IMapper Mapper;
        public ModelController(IVehicleModelService modelService, IMapper mapper)
        {
            ModelService = modelService;
            Mapper = mapper;
        }
        public IActionResult Index([Bind("SearchName")] FilteringDTO filtering, SortingDTO sorting, PagingDTO paging)
        {
            ViewBag.CurrentPage = paging.PageNumber;
            ViewBag.OrderBy = sorting.OrderBy == "DESC" ? "ASC" : "DESC";
            ViewBag.CurrentOrder = sorting.OrderBy;
            ViewBag.CurrentSort = sorting.SortBy;
            paging.CountItems = ModelService.CountModels();
            paging.PageSize = 5;
            if (paging.PageNumber < 1)
            {
                paging.PageNumber = 1;
            }
            if (paging.HasNextPage)
            {
                ViewBag.HasNext = true;
            }
            else
            {
                ViewBag.HasNext = false;
            }
            if (paging.HasPreviousPage)
            {
                ViewBag.HasPrevious = true;
            }
            else
            {
                ViewBag.HasPrevious = false;
            }
            var filter = Mapper.Map<Filtering>(filtering);
            var sort = Mapper.Map<Sorting>(sorting);
            var page = Mapper.Map<Paging>(paging);
            var model = ModelService.GetVehicleModels(filter, sort, page);
            if (model == null)
            {
                return NotFound(filtering.SearchName + " not Found");
            }
            return View(model.Select(Mapper.Map<VehicleModel, VehicleModelDTO>));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleModelDTO vehicleModelDTO)
        {
            if (ModelState.IsValid)
            {
                var vehicleModel = Mapper.Map<VehicleModel>(vehicleModelDTO);
                await ModelService.AddVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            VehicleModelDTO modelDTO = Mapper.Map<VehicleModelDTO>(await ModelService.GetOneVehicleModelAsync(id));
            return View(modelDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleModelDTO vehicleModelDTO)
        {
            if (ModelState.IsValid)
            {
                VehicleModel model = Mapper.Map<VehicleModel>(vehicleModelDTO);
                await ModelService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            await ModelService.DeleteVehicleModelAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}