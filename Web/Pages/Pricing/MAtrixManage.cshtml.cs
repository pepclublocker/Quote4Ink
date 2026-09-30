using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuotedInk.Data.Models;
using QuotedInk.Data.Repository;

namespace Web.Pages
{
    [Authorize]
    public class _WPMatrixManageModel : PageClass
    {

        public _WPMatrixManageModel(UserManager<ApplicationUser> myUser, ILogger<_WPMatrixManageModel> logger, IRepository repository) : base(myUser, logger, repository)
        {

        }

        // [FromQuery(Name = "MatrixID")]
        [BindProperty(SupportsGet = true)]
        public Guid MatrixID { get; set; } = Guid.Empty;



        [BindProperty]
        public PriceMatrix PriceMatrix { get; set; } = new PriceMatrix()!;

        [BindProperty]
        public PriceMatrixProperties PriceMatrixProperties { get; set; } = new PriceMatrixProperties();

        public IEnumerable<PriceMatrixProperties> MatrixProps { get; set; } = Enumerable.Empty<PriceMatrixProperties>();

        public IEnumerable<PriceMatrixPrice> MatrixPrices { get; set; } = Enumerable.Empty<PriceMatrixPrice>();

        public void OnGet()
        {



            if (MatrixID == Guid.Empty)
            {
                Response.Redirect("/Matrix");

            }
            else
            {
                //ViewData["SalesGroupID"] = new SelectList(_repRepository.SalesGroups, "Id", "SalesGroupName");
                PriceMatrix = _repRepository.GetMatrix(MatrixID);
                //  MatrixProps = _repRepository.GetMatrixProperties(MatrixID);
                MatrixPrices = _repRepository.GetPriceMatrixPrices(MatrixID);

            }
            // if (id == null || _repRepository.PriceMatrixs == null)
            // {
            //     return NotFound();
            // }

            // var pricematrix =  await _repRepository.PriceMatrixs.FirstOrDefaultAsync(m => m.Id == id);
            // if (pricematrix == null)
            // {
            //     return NotFound();
            // }
            // PriceMatrix = pricematrix;
            //ViewData["SalesGroupID"] = new SelectList(_repRepository.SalesGroups, "Id", "SalesGroupName");


            //  PriceMatrixProperties = _repRepository.GetProperty(3);





            //return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public ActionResult OnGetGetLoading()
        {
            return Partial("__Loading");
        }


        public ActionResult OnPostDeleteProperty(Guid myPropertyID, Guid myMatrixID)
        {
            _repRepository.DeleteProperty(myPropertyID);

            //    MatrixProps = _repRepository.GetMatrixProperties(myMatrixID);
            return Partial("_MatrixProperties", MatrixProps);
        }

        public ActionResult OnPostSaveProperty()
        {
            if (PriceMatrixProperties.Id == Guid.Empty)
            {
                _repRepository.SaveProperty(PriceMatrixProperties);
            }
            else
            {
                _repRepository.UpdateProperty(PriceMatrixProperties);
            }


            //   MatrixProps = _repRepository.GetMatrixProperties(PriceMatrixProperties.PriceMatrixID);
            return Partial("_MatrixProperties", MatrixProps);
        }
        public void OnPostMatrixGroup()
        {
            //if (!ModelState.IsValid)
            //{
            // return Page();
            //}

            //_context.Attach(PriceMatrix).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PriceMatrixExists(PriceMatrix.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("../Matrix");
        }

        private bool PriceMatrixExists(int id)
        {
            return true;
            // return _context.PriceMatrixs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> OnPostUpdateCount(Guid MatrixID, int Count, int NewCount)
        {
            await _repRepository.MaxLevelCellChange(MatrixID, NewCount, Count);

            MatrixPrices = _repRepository.GetPriceMatrixPrices(MatrixID);

            return Partial("_Matrix", MatrixPrices);
        }

        // [ProducesResponseType(StatusCodes.Status200OK)]
        public void OnPostUpdatePrice(int Item, decimal Price, int Matrix)
        {
            _repRepository.PriceCellChange(Item, Price);

            //  MatrixPrices = _repRepository.GetPriceMatrixPrices(Matrix);

            //return  "Good";// Partial("_Matrix", MatrixPrices);
        }

        public ActionResult OnPostAddColumn(Guid MatrixID)
        {
            _repRepository.CreateMatrixColumn(MatrixID);

            MatrixPrices = _repRepository.GetPriceMatrixPrices(MatrixID);

            return Partial("_Matrix", MatrixPrices);
        }

        public async Task<IActionResult> OnPostRemoveColumn(Guid MatrixID)
        {
            await _repRepository.RemoveMatrixColumn(MatrixID);

            MatrixPrices = _repRepository.GetPriceMatrixPrices(MatrixID);

            return Partial("_Matrix", MatrixPrices);
        }

        public ActionResult OnPostAddRow(Guid MatrixID)
        {
            _repRepository.CreateMatrixRow(MatrixID);

            MatrixPrices = _repRepository.GetPriceMatrixPrices(MatrixID);

            return Partial("_Matrix", MatrixPrices);
        }

        public async Task<IActionResult> OnPostRemoveRow(Guid MatrixID)
        {
            await _repRepository.RemoveMatrixRow(MatrixID);

            MatrixPrices = _repRepository.GetPriceMatrixPrices(MatrixID);

            return Partial("_Matrix", MatrixPrices);
        }

        public ActionResult OnGetGetProperty(Guid myPropID, Guid myMatrixID)
        {
            if (myPropID == Guid.Empty)
            {
                PriceMatrixProperties = new PriceMatrixProperties();
                //    PriceMatrixProperties.PriceMatrixID = myMatrixID;

            }
            else
            {
                PriceMatrixProperties = _repRepository.GetProperty(myPropID);

            }
            return Partial("_ManageProperty", PriceMatrixProperties);
        }
    }
}
