using halak.DTOs;
using halak.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace halak.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HalakController : ControllerBase
    {
        [HttpGet("ByHorgasz")]
        public IActionResult GetByHorgasz(int id)
        {
            using (var cx=new HalakContext())
            {
                try
                {
                    if (cx.Horgaszoks.Select(x => x.Id).Contains(id))
                    {
                        var result = cx.Fogasoks.Include(f => f.Horgasz).Include(f => f.Hal).Where(f=>f.HorgaszId==id).Select(x=>x.Hal).ToList();
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpGet("ByHorgaszNev")]
        public IActionResult GetByHorgaszNev(string nev)
        {
            using (var cx = new HalakContext())
            {
                try
                {
                    if (cx.Horgaszoks.Select(x => x.Nev).Contains(nev))
                    {
                        var result = cx.Fogasoks.Include(f => f.Horgasz).Include(f => f.Hal).Where(f => f.Horgasz.Nev == nev).Select(x => x.Hal).ToList();
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpGet("MeretFolott")]
        public IActionResult Gethalakdto(decimal hatar)
        {
            using (var cx = new HalakContext())
            {
                try
                {
                    var result = cx.Halaks.Include(f => f.To).Where(f => f.MeretCm >= hatar).Select(h => new MeretToDTO()
                    {
                        Faj = h.Faj,
                        MeretCm = h.MeretCm,
                        ToNev = h.To.Nev
                    }).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
