using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZaginioneOsoby.Data;
using ZaginioneOsoby.Models;

namespace ZaginioneOsoby.Controllers
{
    public class OsobaZaginionaModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public OsobaZaginionaModelsController(ApplicationDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _webHost = web;
        }

        public async Task<IActionResult> Index(OsobaZaginionaModel.ProvicesList Nazwawojewodztwo, OsobaZaginionaModel.PlecList plec)
        {
            IEnumerable<OsobaZaginionaModel> ListOsob = await  _context.OsobyZaginione.ToListAsync();

            if (Nazwawojewodztwo != OsobaZaginionaModel.ProvicesList.Brak && plec != OsobaZaginionaModel.PlecList.Brak) // sprawdzanie zaznaczenia dwóch opcji
            {
                ListOsob = await _context.OsobyZaginione.Where(model => model.Provices == Nazwawojewodztwo && model.Sex == plec).ToListAsync();
                return View(ListOsob);
            }

            if (Nazwawojewodztwo != OsobaZaginionaModel.ProvicesList.Brak) // jedna opcja województwo
            {
                var wojewodztwa = await _context.OsobyZaginione.Where(model => model.Provices == Nazwawojewodztwo).ToListAsync();

                ListOsob = wojewodztwa;
                return View(ListOsob);
            }
            if (plec != OsobaZaginionaModel.PlecList.Brak) // jedna opcja plec
            {
                var sex = await _context.OsobyZaginione.Where(model => model.Sex == plec).ToListAsync();
                ListOsob = sex;
                return View(ListOsob);
            }

            return View(ListOsob);
        }
        [Authorize(Roles ="Admin,User")]
        public async Task<IActionResult> UserIndex(OsobaZaginionaModel.ProvicesList Nazwawojewodztwo, OsobaZaginionaModel.PlecList plec)
        {
            IEnumerable<OsobaZaginionaModel> ListOsob = await _context.OsobyZaginione.ToListAsync();

   
            var loggedUser = await _context.Users.FirstOrDefaultAsync(model => model.UserName == User.Identity.Name);
            if (Nazwawojewodztwo != OsobaZaginionaModel.ProvicesList.Brak && plec != OsobaZaginionaModel.PlecList.Brak) // sprawdzanie zaznaczenia dwóch opcji
            {
                ListOsob = await _context.OsobyZaginione.Where(model => model.Provices == Nazwawojewodztwo && model.Sex == plec).ToListAsync();              
                return View(ListOsob.Where(model => model.UserId == loggedUser.Id));
            }

            if (Nazwawojewodztwo != OsobaZaginionaModel.ProvicesList.Brak) // jedna opcja województwo
            {
                var wojewodztwa = await _context.OsobyZaginione.Where(model => model.Provices == Nazwawojewodztwo).ToListAsync();

                ListOsob = wojewodztwa;
                return View(ListOsob.Where(model => model.UserId == loggedUser.Id));
            }
            if (plec != OsobaZaginionaModel.PlecList.Brak) // jedna opcja plec
            {
                var sex = await _context.OsobyZaginione.Where(model => model.Sex == plec).ToListAsync();
                ListOsob = sex;
                return View(ListOsob.Where(model => model.UserId == loggedUser.Id));
            }

            return View(ListOsob.Where(model => model.UserId == loggedUser.Id));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osobaZaginionaModel = await _context.OsobyZaginione
                .FirstOrDefaultAsync(m => m.OsobaZaginionaId == id);
            if (osobaZaginionaModel == null)
            {
                return NotFound();
            }

            return View(osobaZaginionaModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: OsobaZaginionaModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("OsobaZaginionaId,Name,Surrname,Sex,MissingDate,Age,HairColor,Height,Descprition,PhotoUrl,City,Street,Provices")] OsobaZaginionaModel model)
        {

            var user = _context.Users.FirstOrDefault(model => model.UserName == User.Identity.Name);

            model.UserId = user.Id;


            model.FileName = Guid.NewGuid().ToString();
            model.FileName += "." + ".jpg";
            var saveimg = Path.Combine(_webHost.WebRootPath, "img",model.FileName);
            string imgtext = Path.GetExtension(model.PhotoUrl.FileName);
            if (imgtext == ".jpg" || imgtext == ".png")
            {
                using (var uploading = new FileStream(saveimg, FileMode.Create))
                {
                    await model.PhotoUrl.CopyToAsync(uploading);                 
                }
            }
            if (ModelState.IsValid)
            {

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
       
        [Authorize]
        // GET: OsobaZaginionaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osobaZaginionaModel = await _context.OsobyZaginione.FindAsync(id);
            if (osobaZaginionaModel == null)
            {
                return NotFound();
            }
            return View(osobaZaginionaModel);
        }

        // POST: OsobaZaginionaModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("OsobaZaginionaId,Name,Surrname,MissingDate,Age,HairColor,Height,Descprition,PhotoUrl")] OsobaZaginionaModel osobaZaginionaModel)
        {
            if (id != osobaZaginionaModel.OsobaZaginionaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osobaZaginionaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsobaZaginionaModelExists(osobaZaginionaModel.OsobaZaginionaId))
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
            return View(osobaZaginionaModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osobaZaginionaModel = await _context.OsobyZaginione
                .FirstOrDefaultAsync(m => m.OsobaZaginionaId == id);
            if (osobaZaginionaModel == null)
            {
                return NotFound();
            }

            return View(osobaZaginionaModel);
        }

        // POST: OsobaZaginionaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osobaZaginionaModel = await _context.OsobyZaginione.FindAsync(id);
            _context.OsobyZaginione.Remove(osobaZaginionaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsobaZaginionaModelExists(int id)
        {
            return _context.OsobyZaginione.Any(e => e.OsobaZaginionaId == id);
        }
    }
}
