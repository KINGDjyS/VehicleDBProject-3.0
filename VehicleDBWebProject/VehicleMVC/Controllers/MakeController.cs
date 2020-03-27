using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleMVC.Models;
using VehicleService;

namespace VehicleMVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleMakeService MakeService;
        private readonly IMapper Mapper;


        public MakeController(IVehicleMakeService makeService, IMapper mapper)
        {
            MakeService = makeService;
            Mapper = mapper;
        }

        public IActionResult Index([Bind("SearchName")] FilteringDTO filtering, SortingDTO sorting, PagingDTO paging)
        {
            ViewBag.CurrentPage = paging.PageNumber;
            ViewBag.OrderBy = sorting.OrderBy == "DESC" ? "ASC" : "DESC";
            ViewBag.CurrentOrder = sorting.OrderBy;
            ViewBag.CurrentSort = sorting.SortBy;
            paging.CountItems = MakeService.CountMakers();
            paging.PageSize = 3;
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
            var make = MakeService.GetVehicleMakes(filter, sort, page);
            if (make == null)
            {
                return NotFound(filtering.SearchName + " not Found");
            }
            return View(make.Select(Mapper.Map<VehicleMake, VehicleMakeDTO>));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleMakeDTO makeDTO)
        {
            if (ModelState.IsValid)
            {
                var vehicleMake = Mapper.Map<VehicleMake>(makeDTO);
                await MakeService.AddVehicleMakerAsync(vehicleMake);
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
            VehicleMakeDTO makeDTO = Mapper.Map<VehicleMakeDTO>(await MakeService.GetOneVehicleMakerAsync(id));
            return View(makeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleMakeDTO vehicleMakeDTO)
        {
            if (ModelState.IsValid)
            {
                VehicleMake make = Mapper.Map<VehicleMake>(vehicleMakeDTO);
                await MakeService.UpdateAsync(make);
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

            await MakeService.DeleteVehicleMakerAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
